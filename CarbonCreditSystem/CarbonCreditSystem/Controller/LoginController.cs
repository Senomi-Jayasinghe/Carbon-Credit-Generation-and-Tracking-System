using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class LoginController
    {
        public bool SearchUser(UserLogin userLogin)
        {   //CHECK IF A USET WITH CORRECT CREDENTIALS EXIST
            SQLConfig sQLConfig = new SQLConfig();
            bool UserExist = false;
            string sql = "Select count(1) as Count from UserLogin where Upper(user_name) = '" + userLogin.userName.ToUpper()
                + "' AND user_password = '" + userLogin.userPassword + "'";
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            UserExist = Convert.ToBoolean(dt.Rows[0]["Count"]);

            return UserExist;
        }

        public DataTable getUser(UserLogin userLogin)
        {   //GET USER INFORMATION TO SET SESSIONS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select user_role_id from UserLogin where Upper(user_name) = '" + userLogin.userName.ToUpper() + "'";//GET ROLE
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int UserRole = Convert.ToInt32(dt.Rows[0]["user_role_id"]);
            //GET USER DATA
            string sqlgetUser = "Select l.user_reference_id, l.user_role_id, u.user_firstname as Name " +
                             "from Users u Inner Join UserLogin l On Upper(u.user_email) = Upper(l.user_name) " +
                             "where Upper(u.user_email) = '" + userLogin.userName.ToUpper() + "'";

            DataTable dtu = sQLConfig.ExecuteSelect(sqlgetUser);
            //UPDATE STATUS AS ACTIVE
            string sqlUpdateStatus = "Update UserLogin set user_status = 'A' " +
                "where Upper(user_name) = '" + userLogin.userName.ToUpper() + "'";
            sQLConfig.ExecuteCUD(sqlUpdateStatus);

            return dtu;
        }

        public void updateAttempts(UserLogin userLogin)
        {   //UPDATE LOGIN ATTEMPTS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Update UserLogin set user_attempts = " + userLogin.attempts
                + "where Upper(user_name) = '" + userLogin.userName.ToUpper() + "'";
            sQLConfig.ExecuteCUD(sql);
        }

        public int getAttempts(UserLogin userLogin)
        {   //GET THE NUMBER OF LOGIN ATTEMPTS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select user_attempts from UserLogin where Upper(user_name) = '" + userLogin.userName.ToUpper() + "'";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int attempts = Convert.ToInt32(dt.Rows[0]["user_attempts"]);
            return attempts;
        }

        public void endSession(int UserId)
        {   //UPDATE USER STATUS AS UNAVAILABLE (U)
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Update UserLogin set user_status = 'U' where user_reference_id =" + UserId;
            sQLConfig.ExecuteCUD(sql);
        }
    }
}