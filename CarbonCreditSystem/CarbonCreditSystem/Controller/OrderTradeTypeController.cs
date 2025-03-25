using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class OrderTradeTypeController
    {
        public DataTable GetOrderTradeType()
        {   //GET ORDER TRADE TYPE FOR THE DROP DOWN LISTS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select order_type_id, order_type_description from OrderTradeType";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}