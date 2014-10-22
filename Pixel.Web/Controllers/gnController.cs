using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Specialized;
using Pixel.Web.Models;

namespace Pixel.Web.Controllers
{
    public class gnController : ApiController 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

      
        public HttpResponseMessage Get([FromUri] NameValueCollection formData)
        {

            string sUID,s2sURL,strThankYouPage;
            sUID = Pixel.Web.Models.pixHelpers.GetQueryString(Request, "uid");
            s2sURL = SettingsCache.GN_S2SURL;
            strThankYouPage = SettingsCache.THANK_YOU_PAGE_URL;
            s2sURL += sUID;


            try
            {

            
            //sending response:
            WebClient proxy = new WebClient();
            log.Info("Sending pixel response to Provider from Geneo handler: " + s2sURL);
            var response = proxy.DownloadString(s2sURL);
            proxy.Dispose();
            }

            catch (Exception e)
            {
                log.Fatal("Could not send INTERNAL GENEO s2s: " + Request.RequestUri.AbsolutePath, e);
            }


            HttpResponseMessage redirectRequest;
            redirectRequest = Request.CreateResponse();
            redirectRequest.StatusCode = HttpStatusCode.Redirect;
            redirectRequest.Headers.Location = new Uri(strThankYouPage);


            return redirectRequest;

        }

    

    }
}
