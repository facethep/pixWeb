using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pixel.Web.Models;
using Pixel.Web.DB;
using System.Collections;
using System.Web;

namespace Pixel.Web.Controllers
{
    public class cacheController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
   (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: api/cache
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/cache/5
        public string Get(int id)
        {
            //List<string> cacheItems = SettingsCache.CACHE_ITEM_NAMES;
            HttpContext oc = HttpContext.Current;
           // List<string> foundCacheItems = new List<string>();

            if (id == 33197000)
            {
                IDictionaryEnumerator en = oc.Cache.GetEnumerator();

                //string strResult;
                while (en.MoveNext())
                {
                    
                    cacheManager.RemoveFromCache(en.Key.ToString());
                }
                
             //   foreach (string cacheName in cacheItems) // Loop through List with foreach
             //   {
             //       cacheManager.RemoveFromCache(cacheName);
             //   }

               
                return "Cache removed";
            }
            else if (id == 791975)
            {
                var x = SettingsCache.GetProvider(1003);
                int y = SettingsCache.getGeoX(1005, "US", 1003);
                int c = SettingsCache.getRealPageID(5010, 1003);
                int d = SettingsCache.getPageByGEO(1000, 1005, "US");
                pixLandingPages f = SettingsCache.GetPage(5502);
                return "Cache populated ";
            }
            else
            {
                log.Fatal("Someone is trying to delete application cache with no proper key");
            }

            return "mmmmm";
            
        }

        // POST: api/cache
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/cache/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/cache/5
        public void Delete(int id)
        {
        }
    }
}
