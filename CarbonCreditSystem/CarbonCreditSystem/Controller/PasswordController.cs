using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace CarbonCreditSystem.Controller
{
    public class PasswordController
    {
        public void update(string NewPassword, string UserName)
        {   //UPDATE PASSWORD WITH RANDOM PASSWORD
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Update UserLogin set user_password = '" + NewPassword + "', last_password_changedate = '" + DateTime.Now
                + "' where Upper(user_name) = '" + UserName.ToUpper() + "'";
            sQLConfig.ExecuteCUD(sql);
        }

        public bool getOldPassword(int UserID, string OldPassword)
        {   //GET OLD PASSWORD
            SQLConfig sQLConfig = new SQLConfig();
            bool CorrectPsw = false;
            string sql = "Select count(1) as Count from UserLogin where user_reference_id = " + UserID
                + "AND user_password = '" + OldPassword + "'";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            CorrectPsw = Convert.ToBoolean(dt.Rows[0]["Count"]);

            return CorrectPsw;
        }

        public void UpdatePassword(string NewPassword, int UserID)
        {   //UPDATE DB WITH NEW PASSWORD
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Update UserLogin set user_password = '" + NewPassword
                + "' where user_reference_id =" + UserID;
            sQLConfig.ExecuteCUD(sql);
        }
    }
}