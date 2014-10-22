using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Pixel.Web.Models
{
    public static class cacheManager
    {
        //public static void SaveTocache(string cacheKey, object savedItem, DateTime absoluteExpiration)
        public static void SaveTocache(string cacheKey, object savedItem)
        {
            if (IsIncache(cacheKey))
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }

            HttpContext.Current.Cache.Add(cacheKey, savedItem, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(2, 0, 0), System.Web.Caching.CacheItemPriority.Default, null);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return HttpContext.Current.Cache[cacheKey] as T;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        public static bool IsIncache(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey] != null;
        }





    }
}