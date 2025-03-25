using CarbonCreditSystem.Controller;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class ForgotPasswordUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string NewPassword = generatePassword();
            string UserName = txtEmail.Text;
            updatePassword(NewPassword, UserName); //Update DB with new Password
            string subject = "Carbon Credit Generation and Tracking System Login New Password";
            string body = "Your new Password is " + NewPassword;
            string toEmailAddress = UserName;
            SendEmail(subject, body, toEmailAddress, null, string.Empty); //Send Email
            lblMsg.Visible = true;
        }

        protected static string generatePassword()
        {   //GENERATE RANDOM PASSWORD
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+[]{}|;:,.<>?";
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }

        protected void updatePassword(string NewPassword, string UserName)
        {
            PasswordController passwordController = new PasswordController();
            passwordController.update(NewPassword, UserName);//UPDATE PASSWORD IN DB
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
