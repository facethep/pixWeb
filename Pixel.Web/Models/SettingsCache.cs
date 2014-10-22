﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pixel.Web.DB;


namespace Pixel.Web.Models
{
    static public class SettingsCache
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string ERROR_PAGE_URL;
        public static Boolean SEND_RESPONSE;
        public static Boolean GENEO_ACTIVE;
        public static Int16 REDIRECT_TYPE;
        public static string GN_S2SURL;
        public static string THANK_YOU_PAGE_URL;
        public static string MONITOR_HTTP_REDIRECT_TEST_URL;
        public static List<string> CACHE_ITEM_NAMES;
        public static string MAC_PAGES;



        static SettingsCache()
        {

            try
            {
                ERROR_PAGE_URL = System.Configuration.ConfigurationManager.AppSettings["ERROR_PAGE_URL"].ToString();
                SEND_RESPONSE = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SEND_RESPONSE"]);
                GENEO_ACTIVE = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["GENEO_ACTIVE"]);
                REDIRECT_TYPE = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["REDIRECT_TYPE"]);
                GN_S2SURL = System.Configuration.ConfigurationManager.AppSettings["GN_S2SURL"].ToString();
                THANK_YOU_PAGE_URL = System.Configuration.ConfigurationManager.AppSettings["THANK_YOU_PAGE_URL"].ToString();
                MONITOR_HTTP_REDIRECT_TEST_URL = System.Configuration.ConfigurationManager.AppSettings["MONITOR_HTTP_REDIRECT_TEST_URL"].ToString();
                CACHE_ITEM_NAMES = new List<string>(new string[] { "providers", "landingPages", "landingPagesByGeo", "landingPagesMask", "landingPages_X_Mask" });
                MAC_PAGES = System.Configuration.ConfigurationManager.AppSettings["MAC_PAGE_NAMES"].ToString();

            }
            catch (Exception e)
            {
                log.Fatal("!! cannot load configuration from web.config!!", e);

            }

        }

        public static List<pixProviders> loadProviders()
        {
            string cacheName = "providers";
            log.Info("Settings - loadProviders ");

            List<pixProviders> providers;
            if (!cacheManager.IsIncache(cacheName))
            {
                var db = new PetaPoco.Database("myConnectionString");

                string sQuery = "SELECT * FROM providers";
                try
                {
                    var result = db.Fetch<pixProviders>(sQuery);
                    db.CloseSharedConnection();
                    providers = result.ToList();
                    cacheManager.SaveTocache(cacheName, providers);
                }

                catch (Exception e)
                {
                    log.Fatal("!! cannot load Settings of all providers !!", e);
                }
            }
            return cacheManager.GetFromCache<List<pixProviders>>(cacheName);

        }


        public static pixProviders GetProvider(int provideID)
        {

            List<pixProviders> providers = loadProviders();

            try
            {

                return providers.Find(x => x.id == provideID);

            }
            catch (Exception e)
            {
                log.Fatal("!! ERROR settings - GetProvider!!", e);


            }
            return null;
        }


        private static List<pixLandingPages> loadLandingPages()
        {
            string cacheName = "landingPages";

            log.Info("Settings - loadLandingPages ");

            List<pixLandingPages> landingPages;
            if (!cacheManager.IsIncache(cacheName))
            {
                var db = new PetaPoco.Database("myConnectionString");
                string sQuery = "SELECT * FROM LandingPages";
                try
                {
                    var result = db.Fetch<pixLandingPages>(sQuery);
                    db.CloseSharedConnection();
                    landingPages = result.ToList();
                    cacheManager.SaveTocache(cacheName, landingPages);

                }

                catch (Exception e)
                {
                    log.Fatal("!! cannot load Settings of all Landing pages !!", e);
                }
            }
            return cacheManager.GetFromCache<List<pixLandingPages>>(cacheName);

        }


        public static pixLandingPages GetPage(int pageID)
        {

            List<pixLandingPages> pages = loadLandingPages();

            return pages.Find(x => x.id == pageID);
        }


        private static List<pixLandingPagesByGEO> loadLandingPagesByGEO()
        {
            string cacheName = "landingPagesByGeo";
            log.Info("Settings - loadLandingPagesByGEO ");


            List<pixLandingPagesByGEO> pagesByGEO;
            if (!cacheManager.IsIncache(cacheName))
            {
                var db = new PetaPoco.Database("myConnectionString");
                string sQuery = "SELECT * FROM LandingPagesByGEO";
                try
                {
                    var result = db.Fetch<pixLandingPagesByGEO>(sQuery);
                    db.CloseSharedConnection();
                    pagesByGEO = result.ToList();
                    cacheManager.SaveTocache(cacheName, pagesByGEO);

                }

                catch (Exception e)
                {
                    log.Error("!! cannot load Settings of all Landing pages By GEO !!", e);
                }
            }
            return cacheManager.GetFromCache<List<pixLandingPagesByGEO>>(cacheName);

        }


        public static pixLandingPagesByGEO GetPageByGEO(int pageID, string GEO)
        {
            List<pixLandingPagesByGEO> pagesByGEO = loadLandingPagesByGEO();
            return pagesByGEO.Find(x => x.pageid == pageID && x.countryCode == GEO);
        }


        public static Boolean checkLandingPageByGeo(int pageID)
        {
            if (pageID.Equals(5000) || pageID.Equals(5010) || pageID.Equals(5011))
            {
                return false;
                //TODO: work on logic
            }
            return false;

        }



        private static List<pixLandingPagesMask> loadLandingPagesMask()
        {
            string cacheName = "landingPagesMask";
            log.Info("Settings - loadLandingPagesMask ");


            List<pixLandingPagesMask> pagesMask;
            if (!cacheManager.IsIncache(cacheName))
            {

                var db = new PetaPoco.Database("myConnectionString");
                string sQuery = "SELECT * FROM LandingPagesMask";
                try
                {
                    var result = db.Fetch<pixLandingPagesMask>(sQuery);
                    db.CloseSharedConnection();
                    pagesMask = result.ToList();
                    cacheManager.SaveTocache(cacheName, pagesMask);

                }

                catch (Exception e)
                {
                    log.Error("!! cannot load Settings of all Landing pages Mask !!", e);
                }
            }
            return cacheManager.GetFromCache<List<pixLandingPagesMask>>(cacheName);

        }

        public static int getRealPageID(int pageID, int providerID)
        {

            List<pixLandingPagesMask> pagesMask = loadLandingPagesMask();

            pixLandingPagesMask mask_provider, mask_global;

            mask_global = pagesMask.Find(x => x.pageid_origin == pageID && x.providerid == -1);
            mask_provider = pagesMask.Find(x => x.pageid_origin == pageID && x.providerid == providerID);

            if (mask_provider != null)
            {
                return mask_provider.pageid_redirectTo;
            }
            if (mask_global != null)
            {
                return mask_global.pageid_redirectTo;
            }



            return pageID;
        }



        private static List<pixLandingPages_X_Mask> loadLandingPagesMask_X()
        {
            string cacheName = "landingPages_X_Mask";

            log.Info("Settings - loadLandingPagesMask_X ");

            List<pixLandingPages_X_Mask> pages_X_Mask;
            if (!cacheManager.IsIncache(cacheName))
            {
                var db = new PetaPoco.Database("myConnectionString");
                string sQuery = "SELECT * FROM LandingPages_X_Mask";
                try
                {
                    var result = db.Fetch<pixLandingPages_X_Mask>(sQuery);
                    db.CloseSharedConnection();
                    pages_X_Mask = result.ToList();
                    cacheManager.SaveTocache(cacheName, pages_X_Mask);
                }

                catch (Exception e)
                {
                    log.Error("!! cannot load Settings of all Landing pages X Mask !!", e);
                }
            }
            return cacheManager.GetFromCache<List<pixLandingPages_X_Mask>>(cacheName);

        }

        public static int getGeoX(int pageID, string countryCode, int providerid)
        {
            List<pixLandingPages_X_Mask> pages_X_Mask = loadLandingPagesMask_X();

            pixLandingPages_X_Mask pageByCountryCode;
            pixLandingPages_X_Mask pageByProviderX;

            pageByProviderX = pages_X_Mask.Find(x => x.pageid == pageID && x.countryCode == countryCode && x.providerid == providerid);
            pageByCountryCode = pages_X_Mask.Find(x => x.pageid == pageID && x.countryCode == countryCode && x.providerid == -1);


            if (pageByProviderX != null)
            {
                return pageByProviderX.sendResponseEvery_x;
            }

            if (pageByCountryCode != null)
            {
                return pageByCountryCode.sendResponseEvery_x;
            }

            return -1;
        }




    }
}