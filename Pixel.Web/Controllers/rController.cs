using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using Pixel.Web.DB;
using Pixel.Web.Models;
using System.Xml;

/*
 * All providers will get to this handler with the followng structure
 *  http://myloopme.com/api/r/[Providerid][pageid]/?affid=#affid#&tranid=#reqid#
 */

namespace Pixel.Web.Controllers
{
    public class rController : ApiController
    {
        //initialize logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //[FromUri] NameValueCollection formData
        public HttpResponseMessage Get()
        {
           
           // initialize parameters
            string sParam1, sParam2,sParam3, fullurl, sRedirectURL, ERROR_PAGE_URL , ip_address, countryCode;
            sParam1 = sParam2 = sParam3 = fullurl = ip_address = countryCode = string.Empty;
            ERROR_PAGE_URL = SettingsCache.ERROR_PAGE_URL;
            string platform;

            Boolean isFatal = false;

            HttpResponseMessage  response;
            Int32[] intProviderPageId;
            Int32 intProviderId, intPageId;

            //get url from request
            fullurl = Request.RequestUri.AbsoluteUri;
            pixProviders tmpProvider;
            pixLandingPages tmpPage;
            //get providerid & pageid as array of int from url
            intProviderPageId = Pixel.Web.Models.pixHelpers.getProviderPageID(Request);
            platform = Pixel.Web.Models.pixHelpers.RunningPlatform();


            if (intProviderPageId != null)
            {
                intProviderId = intProviderPageId[0];
                intPageId = intProviderPageId[1];

                tmpProvider = SettingsCache.GetProvider(intProviderId);

                if (tmpProvider != null)
                {
                    //look in query string for the query string params names according to what was configured in database
                    sParam1 = Pixel.Web.Models.pixHelpers.GetQueryString(Request, tmpProvider.param1);
                    sParam2 = Pixel.Web.Models.pixHelpers.GetQueryString(Request, tmpProvider.param2);
                    sParam3 = Pixel.Web.Models.pixHelpers.GetQueryString(Request, tmpProvider.param3);
                }

                else {
                    log.Fatal("Could not find provider id: " + intProviderId.ToString() + " in: " + fullurl);
                    isFatal = true;
                }
               
             
                ip_address = Pixel.Web.Models.pixHelpers.GetIPAddress();
                countryCode = Pixel.Web.Models.pixHelpers.GetLocationFromIPDB(ip_address);
                
            
                intPageId = SettingsCache.getRealPageID(intPageId, intProviderId);

                //see if we have a different page ID for that GEO 
                intPageId =  SettingsCache.getPageByGEO(intProviderId, intPageId, countryCode);

                tmpPage = SettingsCache.GetPage(intPageId);

               // 
                
                //get page URL

                if (tmpPage != null)
                {
                   if ((platform.ToUpper()=="MAC") && (tmpPage.pcmac.ToUpper() =="PC")){
                       sRedirectURL = SettingsCache.GetPage(1005).url;
                   }
                    else{
                        sRedirectURL = tmpPage.url;
                    }
                    
                }
                else
                {
                    sRedirectURL = ERROR_PAGE_URL;
                    log.Fatal("Could not find page id: " + intPageId.ToString() + " in  " + fullurl);
                    isFatal = true;
                }

               
                // log to database only good URL's 
                
                if (!isFatal) { 
                     //get new guid to send landing page
                    Guid sGuid = Guid.NewGuid();
                    string stringGuid = sGuid.ToString().Replace("-", "");

                    sRedirectURL = sRedirectURL.Replace("[UID]", stringGuid);

                    //build a request object with all data
                    var myRequest = new pixRequests();
                    myRequest.full_url = fullurl;
                    myRequest.reqGuid = sGuid;
                    myRequest.param1 = sParam1;
                    myRequest.param2 = sParam2;
                    myRequest.param3 = sParam3;
                    myRequest.providerid = intProviderId;
                    myRequest.pageid = intPageId;
                    myRequest.user_ip = ip_address;
                    myRequest.redirect_to = sRedirectURL;
                    myRequest.platform = platform;
                    myRequest.countryCode = countryCode;


                    try
                    {
                        //insert request to database
                        var db = new PetaPoco.Database("myConnectionString");
                        db.Insert(myRequest);
                        db.CloseSharedConnection();

                        
                    }

                    catch (Exception e)
                    {

                        string tmp = " INSERT INTO [dbo].[Requests] ([reqGuid],[full_url],[param1],[user_ip],[redirect_to],[providerid],[pageid],[platform],[countryCode]) VALUES (";
                        tmp +=  " cast ('"+ myRequest.reqGuid +"' as uniqueidentifier),'" +  myRequest.full_url +"','" + myRequest.param1 + "'";
                        tmp +=  ",'" + myRequest.user_ip +"','" + myRequest.redirect_to +"'," +myRequest.providerid.ToString() +"," + myRequest.pageid.ToString();
                        tmp +=  ",'" + myRequest.platform +"','" + myRequest.countryCode + "')";

                        log.Fatal("failed with insert to database: " + fullurl + " /n" + tmp + " /n " + e.Message, e);


                        sRedirectURL = ERROR_PAGE_URL;
                        //throw e;
                    }
             } //  if (isFatal == false) 



            }//if (intProviderPageId != null)
            else
            {
                log.Fatal("No Providerid or Pageid in Query string: " + fullurl + " , redirecting to error page: ");
                sRedirectURL = ERROR_PAGE_URL;
            }

            // redirect to landing page with guid
            response = Request.CreateResponse();
            switch (SettingsCache.REDIRECT_TYPE)
            {

                case 1:
                    response.StatusCode= HttpStatusCode.Moved;
                    break;
                case 2:
                    response.StatusCode = HttpStatusCode.Redirect;    
                    break;
                case 3:
                    response.StatusCode = HttpStatusCode.RedirectKeepVerb;
                    break;
                case 4:
                    response.StatusCode = HttpStatusCode.RedirectMethod;
                    break;
            }
            
            
            response.Headers.Location = new Uri(sRedirectURL);
            log.Info("Redirecting to landing page: " + sRedirectURL);
            return response;


           
        }


          

        // GET: api/r/5
        public string Get(string id)
        {
            //break id to page + provider
            // initialize request + params 
          
            //Questions
            //1. do we need to deprinciate GEO? if so how will we get them ? it is not web/ it is client
            //2. 

            Guid sGuid = Guid.NewGuid();

            return sGuid.ToString();
            
        }

        // POST: api/r
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/r/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/r/5
        public void Delete(int id)
        {
        }
    }
}
