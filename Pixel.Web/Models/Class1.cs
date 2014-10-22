using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Pixel.Web.DB;

namespace CachePoc
{
    class Program
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static object everoneUseThisLockObject4Cache_Providers = new object();
        const string Cache_Providers = "Providers";
        static object everoneUseThisLockObject4Cache_Pages = new object();
        const string Cache_Pages = "Pages";
        static object everoneUseThisLockObject4Cache_PagesByGeo = new object();
        const string Cache_PagesByGeo = "PagesByGeo";

        static void Main(string[] args)
        {
           // List<pixProviders> providers = MemoryCacheHelper.GetCachedData(Cache_Providers, everoneUseThisLockObject4Cache_Providers, 20, SomeHeavyAndExpensive_Providers_Calculation);
           // List<pixLandingPages> pages = MemoryCacheHelper.GetCachedData(Cache_Pages, everoneUseThisLockObject4Cache_Pages, 20, SomeHeavyAndExpensive_Pages_Calculation);
           // List<pixLandingPagesByGEO> pagesByGeo = MemoryCacheHelper.GetCachedData(Cache_Pages, everoneUseThisLockObject4Cache_PagesByGeo, 20, SomeHeavyAndExpensive_PagesByGeo_Calculation);
        }

        public static  List<pixProviders> providers() {
            List<pixProviders> providers;
            providers  = MemoryCacheHelper.GetCachedData(Cache_Providers, everoneUseThisLockObject4Cache_Providers, 20, SomeHeavyAndExpensive_Providers_Calculation);

            return providers;

        }

        public static Boolean DeleteCach(string cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
            return true;
        }
        
       // private static List<pixLandingPages> pages;
       // private static List<pixLandingPagesByGEO> pagesByGEO;



        private static List<pixProviders>  SomeHeavyAndExpensive_Providers_Calculation() {

            List<pixProviders> tmp_providers = null;

            var db = new PetaPoco.Database("myConnectionString");

            string sQuery = "SELECT * FROM providers";
            try
            {
                var result = db.Fetch<pixProviders>(sQuery);
                db.CloseSharedConnection();
                tmp_providers = result.ToList();
            }

            catch (Exception e)
            {
                log.Fatal("!! cannot load Settings of all providers !!", e);
            }
            return tmp_providers;
        
        }

        private static List<pixLandingPages>  SomeHeavyAndExpensive_Pages_Calculation() {

            List<pixLandingPages> tmp_LandingPages = null;

           string  sQuery = "SELECT * FROM LandingPages";
           var db = new PetaPoco.Database("myConnectionString");

            try
            {
                var result = db.Fetch<pixLandingPages>(sQuery);
                db.CloseSharedConnection();
                tmp_LandingPages = result.ToList();
            }

            catch (Exception e)
            {
                log.Fatal("!! cannot load Settings of all Landing pages !!", e);
            }
            
            return tmp_LandingPages;
        }


        private static List<pixLandingPagesByGEO> SomeHeavyAndExpensive_PagesByGeo_Calculation()
        {

            List<pixLandingPagesByGEO> tmp_LandingPagesByGeo = null;

            string sQuery = "SELECT * FROM LandingPagesByGEO";
           var db = new PetaPoco.Database("myConnectionString");

            try
            {
                var result = db.Fetch<pixLandingPagesByGEO>(sQuery);
                db.CloseSharedConnection();
                tmp_LandingPagesByGeo = result.ToList();
            }

            catch (Exception e)
            {
                log.Fatal("!! cannot load Settings of all Landing pages !!", e);
            }

            return tmp_LandingPagesByGeo;
        }

        

        public static class MemoryCacheHelper
        {
            public static Boolean  DeleteCach (string cacheKey){
                MemoryCache.Default.Remove(cacheKey);
                return true;
            }

            public static T GetCachedData<T>(string cacheKey, object cacheLock, int cacheTimePolicyMinutes, Func<T> GetData)
                where T : class
            {
                //Returns null if the string does not exist, prevents a race condition where the cache invalidates between the contains check and the retreival.
                T cachedData = MemoryCache.Default.Get(cacheKey, null) as T;

                if (cachedData != null)
                {
                    return cachedData;
                }

                lock (cacheLock)
                {
                    //Check to see if anyone wrote to the cache while we where waiting our turn to write the new value.
                    cachedData = MemoryCache.Default.Get(cacheKey, null) as T;

                    if (cachedData != null)
                    {
                        return cachedData;
                    }

                    //The value still did not exist so we now write it in to the cache.
                    CacheItemPolicy cip = new CacheItemPolicy()
                    {
                        AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(cacheTimePolicyMinutes))
                    };
                    cachedData = GetData();
                    MemoryCache.Default.Set(cacheKey, cachedData, cip);
                    return cachedData;
                }
            }
        }
    }
}