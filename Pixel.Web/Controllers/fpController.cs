using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pixel.Web.Models;


namespace Pixel.Web.Controllers
{
    /// <summary>
    /// this is a handler just for FaceThePlanet media buy
    /// it should only answer 1045 - mac and 2045 pc provider id request
    /// </summary>
    public class fpController : ApiController
    {
        // GET: api/fp
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HttpResponseMessage Get()
        {
           
            Int32[] intProviderPageId;
            Int32 intProviderId, intPageId;
            string platform, redirectUrl;

            redirectUrl = SettingsCache.ERROR_PAGE_URL;
           
            //get page and provider ID 
            intProviderPageId = Pixel.Web.Models.pixHelpers.getProviderPageID(Request);
           
            //get platform to know where to redirect user to PC or MAC
            platform = Pixel.Web.Models.pixHelpers.RunningPlatform();

            intProviderId = intProviderPageId[0];
            intPageId = intProviderPageId[1];

              string stid = Pixel.Web.Models.pixHelpers.GetQueryString(Request, "tid");

            if (intProviderId == 1045 || intProviderId == 2045)
            {
                if (platform.ToLower() == "mac")
                {
                    intProviderId = 1045;
                    intPageId = 1005;//getPageByPlatformAndPageId(intProviderId) ;
                }

                if (platform.ToLower().IndexOf("win")>-1) {
                    intProviderId = 2045;
                    intPageId = 3001;//getPageByPlatformAndPageId(intProviderId);
                }

                redirectUrl="http://www.myloopme.com/api/r/"+intProviderId.ToString()+intPageId.ToString() +"/tid=" +stid;
            }
            

            

            HttpResponseMessage redirectRequest;
            redirectRequest = Request.CreateResponse();
            redirectRequest.StatusCode = HttpStatusCode.Redirect;
            redirectRequest.Headers.Location = new Uri(redirectUrl);


            return redirectRequest;

        }

        private Int32 getPageByPlatformAndPageId(int providerID){
            
            int returnPageId = 3001;

            switch (providerID){
                case 1045: //MAC
                   returnPageId = 1005; 
                    break;

                case 2045://PC
                     returnPageId = 3001; 
                    break;

                
            }

            return returnPageId;
        }

        // POST: api/fp
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/fp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fp/5
        public void Delete(int id)
        {
        }
    }
}
