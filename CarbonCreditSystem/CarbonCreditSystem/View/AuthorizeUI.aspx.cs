using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class AuthorizeUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getPendingCC();
        }

        protected void getPendingCC() //GET PENDING CARBON CREDITS FOR AUTHORIZATION
        {
            AuthorizeController authorizeController = new AuthorizeController();
            DataTable dt = authorizeController.getPendingCC();

            grdCarbonCredits.DataSource = dt;
            grdCarbonCredits.DataBind();
        }

        protected void grdCarbonCredits_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {   //REDIRECT TO DETAILS PAGE
            HiddenField field = (HiddenField)grdCarbonCredits.Rows[e.NewSelectedIndex].FindControl("hdnGrdCCId");
            CarbonCreditDetails carbonCreditDetails = new CarbonCreditDetails();
            hdnCCId.Value = field.Value;
            carbonCreditDetails.ccGeneratedId = Convert.ToInt32(field.Value);
            Response.Redirect("AuthorizeCCUI.aspx?ccGeneratedID=" + carbonCreditDetails.ccGeneratedId);
        }
    }
}