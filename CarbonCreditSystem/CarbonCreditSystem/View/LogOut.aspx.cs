using CarbonCreditSystem.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginController loginController = new LoginController();
            int userId = Convert.ToInt32(Session["USER_ID"]);
            loginController.endSession(userId); //UPDATE USER STATUS AS UNAVAILABLE (U)
            Session.RemoveAll();
            Response.Redirect("LoginUI.aspx");
        }
    }
}