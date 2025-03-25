using CarbonCreditSystem.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (getPassword()) //GET OLD PASSWORD
            {
                if (txtNewPsw.Text == txtConfirmPsw.Text) //IF NEW PASSSWORD AND CONFIRM PASSWORD IS THE SAME
                {
                    updatePassword(); //UPDATE PASSWORD TO DATABASE
                    lblPnlMsg.Visible = true;
                    lblPnlMsg.CssClass = "alert alert-success text-success";
                    lblPnlMsg.Text = "Update Successful";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Password Updated Successfuly', " +
                    "showConfirmButton: true});", true);
                }
                else
                {   //ALERT USER THAT NEW AND CONFIRM PASSWORD DO NOT MATCH
                    lblPnlMsg.Visible = true;
                    lblPnlMsg.CssClass = "alert alert-danger text-danger";
                    lblPnlMsg.Text = "Passwords do not match. Please try again";
                }
            }
            else
            {   //ALERT TO USER THAT OLD PASSWORD IS INCORRECT
                lblPnlMsg.Visible = true;
                lblPnlMsg.CssClass = "alert alert-danger text-danger";
                lblPnlMsg.Text = "Incorrect Password Entered";
            }
        }

        protected bool getPassword()
        {   //GET OLD PASSWORD FROM DB
            PasswordController passwordController = new PasswordController();
            int UserID = Convert.ToInt32(Session["USER_ID"]);
            string OldPassword = txtOldPsw.Text;
            return passwordController.getOldPassword(UserID, OldPassword);
        }

        protected void updatePassword()
        {   //UPDATE PASSWORD TO DB
            PasswordController passwordController = new PasswordController();
            int UserID = Convert.ToInt32(Session["USER_ID"]);
            string NewPassword = txtNewPsw.Text;
            passwordController.UpdatePassword(NewPassword, UserID);
        }
    }
}