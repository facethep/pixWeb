using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pixel.Web
{
    public partial class tmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text;
            string[] tmp2;
            string[] tmp;
            tmp2 = s.Split(new Char[] { '/', '?' });
            tmp = s.Split('/');
        }
    }
}