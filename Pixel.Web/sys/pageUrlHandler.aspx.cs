using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pixel.Web.DB;
using Pixel.Web.Models;



namespace Pixel.Web.sys
{
    public partial class pageUrlHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int intPageId,number;
            string pageid = Request["pageid"];
            pixLandingPages oPage;

            if (Int32.TryParse(pageid, out number))
            {
                intPageId = Convert.ToInt16(pageid);
                oPage = SettingsCache.GetPage(intPageId);
                Response.Write(oPage.url);
            }
            else
            {
                Response.Write("");
            }

            


        }
    }
}