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
    public partial class AuthorizersUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getAuthorizers(); //GET AUTHORIZERS FROM DB
        }

        protected void getAuthorizers()
        {   //GET AUTHORIZERS FROM DB
            UserController userController = new UserController();
            DataTable dt =userController.getAuthorizers();

            grdAuthorizers.DataSource = dt;
            grdAuthorizers.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {   //REDIRECT TO USER DETAILS TABLE IN ADD (A) MODE
            Response.Redirect("UserUI.aspx?mode=A");
        }

        protected void grdAuthorizers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {   //REDIRECT TO USER DETAILS TABLE IN UPDATE (U) MODE
            HiddenField field = (HiddenField)grdAuthorizers.Rows[e.NewSelectedIndex].FindControl("hdnGrdAuthorizerId");
            int userId = Convert.ToInt32(field.Value);
            Response.Redirect("UserUI.aspx?userID=" + userId + "&mode=U");
        }

        protected void grdAuthorizers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {   //REDIRECT TO USER DETAILS TABLE IN DELETE (D) MODE
            HiddenField field = (HiddenField)grdAuthorizers.Rows[e.RowIndex].FindControl("hdnGrdAuthorizerId");
            int userId = Convert.ToInt32(field.Value);
            Response.Redirect("UserUI.aspx?userID=" + userId + "&mode=D");
        }
    }
}