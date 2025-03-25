using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class BankController
    {
        public DataTable GetBanks()
        {   //GET BANK NAMES FROM DB FOR THE DROP DOWN LIST
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select bank_id, bank_name from Bank";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}