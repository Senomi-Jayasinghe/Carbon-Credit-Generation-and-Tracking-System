using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class SignUpController
    {
        public bool IsExistEmail(string Email)
        {   //CHECK IF THE EMAIL EXISTS IN THE SYSTEM.
            SQLConfig sqlConfig = new SQLConfig();
            bool IsExistEmail = false;
            string sql = "Select count(1) as Count from UserLogin where Upper(user_name) = '" + Email.ToUpper()
                + "'";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            IsExistEmail = Convert.ToBoolean(dt.Rows[0]["Count"]);
            return IsExistEmail;
        }

        public void EnterDetails(User user, UserLogin userLogin)
        {   //SIGN UP THE USER
            SQLConfig sQLConfig = new SQLConfig();
            //ENTER TO USER TABLE
            string sqlUser = "Insert into Users(user_firstname, user_lastname, user_fullname, user_email, user_type) " +
                "values ('" + user.userFirstName + "','" + user.userLastName + "','" + user.userFullName + "','" + user.userEmail + "', 1);" +
                "select CAST(scope_identity() as int)";
            int maxID = Convert.ToInt32(sQLConfig.InsertDataWithReturnId(sqlUser));
            //CREATE USER LOGIN
            string sqlLogin = "Insert into UserLogin(user_name, user_password, user_reference_id, user_role_id, user_status, " +
                "last_password_changedate) "
                + "values ('" + userLogin.userName + "','" + userLogin.userPassword + "'," + maxID + "," + 1 + ", 'A', '"
                + userLogin.lastPasswordChangeDate + "')";
            sQLConfig.ExecuteCUD(sqlLogin);
            //CREATE A CARBON CREDIT WALLET UPON SIGNUP
            string sqlCCWallet = "INSERT INTO CarbonCreditMaster (user_id, carbon_creditbalance, last_update_date) " +
                "VALUES (" + maxID + ", 0, CAST(GETDATE() AS DATE))";
            sQLConfig.ExecuteCUD(sqlCCWallet);
        }
    }
}