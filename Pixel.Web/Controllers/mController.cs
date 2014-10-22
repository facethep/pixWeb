using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pixel.Web.DB;
using Pixel.Web.Models;



namespace Pixel.Web.Controllers
{
    /// <summary>
    /// This controller is being used by the Monotor service
    /// </summary>
    public class mController : ApiController
    {
        // GET: api/m

        public enum errorMonitor
        {
            All_OK_Relax = 0,
            Database_Select_Error = 100,
            Database_Insert_Error = 200,
            Settings_Error = 300,
            Http_Get_Error = 600
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Get()
        {

            string returnValue ;
            var db = new PetaPoco.Database("myConnectionString");

            try
            {
                //initialize settings and try to get provider
                //this will initialize settings and will do a select on database
                pixProviders tmpProviders;
                tmpProviders = SettingsCache.GetProvider(1000);
            }

            catch (Exception e)
            {
                log.Fatal("PIX Monitor - settings ", e);
                returnValue = errorMonitor.Settings_Error.ToString();
                return returnValue;
            }

           
            
            //2. test insert to database
            Random rnd = new Random();
            pixMonitor m = new pixMonitor();
            m.id2 = rnd.Next(1,13);

            try{
                db.Insert(m);
                db.CloseSharedConnection();
            }

            catch  (Exception e)  {
                returnValue = errorMonitor.Database_Insert_Error.ToString();
                log.Fatal("PIX Monitor - Insert ",e);
                return returnValue;
            }


            //3. test HTTPcredirect 
            string checkLiveURL = SettingsCache.MONITOR_HTTP_REDIRECT_TEST_URL;
           try
           {
               WebClient proxy = new WebClient();
               var response = proxy.DownloadString(checkLiveURL);
               proxy.Dispose();
               if (response.ToLower().Contains("error")){
                   return errorMonitor.Http_Get_Error.ToString();
               }


           }
           catch (WebException ex)
           {
               var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
               log.Fatal("PIX Monitor - HTTP code :" + statusCode, ex);
               returnValue = errorMonitor.Http_Get_Error.ToString();
               return returnValue;

           }

           return System.DateTime.Now.ToString() + " : " + errorMonitor.All_OK_Relax.ToString();
        }


        // GET: api/m/5
        public string Get(int id)
        {

            string[] pagesList = SettingsCache.MAC_PAGES.Split(',');

            pixLandingPages macPage  = SettingsCache.GetPage(1005);
            string macUrl = macPage.url;
            string currentPageName = macPage.url.Split('/')[4].Split('?')[0]+'f';
            string newPageName;
            newPageName = currentPageName;
            string tmpPageName;
            //getting the new page name 
            for (int i = 0; i < pagesList.Length-1; i++)
            {
                tmpPageName = pagesList[i].ToString();
                if (tmpPageName.ToLower() == currentPageName)
                {
                    newPageName = pagesList[i + 1].ToString();
                }
            }

            if (!newPageName.Equals(currentPageName))
            {

                macPage.url = macPage.url.Replace(currentPageName, newPageName);
                var db = new PetaPoco.Database("myConnectionString");
                db.Update(macPage);
             }

            
            return macPage.url;

        }

        // POST: api/m
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/m/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/m/5
        public void Delete(int id)
        {
        }
    }
}
