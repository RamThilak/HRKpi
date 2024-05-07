using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRKpi
{
    public partial class PMS_Approve_Mst : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }

}