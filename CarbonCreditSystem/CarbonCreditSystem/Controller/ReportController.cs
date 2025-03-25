using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using DotNet.Highcharts.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarbonCreditSystem.Controller
{
    public class ReportController
    {
        public object[] getPriceWatchDailyPrices()
        {   // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT price_per_cc, excecuted_date_time " +
                         "FROM Trades  WHERE CONVERT(VARCHAR(10), excecuted_date_time, 101) = CONVERT(VARCHAR(10), GETDATE(), 101) " +
                         "ORDER BY excecuted_date_time ASC";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int i = 0;
            object[] dailyPrices = new object[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                double pricePerCc = Convert.ToDouble(dr["price_per_cc"]);
                dailyPrices[i] = pricePerCc;
                i++;
            }
            return dailyPrices;
        }

        public string[] getPriceWatchDailyDates()
        {
            // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT price_per_cc, FORMAT(excecuted_date_time, 'HH:mm:ss') AS excecuted_date_time " +
                         "FROM Trades  WHERE CONVERT(VARCHAR(10), excecuted_date_time, 101) = CONVERT(VARCHAR(10), GETDATE(), 101) " +
                         "ORDER BY excecuted_date_time ASC";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int i = 0;
            string[] dailyDates = new string[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                string dates = dr["excecuted_date_time"].ToString();
                dailyDates[i] = dates;
                i++;
            }
            return dailyDates;
        }

        public object[] getPriceWatchWeeklyPrices()
        {
            // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT FORMAT(excecuted_date_time, 'MM/dd') AS excecuted_date_time, AVG(price_per_cc) AS avg_price_per_cc " +
             "FROM Trades " +
             "WHERE excecuted_date_time >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) " +
             "AND excecuted_date_time <= GETDATE() " +
             "GROUP BY FORMAT(excecuted_date_time, 'MM/dd') " +
             "ORDER BY excecuted_date_time ASC;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int i = 0;
            object[] dailyPrices = new object[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                double pricePerCc = Convert.ToDouble(dr["avg_price_per_cc"]);
                dailyPrices[i] = pricePerCc;
                i++;
            }
            return dailyPrices;
        }

        public string[] getPriceWatchWeeklyDates()
        {
            // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT FORMAT(excecuted_date_time, 'MM/dd') AS excecuted_date_time, AVG(price_per_cc) AS avg_price_per_cc " +
             "FROM Trades " +
             "WHERE excecuted_date_time >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE)) " +
             "AND excecuted_date_time <= GETDATE() " +
             "GROUP BY FORMAT(excecuted_date_time, 'MM/dd') " +
             "ORDER BY excecuted_date_time ASC;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            int i = 0;
            string[] dailyDates = new string[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                string dates = dr["excecuted_date_time"].ToString();
                dailyDates[i] = dates;
                i++;
            }
            return dailyDates;
        }

        public object[] getPriceWatchMonthlyPrices()
        {    // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT FORMAT(CAST(excecuted_date_time AS DATE), 'MM/dd') AS TradeDate, AVG(price_per_cc) AS AveragePrice FROM Trades " +
                "WHERE excecuted_date_time >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) " +
                "AND excecuted_date_time<DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) +1, 0) GROUP BY FORMAT(CAST(excecuted_date_time AS DATE), 'MM/dd') " +
                "ORDER BY TradeDate;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int i = 0;
            object[] dailyPrices = new object[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                double pricePerCc = Convert.ToDouble(dr["AveragePrice"]);
                dailyPrices[i] = pricePerCc;
                i++;
            }
            return dailyPrices;
        }

        public string[] getPriceWatchMonthlyDates()
        {   // Fetch trades ordered by date ASCENDING
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT FORMAT(CAST(excecuted_date_time AS DATE), 'MM/dd') AS TradeDate, AVG(price_per_cc) AS AveragePrice FROM Trades " +
                "WHERE excecuted_date_time >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) " +
                "AND excecuted_date_time<DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) +1, 0) GROUP BY FORMAT(CAST(excecuted_date_time AS DATE), 'MM/dd') " +
                "ORDER BY TradeDate;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            int i = 0;
            string[] dailyDates = new string[dt.Rows.Count];
            foreach (DataRow dr in dt.Rows)
            {
                string dates = dr["TradeDate"].ToString();
                dailyDates[i] = dates;
                i++;
            }
            return dailyDates;
        }

        public object[] getMonthlyBuyOrderCounts(int user_id)
        {
            int[] monthlyCounts = new int[12];
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT MONTH(order_date_time) AS Month, COUNT(*) AS BuyOrderCount FROM BuyOrder WHERE user_id = " + user_id
                + " GROUP BY MONTH(order_date_time) ORDER BY Month;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            // Fill the monthlyCounts array with results
            foreach (DataRow row in dt.Rows)
            {
                int month = Convert.ToInt32(row["Month"]);
                int count = Convert.ToInt32(row["BuyOrderCount"]);
                monthlyCounts[month - 1] = count; // Adjust for zero-based index
            }

            return monthlyCounts.Cast<object>().ToArray();
        }

        public object[] getMonthlySellOrderCounts(int user_id)
        {
            int[] monthlyCounts = new int[12];
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT MONTH(order_date_time) AS Month, COUNT(*) AS SellOrderCount FROM SellOrder WHERE user_id = " + user_id
                + " GROUP BY MONTH(order_date_time) ORDER BY Month;";
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            // Fill the monthlyCounts array with results
            foreach (DataRow row in dt.Rows)
            {
                int month = Convert.ToInt32(row["Month"]);
                int count = Convert.ToInt32(row["SellOrderCount"]);
                monthlyCounts[month - 1] = count; // Adjust for zero-based index
            }

            return monthlyCounts.Cast<object>().ToArray();
        }

        public string getSellOrders(int user_id)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT COUNT(sell_order_id) AS SellOrderCount FROM SellOrder WHERE user_id = " + user_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            string orders = dt.Rows[0]["SellOrderCount"].ToString();
            return orders;
        }

        public string getBuyOrders(int user_id)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT COUNT(buy_order_id) AS BuyOrderCount FROM BuyOrder WHERE user_id = " + user_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);

            string orders = dt.Rows[0]["BuyOrderCount"].ToString();
            return orders;
        }
    }
}
