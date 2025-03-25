using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class TitleController
    {
        public DataTable GetTitle()
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select title_id, title_description from Title";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}