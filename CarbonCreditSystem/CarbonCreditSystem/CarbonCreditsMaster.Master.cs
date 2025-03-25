using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem
{
    public partial class CarbonCreditsMaster : System.Web.UI.MasterPage
    {
        public int roleId { get; set; }
        public string userName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USER_ROLE"] == null)
                {
                    Response.Redirect("LoginUI.aspx");//IF THERE IS NO SESSION, REDIRECT TO LOGIN PAGE
                }
                else
                {
                    roleId = Convert.ToInt32(Session["USER_ROLE"]);
                    userName = Session["USER_NAME"].ToString();
                    lblname.Text = userName;//SET NAV BAR NAME
                    lblUsername.Text = userName;
                }

            }
            roleId = Convert.ToInt32(Session["USER_ROLE"]);
            userName = Session["USER_NAME"].ToString();
            lblname.Text = userName;
            lblUsername.Text = userName;
            //roleId = 1;
            //userName = "eee";
            //lblUsername.Text = userName;
            //lblname.Text = userName;
        }
    }
}