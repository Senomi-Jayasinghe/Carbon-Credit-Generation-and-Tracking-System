using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class SignUpUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (IsEmailExist()) //CHECK IF THE USER IS ALREADY IN THE SYSTEM
            {
                lblerror.Text = "Email Already Exists";
            }
            else
            {
                if (txtCreatePsw.Text == txtConfirmPsw.Text)
                {
                    EnterDetails();//ENTER DETAILS
                    Response.Redirect("HomeUI.aspx");
                }
                else
                {   //IF CONFIRM PASSWORD AND NEW PASSWORD DOESNT MATCH
                    lblerror.Text = "Passwords do not match. Please try again";
                }
            }
        }

        protected bool IsEmailExist()
        {   //CHECK IF THE USER EXISTS IN THE SYSTEM
            SignUpController signUpController = new SignUpController();
            string Email = txtEmail.Text;
            return signUpController.IsExistEmail(Email);
        }
        protected void EnterDetails()
        {   //ENTER USER DETAILS 
            User user = new User();
            user.userFirstName = txtFirstName.Text;
            user.userLastName = txtLastName.Text;
            user.userFullName = txtFirstName.Text + " " + txtLastName.Text;
            user.userEmail = txtEmail.Text;
            user.userType = 1;
            //CREATE A LOGIN
            UserLogin userLogin = new UserLogin();
            userLogin.userName = txtEmail.Text;
            userLogin.userPassword = txtCreatePsw.Text;
            userLogin.lastPasswordChangeDate = DateTime.Now;
            userLogin.userRoleId = 1;

            SignUpController signUpController = new SignUpController();
            signUpController.EnterDetails(user, userLogin);
        }
    }
}