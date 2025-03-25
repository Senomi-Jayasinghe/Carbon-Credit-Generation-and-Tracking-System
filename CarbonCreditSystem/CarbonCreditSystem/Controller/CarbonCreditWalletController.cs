using CarbonCreditSystem.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class CarbonCreditWalletController
    {
        public double getBalance(int user_id)
        {   //GET CARBON CERDIT WALLET BALANCE
            SQLConfig Sqlconfig = new SQLConfig();
            string sql = "SELECT carbon_creditbalance FROM CarbonCreditMaster WHERE user_id = " + user_id;
            DataTable dt = Sqlconfig.ExecuteSelect(sql);
            double balance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                balance = Convert.ToDouble(dr["carbon_creditbalance"]);
            }
            return balance;
        }

        public double getAvailableBalance(int user_id)
        {   //GET AVAILABLE BALANCE OF CARBON CREDITS FOR TRADE
            SQLConfig Sqlconfig = new SQLConfig();
            string sql = "SELECT ccm.carbon_creditbalance - COALESCE(SUM(so.sell_quantity), 0) AS available_balance " +
                "FROM CarbonCreditMaster ccm LEFT JOIN SellOrder so ON ccm.user_id = so.user_id AND so.sell_status = 'A' " +
                "WHERE ccm.user_id = " + user_id + " GROUP BY ccm.carbon_creditbalance";
            DataTable dt = Sqlconfig.ExecuteSelect(sql);
            double availableBalance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                availableBalance = Convert.ToDouble(dr["available_balance"]);
            }
            return availableBalance;
        }

        public DataTable GetHistory(int user_id)
        {   //GET LAST 10 CARBON CREDIT TRANSACTION RECORDS
            SQLConfig Sqlconfig = new SQLConfig();
            string sql = "SELECT TOP (10) previous_cc_balance, cc_update_type, update_quantity, " +
                "FORMAT(update_date, 'yyyy-MM-dd') AS update_date FROM CarbonCreditMasterHistory WHERE user_id = " + user_id + " " +
                "ORDER BY update_date DESC";
            DataTable dt = Sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable GetFullHistory(int user_id)
        {
            SQLConfig Sqlconfig = new SQLConfig();
            string sql = "SELECT previous_cc_balance, cc_update_type, update_quantity, " +
                "FORMAT(update_date, 'yyyy-MM-dd') AS update_date FROM CarbonCreditMasterHistory WHERE user_id = " + user_id + " " +
                "ORDER BY update_date DESC";
            DataTable dt = Sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable getSearchInfo(int user_id, string FromDate, string ToDate)
        {
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT previous_cc_balance, cc_update_type, update_quantity, FORMAT(update_date, 'yyyy-MM-dd') AS update_date " +
                "FROM CarbonCreditMasterHistory WHERE (entry_date BETWEEN '" + FromDate + "' AND '"
                + ToDate + "') AND user_id = " + user_id + " ORDER BY entry_date DESC";
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }
    }
}