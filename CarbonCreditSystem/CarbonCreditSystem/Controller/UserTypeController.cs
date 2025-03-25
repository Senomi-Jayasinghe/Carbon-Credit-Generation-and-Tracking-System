using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class UserTypeController
    {
        public DataTable GetUserType()
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select user_type_id, type_description from UserType";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}