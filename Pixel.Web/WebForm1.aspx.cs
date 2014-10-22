using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetaPoco;
using Pixel.Web.DB;
using Pixel.Web.Models;
using log4net;
using Pixel.Web.Controllers;
using System.Configuration;

namespace Pixel.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
       

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected void Page_Load(object sender, EventArgs e)
        {

      //    List <pixProviders> p =  Program.providers();


       //   int c = p.Count();
        //  Program.DeleteCach("Providers");
         // p = Program.providers();


        }

       

   

        protected void Button2_Click(object sender, EventArgs e)
        {

            log.Error("erro me");
            log.Debug("debug me");
            log.Fatal(" fatal me");
            
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var db = new PetaPoco.Database("myConnectionString");

           string queryBooks = "SELECT * FROM Requests order by date_created desc"; 
        var result = db.Query<pixRequests>(queryBooks);
       // GridView1.DataSource = result;
        //GridView1.DataBind(); 


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var db = new PetaPoco.Database("myConnectionString");

            string queryBooks = "SELECT * FROM Responses order by date_created desc";
            var result = db.Query<pixResponses>(queryBooks);
          //  GridView1.DataSource = result;
           // GridView1.DataBind(); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

      

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string s = Pixel.Web.Models.pixHelpers.GetLocationFromIPDB(txtIP.Text);
            lblCountry.Text = s;
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {

            string s = Pixel.Web.Models.pixHelpers.GetLocationFromIPDB(txtIP.Text);
            lblCountry.Text = s;

        }

        protected void rfrCache_Click(object sender, EventArgs e)
        {

            cacheManager.RemoveFromCache("providers");

           pixProviders p = Models.SettingsCache.GetProvider(Convert.ToInt16(txtIP.Text));

             var db = new PetaPoco.Database("myConnectionString");
                string sQuery = "SELECT * FROM LandingPagesByGEO";
             
                    var result = db.Fetch<object>(sQuery);
               

            if (p == null)
                label1.Text = "NULL";
            else
                label1.Text = p.name;
        }
      

      
    }
}