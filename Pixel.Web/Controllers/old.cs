using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pixel.Web.Controllers
{
    public class old


    {
        // THIS CODE IS FROM R CONTROLLER

        //this is for AirInstaller AND IronSource to redirect according to page and GEO
                //Please NOTE: in the sControler there is the same logic like this for sending pixel
                

                //TODO: work on better logic !!!
        /*
                if (SettingsCache.checkLandingPageByGeo(intPageId))
                {
                    pixLandingPagesByGEO oPage = SettingsCache.GetPageByGEO(intPageId, countryCode);
                    if (oPage != null)
                    {
                        sRedirectURL = oPage.url;
                    }
                    else 
                    {
                        if (intPageId.Equals(5000))
                        { //different default URLs for air installer and iron source
                            sRedirectURL = SettingsCache.GetPageByGEO(intPageId, "BR").url;  //ERROR_PAGE_URL;
                            //TODO: need to redirect to somewhere else 
                           // log.Fatal("AirInstaller - could not find page " + intPageId .ToString()+ " with GEO: " + countryCode + ",IP: " + ip_address + ", in  " + fullurl);
                            isFatal = false;
                        }
                        else{
                            sRedirectURL = SettingsCache.GetPageByGEO(intPageId, "IT").url;  //ERROR_PAGE_URL;
                            //TODO: need to redirect to somewhere else 
                           // log.Fatal("AirInstaller - could not find page " + intPageId .ToString()+ " with GEO: " + countryCode + ",IP: " + ip_address + ", in  " + fullurl);
                            isFatal = false;
                        }
                    }
                    
                }
         * 
         */
    }
}