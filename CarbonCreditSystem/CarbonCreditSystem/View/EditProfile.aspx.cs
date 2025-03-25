using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class EditProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["USER_ID"]);
            Response.Redirect("UserUI.aspx?userID=" + userId + "&mode=U");
        }
    }
}