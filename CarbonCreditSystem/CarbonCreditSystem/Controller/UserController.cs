using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Lifetime;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class UserController
    {
        public void SaveUser(User user)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "insert into Users(title_id, user_firstname, user_lastname, user_fullname, user_address, " +
                "user_telephoneno, user_mobileno, user_email, user_nic, user_type) " +
                "values (" + user.titleId + ", '" + user.userFirstName + "', '" + user.userLastName + "', '"
                + user.userFullName + "', '" + user.userAddress + "', '" + user.userTelephoneNo + "', '"
                + user.userMobileNo + "', '" + user.userEmail + "', " + user.userNIC + ", " + user.userType + ")";

            sQLConfig.ExecuteCUD(sql);
        }

        public void SaveAuthorizer(User user, string password)
        {   //SAVE AUTHORIZER
            SQLConfig sQLConfig = new SQLConfig(); //SAVE TO USER TABLE
            string sql = "insert into Users(title_id, user_firstname, user_lastname, user_fullname, user_address, " +
                "user_telephoneno, user_mobileno, user_email, user_nic, user_type, entry_user, entry_date) " +
                "values (" + user.titleId + ", '" + user.userFirstName + "', '" + user.userLastName + "', '"
                + user.userFullName + "', '" + user.userAddress + "', '" + user.userTelephoneNo + "', '"
                + user.userMobileNo + "', '" + user.userEmail + "', " + user.userNIC + ", " + user.userType + ", " + user.entryUser + ", " +
                "'" + user.entryDate + "'); " +
                "select CAST(scope_identity() as int)";
            int maxID = Convert.ToInt32(sQLConfig.InsertDataWithReturnId(sql));
            //INSERT INTO LOGIN TABLE AND CREATE A LOGIN
            string sqlLogin = "Insert into UserLogin(user_name, user_password, user_reference_id, user_role_id, user_status, user_attempts, " +
                "last_password_changedate) "
                + "values ('" + user.userEmail + "','" + password + "'," + maxID + "," + 2 + ", 'A', 0,'"
                + DateTime.Now + "')";
            sQLConfig.ExecuteCUD(sqlLogin);
        }

        public void UpdateUser(User user)
        {   //UPDATE USER DATA
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Update Users set title_id = " + user.titleId + ", user_firstname = '" + user.userFirstName + "', " +
                "user_lastname = '" + user.userLastName + "', user_fullname = '" + user.userFullName + "', " +
                "user_address = '" + user.userAddress + "', user_telephoneno = '" + user.userTelephoneNo + "', " +
                "user_mobileno = '" + user.userMobileNo + "', user_email = '" + user.userEmail + "', user_nic = " + user.userNIC + " " +
                "where user_id=" + user.userId;
            sQLConfig.ExecuteCUD(sql);
        }

        public void DeleteUser(int userid)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "DELETE FROM UserLogin WHERE user_reference_id = " + userid + "; DELETE FROM Users WHERE user_id = " + userid;
            sqlConfig.ExecuteCUD(sql);
        }

        public User getUserByID(int userId)
        {   //GET USER FOR UPDATE AND DELETE MODES
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select user_id, title_id, user_firstname, user_lastname, user_fullname, user_address, user_telephoneno, " +
                "user_mobileno, user_email, user_nic, user_type from Users where user_id =" + userId;

            DataTable dt = sQLConfig.ExecuteSelect(sql);
            User user = new User();

            foreach (DataRow dr in dt.Rows)
            {
                user.userId = userId;
                user.titleId = dr["title_id"] != DBNull.Value ? Convert.ToInt32(dr["title_id"].ToString()) : 0;
                user.userFirstName = dr["user_firstname"] != DBNull.Value ? dr["user_firstname"].ToString() : string.Empty;
                user.userLastName = dr["user_lastname"] != DBNull.Value ? dr["user_lastname"].ToString() : string.Empty;
                user.userFullName = dr["user_fullname"] != DBNull.Value ? dr["user_fullname"].ToString() : string.Empty;
                user.userAddress = dr["user_address"] != DBNull.Value ? dr["user_address"].ToString() : string.Empty;
                user.userTelephoneNo = dr["user_telephoneno"] != DBNull.Value ? dr["user_telephoneno"].ToString() : string.Empty;
                user.userMobileNo = dr["user_mobileno"] != DBNull.Value ? dr["user_mobileno"].ToString() : string.Empty;
                user.userEmail = dr["user_email"] != DBNull.Value ? dr["user_email"].ToString() : string.Empty;
                user.userNIC = dr["user_nic"] != DBNull.Value ? Convert.ToInt64(dr["user_nic"]) : 0;
                user.userType = dr["user_type"] != DBNull.Value ? Convert.ToInt32(dr["user_type"]) : 0;
            }

            return user;
        }

        public DataTable getAuthorizers()
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT user_id, user_fullname, user_mobileno, user_telephoneno, user_email, user_nic FROM Users " +
                "WHERE user_type = 2";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}