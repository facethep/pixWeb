using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

using System.Xml;

namespace Pixel.Web.Models
{
  

    /// <summary>
    /// Extends the HttpRequestMessage collection
    /// </summary>
    public static class pixHelpers
    {

        //private static WebClient proxy ;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

      //  static pixHelpers() {
        //     proxy = new WebClient();
      // }
        
        /// <summary>
        /// Returns a dictionary of QueryStrings that's easier to work with 
        /// than GetQueryNameValuePairs KevValuePairs collection.
        /// 
        /// If you need to pull a few single values use GetQueryString instead.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryStrings(this HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs()
                          .ToDictionary(kv => kv.Key, kv=> kv.Value, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns an individual querystring value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryString(this HttpRequestMessage request, string key)
        {      
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
                return null;

            return keys.First();
        }

        /// <summary>
        /// Retrieves an individual cookie from the cookies collection
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(this HttpRequestMessage request, string cookieName)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
            if (cookie != null)
                return cookie[cookieName].Value;

            return null;
        }


        //Return the IP of the user
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string _ip;
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    _ip =  addresses[0];
                    if (_ip == "" || _ip.ToLower() == "unknown")
                    {
                        return  context.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        return _ip;
                    }
                }
            } 

           
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }


      /* public static string GetLocationFromIP(string IP)
        {
            string strRequestLocation = "http://freegeoip.net/xml/";
            strRequestLocation += IP;
            XmlDocument xml = new XmlDocument();
            string retVal = string.Empty;

            try
            {
                if (!proxy.IsBusy) { 
                    var response = proxy.DownloadString(strRequestLocation);
                    xml.LoadXml(response);
                    XmlNode node = xml.SelectSingleNode("/Response/CountryCode");
                    retVal=  node.InnerText;
                }
            }

            catch (Exception e)
            {
                log.Error("Error getting location for IP: " + IP, e);
               
            }

            return retVal;
        }*/





        public static string RunningPlatform()
        {

            string oAgent = HttpContext.Current.Request.UserAgent;
            string retVal = string.Empty;

            try
            {
                
                //var osInfo = oAgent.Split(new Char[] { '(', ')' ,'/'})[1];
                if (oAgent.ToLower().Contains("mac_powerpc") || oAgent.ToLower().Contains("macintosh"))
                {
                    retVal = "Mac";
                }

                else if (oAgent.ToLower().Contains("linux") || oAgent.ToLower().Contains("python")) {
                    retVal = "Linux";
                }

                else
                {
                    retVal = HttpContext.Current.Request.Browser.Platform;
                }
            }
            catch(Exception e)
            {
                log.Fatal("Error getting platform from:  " + oAgent, e);
                
            }

            return retVal;       

        }


        public static string GetLocationFromIPDB(string IP)
        {

            string GeoipDb = System.Configuration.ConfigurationManager.AppSettings["GEO_COUNTRY_DB"].ToString();
            //open the database
            string retVal = "";

            try
            {
                LookupService ls = new LookupService(GeoipDb, LookupService.GEOIP_MEMORY_CACHE);
                //get country of the ip address
                Country c = ls.getCountry(IP);

                if (c != null)
                {
                    retVal = c.getCode();
                    //-- means there is no city for that IP in the database
                    if (retVal == "--") { retVal = ""; }
                }
            }

            catch (Exception e)
            {
                log.Error("Could not return IP from DATABASE", e);
                retVal = "";
            }

            if (retVal == "")
            {
                retVal = GetLocationFromIP(IP);
            }


            return retVal;
        }

        public static string GetLocationFromIP(string IP)
        {
            string strRequestLocation = "http://freegeoip.net/xml/";
            WebClient proxy = new WebClient();

            strRequestLocation += IP;
            XmlDocument xml = new XmlDocument();
            string retVal = "N/A";


            try
            {
                if (!proxy.IsBusy)
                {
                    
                    var response = proxy.DownloadString(strRequestLocation);
                    xml.LoadXml(response);
                    XmlNode node = xml.SelectSingleNode("/Response/CountryCode");
                    retVal = node.InnerText;
                }
            }

            catch 
            {
                log.Error("rController - Error getting location for IP: " + IP);

            }
            proxy.Dispose();

            return retVal;
        }



        public static Int32[] getProviderPageID(HttpRequestMessage request)
        {

            string uri = request.RequestUri.AbsoluteUri;
            string[] tmp;

            Int32[] retVal = null;
            tmp = uri.Split(new Char[] { '/', '?' });


            Int32 number;
            string strTmp;

            try
            {
                //the provider and page id will always be in the 5th element in
                //http://www.myloopme.com/api/r/10003000/?uid=ffff 
                strTmp = tmp[5];
                if (Int32.TryParse(strTmp, out number))
                {
                    retVal = new Int32[2];
                    retVal[0] = Convert.ToInt32(strTmp.Substring(0, 4));
                    retVal[1] = Convert.ToInt32(strTmp.Substring(4, 4));
                    if (retVal[0] == 0 && retVal[1] == 0) { retVal = null; }
                }
                else
                {
                    retVal = null;
                }

            }
            catch
            {

                log.Error("getProviderPageID: cannot get providerid or pageid from querystring: " + uri);
                retVal = null;
            }


            return retVal;
        }

    }
    

}