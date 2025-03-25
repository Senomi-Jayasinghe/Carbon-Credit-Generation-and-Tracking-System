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
    public partial class LoginUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int attempts = 0;
            bool isExistUser;
            LoginController loginController = new LoginController();
            UserLogin userLogin = new UserLogin();
            userLogin.userName = txtEmail.Text;
            userLogin.userPassword = txtPassword.Text;
            //CHECK IF THE USER EXISTS IN SYSTEM WITH THE VALID CREDENTIALS
            isExistUser = loginController.SearchUser(userLogin);

            if (isExistUser)
            {   //IF CREDENTIALS ARE VALID RESET LOGIN ATTEMPTS TO 0
                userLogin.attempts = attempts;
                loginController.updateAttempts(userLogin);
                //GET USER DATA AND SET SESSIONS
                DataTable dt = loginController.getUser(userLogin);
                Session["USER_ROLE"] = dt.Rows[0]["user_role_id"];
                Session["USER_NAME"] = dt.Rows[0]["Name"];
                Session["USER_ID"] = dt.Rows[0]["user_reference_id"];
                Response.Redirect("Dashboard.aspx");
            }
            else
            {   //IF CREDENTIALS ARE NOT VALID GET NUMBER OF ATTEMPTS AND UPDATE BY 1
                lblerror.Visible = true;
                attempts = loginController.getAttempts(userLogin);
                userLogin.attempts = attempts + 1;
                loginController.updateAttempts(userLogin);
            }
        }
    }
}