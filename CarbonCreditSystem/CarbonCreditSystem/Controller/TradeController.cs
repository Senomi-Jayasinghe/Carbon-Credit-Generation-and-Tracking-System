using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using CarbonCreditSystem.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.Controller
{
    public class TradeController
    {
        public int placeBuyOrder(BuyOrder buyOrder)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "INSERT INTO BuyOrder (user_id, buy_quantity, minimum_quantity, order_trade_type, maximum_price, buy_status, order_date_time, " +
                "expire_time, balance_quantity, entry_user, entry_date) VALUES (" + buyOrder.userId + ", " + buyOrder.buyQuantity + ", "
                + buyOrder.minimumQuantity + ", " + buyOrder.orderTradeType + ", " + buyOrder.maximumPrice + ", '" + buyOrder.buyStatus + "', '"
                + buyOrder.orderDateTime + "', '" + buyOrder.expireTime + "', " + buyOrder.balanceQuantity + ", " + buyOrder.entryUser + ", '"
                + buyOrder.entryDate + "');" +
                "select CAST(scope_identity() as int)";
            int maxID = Convert.ToInt32(sqlConfig.InsertDataWithReturnId(sql));
            return maxID;
        }

        public void cancelBuyOrder(int orderId)
        {   //CANCEL BUY ORDER
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "UPDATE BuyOrder SET buy_status = 'C' where buy_order_id = " + orderId;
            sQLConfig.ExecuteCUD(sql);
        }

        public void cancelSellOrder(int orderId)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "UPDATE SellOrder SET sell_status = 'C' where sell_order_id = " + orderId;
            sQLConfig.ExecuteCUD(sql);
        }

        public int placeSellOrder(SellOrder sellOrder)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "INSERT INTO SellOrder (user_id, sell_quantity, minimum_quantity, order_trade_type, minimum_price, sell_status, expire_time, " +
                "order_date_time, balance_quantity, entry_user, entry_date) VALUES (" + sellOrder.UserId + ", " + sellOrder.sellQuantity + ", "
                + sellOrder.minimumQuantity + ", " + sellOrder.orderTradeType + ", " + sellOrder.minimumPrice + ", '" + sellOrder.sellStatus + "', '"
                + sellOrder.expireTime + "', '" + sellOrder.orderDateTime + "', " + sellOrder.balanceQuantity + ", " + sellOrder.entryUser + ", '"
                + sellOrder.entryDate + "'); " +
                "select CAST(scope_identity() as int)";
            int maxID = Convert.ToInt32(sqlConfig.InsertDataWithReturnId(sql));
            return maxID;
        }

        public BuyOrder getBuyOrder(int orderId)
        {   //GET ORDER DETAILS BUY ID TO FILL FORM
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT buy_quantity, minimum_quantity, order_trade_type, maximum_price from BuyOrder where buy_order_id = "
                + orderId;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            BuyOrder buyOrder = new BuyOrder();
            foreach (DataRow dr in dt.Rows)
            {
                buyOrder.buyOrderId = orderId;
                buyOrder.buyQuantity = Convert.ToDouble(dr["buy_quantity"]);
                buyOrder.minimumQuantity = Convert.ToDouble(dr["minimum_quantity"]);
                buyOrder.orderTradeType = Convert.ToInt32(dr["order_trade_type"]);
                buyOrder.maximumPrice = Convert.ToDouble(dr["maximum_price"]);
            }
            return buyOrder;
        }

        public SellOrder getSellOrder(int orderId)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT sell_quantity, minimum_quantity, order_trade_type, minimum_price from SellOrder where sell_order_id = "
                + orderId;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            SellOrder sellOrder = new SellOrder();
            foreach (DataRow dr in dt.Rows)
            {
                sellOrder.sellOrderId = orderId;
                sellOrder.sellQuantity = Convert.ToDouble(dr["sell_quantity"]);
                sellOrder.minimumQuantity = Convert.ToDouble(dr["minimum_quantity"]);
                sellOrder.orderTradeType = Convert.ToInt32(dr["order_trade_type"]);
                sellOrder.minimumPrice = Convert.ToDouble(dr["minimum_price"]);
            }
            return sellOrder;
        }

        public DataTable getOrderHistory(int user_id)
        {   //GET ORDER HISTORY FOR TABLE
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT TOP (10) order_id, order_trade_type, price, status, expire_time, order_date_time, balance_quantity, order_type " +
                "FROM ( " +
                "    SELECT sell_order_id AS order_id, order_trade_type, minimum_price AS price, sell_status AS status, " +
                "           FORMAT(expire_time, 'yyyy-MM-dd') AS expire_time, FORMAT(order_date_time, 'yyyy-MM-dd') AS order_date_time, balance_quantity, " +
                "           'Sell Order' AS order_type " +
                "    FROM SellOrder WHERE user_id = " + user_id + " " +
                "    UNION ALL " +
                "    SELECT buy_order_id AS order_id, order_trade_type, maximum_price AS price, buy_status AS status, " +
                "           FORMAT(expire_time, 'yyyy-MM-dd') AS expire_time, FORMAT(order_date_time, 'yyyy-MM-dd') AS order_date_time, balance_quantity, " +
                "           'Buy Order' AS order_type " +
                "    FROM BuyOrder WHERE user_id = " + user_id +
                ") AS CombinedOrders " +
                "ORDER BY order_date_time DESC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable SearchOrderDetails(string status, string type, string FromDate, string ToDate, int user_id)
        {   //SEARCH FUNCTIONALITY
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "WITH Orders AS (SELECT sell_order_id AS order_id, order_trade_type, minimum_price AS price, sell_status AS status, " +
                "FORMAT(expire_time, 'yyyy-MM-dd') AS expire_time, FORMAT(order_date_time, 'yyyy-MM-dd') AS order_date_time, balance_quantity, " +
                "'Sell Order' AS order_type FROM SellOrder " +
                "WHERE(sell_status = '" + status + "' OR '' = '" + status + "') " +
                "AND CAST(order_date_time AS DATE) BETWEEN CAST('" + FromDate + "' AS DATE) " +
                         "AND CAST('" + ToDate + "' AS DATE)  AND user_id = " + user_id
                + " UNION ALL SELECT buy_order_id AS order_id, order_trade_type, maximum_price AS price, buy_status AS status, " +
                "FORMAT(expire_time, 'yyyy-MM-dd') AS expire_time, FORMAT(order_date_time, 'yyyy-MM-dd') AS order_date_time, balance_quantity, " +
                "'Buy Order' AS order_type FROM BuyOrder " +
                "WHERE(buy_status = '" + status + "' OR '' = '" + status + "') " +
                "AND CAST(order_date_time AS DATE) BETWEEN CAST('" + FromDate + "' AS DATE) " +
                         "AND CAST('" + ToDate + "' AS DATE) AND user_id = " + user_id + ") " +
                "SELECT* FROM Orders WHERE(order_type = '" + type + "' OR '' = '" + type + "') ORDER BY order_date_time DESC;";
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable getTradeHistory(int user_id)
        {   //GET LAST 10 TRADE RECORDS FOR TABLE
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT TOP (10) t.price_per_cc, t.total_price, t.quantity, t.excecuted_date_time, CASE WHEN bo.user_id = " + user_id + " " +
                "THEN 'BuyOrder' WHEN so.user_id = " + user_id + " THEN 'SellOrder' END AS source_table FROM Trades t JOIN BuyOrder bo " +
                "ON t.buy_order_id = bo.buy_order_id JOIN SellOrder so ON t.sell_order_id = so.sell_order_id WHERE bo.user_id = " + user_id +
                " OR so.user_id = " + user_id + "ORDER BY t.excecuted_date_time DESC";
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable SearchTradeDetails(string FromDate, string ToDate, int user_id)
        {   //SEARCH FOR TRADE RECORDS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT t.price_per_cc, t.total_price, t.quantity, t.excecuted_date_time, CASE " +
                         "WHEN bo.user_id = " + user_id + " THEN 'BuyOrder' " +
                         "WHEN so.user_id = " + user_id + " THEN 'SellOrder' " +
                         "END AS source_table " +
                         "FROM Trades t " +
                         "LEFT JOIN BuyOrder bo ON t.buy_order_id = bo.buy_order_id " +
                         "LEFT JOIN SellOrder so ON t.sell_order_id = so.sell_order_id " +
                         "WHERE CAST(t.excecuted_date_time AS DATE) BETWEEN CAST('" + FromDate + "' AS DATE) " +
                         "AND CAST('" + ToDate + "' AS DATE) " +
                         "AND (bo.user_id = " + user_id + " OR so.user_id = " + user_id + ") " +
                         "ORDER BY t.excecuted_date_time DESC;";

            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }

        public void expireOrders()
        {   //SET ORDER STATUS AS EXPIRE IF THE ORDER HAS EXCEEDED EXPIRY DATE UPON PAGE LOAD
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "UPDATE BuyOrder SET buy_status = 'X' WHERE expire_time < GETDATE() AND buy_status = 'A'; " +
                "UPDATE SellOrder SET sell_status = 'X' WHERE expire_time < GETDATE() AND sell_status = 'A';";
            sQLConfig.ExecuteCUD(sql);
        }

    }
}