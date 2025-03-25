using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CarbonCreditSystem.View
{
    public partial class UserUI : System.Web.UI.Page
    {
        string mode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getTitleData(); //GET TITLES FORM DB TO DROP DOWN LIST

                if (Request.QueryString["userID"] != null)
                {
                    int userID; //GET USER_ID
                    if (int.TryParse(Request.QueryString["userID"], out userID))
                    {
                        getData(userID); //LOAD USER ID DATA

                        if (Request.QueryString["mode"] == "U")
                        {
                            btnSave.Text = "Update";
                            ViewState["mode"] = "U"; //UPDATE MODE
                        }
                        else if (Request.QueryString["mode"] == "D")
                        {
                            btnSave.Text = "Delete";
                            btnSave.CssClass = "btn btn-danger";
                            ViewState["mode"] = "D"; //DELETE MODE
                        }
                    }

                }
                if (Request.QueryString["mode"] == "A")
                {
                    ViewState["mode"] = "A"; //GET ADD MODE
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {   //GET DATA FROM THE FORM
            try
            {
                UserController userController = new UserController();
                User user = new User();

                user.titleId = Convert.ToInt32(ddlTitle.SelectedValue);
                user.userFirstName = txtFirstName.Text;
                user.userLastName = txtLastName.Text;
                user.userFullName = txtFullName.Text;
                user.userAddress = txtAddress.Text;
                user.userTelephoneNo = txtTelephoneNo.Text;
                user.userMobileNo = txtMobileNo.Text;
                user.userEmail = txtEmail.Text;
                user.userNIC = Convert.ToInt64(txtNIC.Text);
                user.userType = 1;
                mode = ViewState["mode"].ToString();

                if (btnSave.Text == "Save" && mode == "A")
                {   //IF ADDING AN AUTHORIZER
                    user.userType = 2; //(AUTHORIZER)
                    user.entryUser = Convert.ToInt32(Session["USER_ID"]);
                    user.entryDate = DateTime.Now;
                    string password = generatePassword(); //GENERATE A LOGIN PASSWORD FOR THE AUTHORIZER
                    userController.SaveAuthorizer(user, password); //SAVE AUTHORIZER
                    string subject = "You are an Authorizer!";
                    string body = "Username: " + user.userEmail + "\nPassword: " + password;
                    string toEmailAddress = user.userEmail;
                    SendEmail(subject, body, toEmailAddress, null, string.Empty); //Send Email
                    lblPnlMsg.Text = "Username: " + user.userEmail + ", Password: " + password; //DISPLAY USERNAME AND PASSWORD
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Authorizer Added Successfully', " +
                    "showConfirmButton: true});", true);
                }
                else if (btnSave.Text == "Save")
                {
                    userController.SaveUser(user);
                    lblPnlMsg.Text = "Save Success";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'User Saved Successfully', " +
                    "showConfirmButton: true});", true);
                }
                else if (btnSave.Text == "Update")
                {
                    user.userId = Convert.ToInt32(hdnUserId.Value);
                    userController.UpdateUser(user);
                    lblPnlMsg.Text = "Details Updated Successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'User Details Updated Successfully', " +
                    "showConfirmButton: true});", true);
                }
                else if (btnSave.Text == "Delete")
                {
                    user.userId = Convert.ToInt32(hdnUserId.Value);
                    userController.DeleteUser(user.userId);
                    lblPnlMsg.Text = "Details Deleted Successfully";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'User Deleted Successfully', " +
                    "showConfirmButton: true});", true);
                }

                lblPnlMsg.Visible = true;
                lblPnlMsg.CssClass = "alert alert-success text-success";
            }

            catch (Exception ex)
            {
                lblPnlMsg.Visible = true;
                lblPnlMsg.CssClass = "alert alert-danger text-danger";
                lblPnlMsg.Text = "Save Error!" + ex.Message;
            }


        }
        protected void getTitleData()
        {    //GET TITLES FORM DB TO DROP DOWN LIST
            TitleController titleController = new TitleController();
            DataTable dt = new DataTable();
            dt = titleController.GetTitle();

            ddlTitle.DataSource = dt;
            ddlTitle.DataTextField = "title_description";
            ddlTitle.DataValueField = "title_id";
            ddlTitle.DataBind();

            ddlTitle.Items.Insert(0, new ListItem("Select Title", "0"));
        }

        protected void txtLastName_TextChanged(object sender, EventArgs e)
        {   //COMBINE FIRST NAME AND LAST NAME
            txtFullName.Text = txtFirstName.Text + " " + txtLastName.Text;
        }

        protected void getData(int userID)
        {   //GET DATA AND FILL FORM
            UserController userController = new UserController();
            User user = userController.getUserByID(userID);

            ddlTitle.SelectedValue = user.titleId.ToString();
            txtFirstName.Text = user.userFirstName;
            txtLastName.Text = user.userLastName;
            txtFullName.Text = user.userFullName;
            txtAddress.Text = user.userAddress;
            txtTelephoneNo.Text = user.userTelephoneNo;
            txtMobileNo.Text = user.userMobileNo;
            txtEmail.Text = user.userEmail;
            txtNIC.Text = user.userNIC.ToString();
            hdnUserId.Value = user.userId.ToString();
        }

        protected static string generatePassword()
        {   //GENERATE PASSWORD FOR AUTHORIZER
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+[]{}|;:,.<>?";
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }

        public void SendEmail(string subject, string body, string toEmailAddress, byte[] mergeDoc, string attachementName)
        {
            try
            {
                // Define SMTP server details
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // or 25
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("senomi.jayasinghe@gmail.com", "uvbx upvx igog dkqf"),
                    EnableSsl = true, // Use SSL if required
                };

                // Compose the email
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("senomi.jayasinghe@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true, // Set to true if you're sending an HTML email
                };
                // Add recipient
                mail.To.Add(toEmailAddress);

                smtpClient.Send(mail);

            }
            catch (Exception ex)
            {
                Response.Write($"Failed to send email. Error: {ex.Message}");
            }

        }
    }
}