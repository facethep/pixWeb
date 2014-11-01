using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Pixel.Web.Models;
using Pixel.Web.DB;
namespace Pixel.Web.sys
{
    public partial class cacheObjets : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {  showCache();}
               
        }



        private void showCache()
        {
            HttpContext oc = HttpContext.Current;

            List<string> foundCacheItems = new List<string>();

            IDictionaryEnumerator en = oc.Cache.GetEnumerator();
            while (en.MoveNext())
            {

                if (en.Key.ToString().IndexOf("WebPages") == -1) { 
                    foundCacheItems.Add(en.Key.ToString());
                    DataGrid aa = new DataGrid();
                    aa.EnableViewState = false;
                    aa.DataSource = en.Value;
                    aa.DataBind();
                    Label lbl = new Label();
                    lbl.Text = "<h2>Cache content of: " + en.Key + "</h2>";

                    Page.Controls.Add(lbl);

                    Page.Controls.Add(aa);


                }

            }

            BulletedList1.DataSource = foundCacheItems;
            BulletedList1.DataBind();


        }


        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnRefreshCach_Click(object sender, EventArgs e)
        {
           // foreach (string cacheName in cacheItems) // Loop through List with foreach
          //  {
          //      cacheSettings.RemoveFromCache(cacheName);
           // }
          //  showCache();

        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            var x = SettingsCache.GetProvider(1003);
            int y = SettingsCache.getGeoX(1005, "US", 1003);
            int c = SettingsCache.getRealPageID(5010, 1003);
           // pixLandingPagesByGEO d = SettingsCache.GetPageByGEO(5011, "US");
            pixLandingPages f = SettingsCache.GetPage(5502);

            lblStatus.Text = "Done - " + System.DateTime.Today.ToLongTimeString();
        }

        protected void btnShowCache_Click(object sender, EventArgs e)
        {
            showCache();
        }

     
    }
}