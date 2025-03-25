using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class MatchingAlgorithmController
    {
        public string test()
        {
            string statement = "THIS IS A TEST";
            return statement;
        }

        public string MatchingAlgorithm()
        {
            List<SellOrder> sellOrders = getSellOrders();
            List<BuyOrder> buyOrders = getBuyOrders();
            string statement = "No Trades were Excecuted";
            int sellIndex = 0, buyIndex = 0;
            while (buyIndex < buyOrders.Count && sellIndex < sellOrders.Count)
            {
                BuyOrder buyOrder = buyOrders[buyIndex];
                SellOrder sellOrder = sellOrders[sellIndex];

                buyOrder.buyStatus = checkBuyStatus(buyOrder.buyOrderId);
                sellOrder.sellStatus = checkSellStatus(sellOrder.sellOrderId);

                if (buyOrder.entryDate <= sellOrder.entryDate && buyOrder.buyStatus == "A" && sellOrder.sellStatus == "A")
                {
                    // Check if the sell order price is less than or equal to the buy order's maximum price
                    if (buyOrder.maximumPrice >= sellOrder.minimumPrice && buyOrder.userId != sellOrder.UserId)
                    {
                        Trades trades = new Trades();
                        trades.pricePercc = buyOrder.maximumPrice;
                        trades.sellOrderId = sellOrder.sellOrderId;
                        trades.buyOrderId = buyOrder.buyOrderId;
                        trades.excecutedDateTime = DateTime.Now;
                        trades.quantity = 0;

                        // If the sell order trade type is single
                        if (sellOrder.orderTradeType == 1)
                        {
                            // If the buy order trade type is single and balances are equal
                            if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = sellOrder.balanceQuantity * trades.pricePercc;
                                trades.quantity = sellOrder.balanceQuantity;
                            }
                            // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
                            else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.balanceQuantity && buyOrder.balanceQuantity >= sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = sellOrder.balanceQuantity * trades.pricePercc;
                                trades.quantity = sellOrder.balanceQuantity;
                            }
                            else sellIndex++;
                        }
                        // If the sell order trade type is multiple
                        else if (sellOrder.orderTradeType == 2)
                        {
                            // If the buy order trade type is single and balances are equal
                            if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = buyOrder.minimumQuantity * trades.pricePercc;
                                trades.quantity = buyOrder.minimumQuantity;
                            }
                            // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
                            else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.minimumQuantity)
                            {
                                trades.totalPrice = buyOrder.minimumQuantity * trades.pricePercc;
                                trades.quantity = buyOrder.minimumQuantity;
                            }
                            else sellIndex++;
                        }

                        if (trades.quantity != 0)
                        {
                            int trade_id = SaveTrade(trades);
                            int sell_user = sellOrder.UserId;
                            int buy_user = buyOrder.userId;
                            UpdateCashWalletForSeller(sell_user, trade_id, trades.totalPrice, trades.excecutedDateTime);
                            UpdateCashWalletForBuyer(buy_user, trade_id, trades.totalPrice, trades.excecutedDateTime);

                            UpdateCCWalletForSeller(sell_user, trade_id, trades.quantity, trades.excecutedDateTime);
                            UpdateCCWalletForBuyer(buy_user, trade_id, trades.quantity, trades.excecutedDateTime);
                            statement = "Trade of Carbon Credits" + trades.quantity + "was excecuted at " + DateTime.Now + "";
                        }
                    }
                }
                else if (buyOrder.entryDate >= sellOrder.entryDate && buyOrder.buyStatus == "A" && sellOrder.sellStatus == "A")
                {
                    if (buyOrder.maximumPrice >= sellOrder.minimumPrice && buyOrder.userId != sellOrder.UserId)
                    {
                        Trades trades = new Trades();
                        trades.pricePercc = sellOrder.minimumPrice;
                        trades.sellOrderId = sellOrder.sellOrderId;
                        trades.buyOrderId = buyOrder.buyOrderId;
                        trades.excecutedDateTime = DateTime.Now;
                        trades.quantity = 0;

                        // If the sell order trade type is single
                        if (sellOrder.orderTradeType == 1)
                        {
                            // If the buy order trade type is single and balances are equal
                            if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = sellOrder.balanceQuantity * trades.pricePercc;
                                trades.quantity = sellOrder.balanceQuantity;
                            }
                            // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
                            else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.balanceQuantity && buyOrder.balanceQuantity >= sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = sellOrder.balanceQuantity * trades.pricePercc;
                                trades.quantity = sellOrder.balanceQuantity;
                            }
                            else buyIndex++;
                        }
                        // If the sell order trade type is multiple
                        else if (sellOrder.orderTradeType == 2)
                        {
                            // If the buy order trade type is single and balances are equal
                            if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
                            {
                                trades.totalPrice = buyOrder.minimumQuantity * trades.pricePercc;
                                trades.quantity = buyOrder.minimumQuantity;
                            }
                            // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
                            else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.minimumQuantity)
                            {
                                trades.totalPrice = buyOrder.minimumQuantity * trades.pricePercc;
                                trades.quantity = buyOrder.minimumQuantity;
                            }
                            else buyIndex++;
                        }

                        if (trades.quantity != 0)
                        {
                            int trade_id = SaveTrade(trades);
                            int sell_user = sellOrder.UserId;
                            int buy_user = buyOrder.userId;
                            UpdateCashWalletForSeller(sell_user, trade_id, trades.totalPrice, trades.excecutedDateTime);
                            UpdateCashWalletForBuyer(buy_user, trade_id, trades.totalPrice, trades.excecutedDateTime);

                            UpdateCCWalletForSeller(sell_user, trade_id, trades.quantity, trades.excecutedDateTime);
                            UpdateCCWalletForBuyer(buy_user, trade_id, trades.quantity, trades.excecutedDateTime);
                            statement = "Trade of Carbon Credits" + trades.quantity + "was excecuted at " + DateTime.Now + "";
                        }
                    }
                }
            }
            return statement;
        }

        public List<SellOrder> getSellOrders()
        {   //GET A LIST OF SELL ORDERS TO BE MATCHED WITH THE BUY ORDER
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT sell_order_id, user_id, sell_quantity, minimum_quantity, order_trade_type, minimum_price, sell_status, " +
                "order_date_time, balance_quantity FROM SellOrder WHERE sell_status = 'A' ORDER BY order_date_time ASC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            List<SellOrder> sellOrders = new List<SellOrder>();
            foreach (DataRow dr in dt.Rows)
            {
                SellOrder sellOrder = new SellOrder();
                sellOrder.sellOrderId = Convert.ToInt32(dr["sell_order_id"]);
                sellOrder.UserId = Convert.ToInt32(dr["user_id"]);
                sellOrder.sellQuantity = Convert.ToDouble(dr["sell_quantity"]);
                sellOrder.minimumQuantity = Convert.ToDouble(dr["minimum_quantity"]);
                sellOrder.orderTradeType = Convert.ToInt32(dr["order_trade_type"]);
                sellOrder.minimumPrice = Convert.ToDouble(dr["minimum_price"]);
                sellOrder.sellStatus = dr["sell_status"].ToString();
                sellOrder.balanceQuantity = Convert.ToDouble(dr["balance_quantity"]);

                sellOrders.Add(sellOrder);
            }

            return sellOrders;
        }

        public double getSellOrderBalance(int order_id)
        {
            // GET THE REMAINING BALANCE TO BE TRADED
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT balance_quantity FROM SellOrder WHERE sell_order_id = " + order_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            double balance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                balance = Convert.ToDouble(dr["balance_quantity"]);
            }
            return balance;
        }

        public List<BuyOrder> getBuyOrders()
        {   //GET A LIST OF BUY ORDERS TO BE MATCHED WITH THE SELL ORDER
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT buy_order_id, user_id, buy_quantity, minimum_quantity, order_trade_type, maximum_price, buy_status, " +
                         "order_date_time, balance_quantity FROM BuyOrder WHERE buy_status = 'A' ORDER BY order_date_time ASC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);

            List<BuyOrder> buyOrders = new List<BuyOrder>(); // List to hold buy orders

            foreach (DataRow dr in dt.Rows)
            {
                BuyOrder buyOrder = new BuyOrder();
                buyOrder.buyOrderId = Convert.ToInt32(dr["buy_order_id"]);
                buyOrder.userId = Convert.ToInt32(dr["user_id"]);
                buyOrder.buyQuantity = Convert.ToDouble(dr["buy_quantity"]);
                buyOrder.minimumQuantity = Convert.ToDouble(dr["minimum_quantity"]);
                buyOrder.orderTradeType = Convert.ToInt32(dr["order_trade_type"]);
                buyOrder.maximumPrice = Convert.ToDouble(dr["maximum_price"]);
                buyOrder.buyStatus = dr["buy_status"].ToString();
                buyOrder.balanceQuantity = Convert.ToDouble(dr["balance_quantity"]);

                // Add each buyOrder to the list
                buyOrders.Add(buyOrder);
            }

            return buyOrders;
        }

        public double getBuyOrderBalance(int order_id)
        {   // GET THE REMAINING BALANCE TO BE TRADED
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT balance_quantity FROM BuyOrder WHERE buy_order_id = " + order_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            double balance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                balance = Convert.ToDouble(dr["balance_quantity"]);
            }
            return balance;
        }

        public string checkSellStatus(int sell_order_id)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT sell_status FROM SellOrder WHERE sell_order_id = " + sell_order_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            string status = "X";
            foreach (DataRow dr in dt.Rows)
            {
                status = (dr["sell_status"]).ToString();
            }

            return status;
        }

        public string checkBuyStatus(int buy_order_id)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT buy_status FROM BuyOrder WHERE buy_order_id = " + buy_order_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            string status = "X";
            foreach (DataRow dr in dt.Rows)
            {
                status = (dr["buy_status"]).ToString();
            }

            return status;
        }

        public int SaveTrade(Trades trades)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "INSERT INTO Trades (buy_order_id, sell_order_id, price_per_cc, total_price, quantity, excecuted_date_time) " +
                "VALUES (" + trades.buyOrderId + ", " + trades.sellOrderId + ", " + trades.pricePercc + ", " + trades.totalPrice + ", "
                + trades.quantity + ", '" + trades.excecutedDateTime + "'); " +
                "select CAST(scope_identity() as int)";
            int maxID = Convert.ToInt32(sqlConfig.InsertDataWithReturnId(sql));

            string sqlSellOrder = "UPDATE SellOrder SET last_sell_date_time = '" + trades.excecutedDateTime + "', " +
                "balance_quantity = balance_quantity - " + trades.quantity + ", sell_status = " +
                "CASE WHEN balance_quantity - " + trades.quantity + " = 0 THEN 'E' ELSE sell_status END " +
                "WHERE sell_order_id = " + trades.sellOrderId;

            sqlConfig.ExecuteCUD(sqlSellOrder);

            string sqlBuyOrder = "UPDATE BuyOrder SET last_buy_date_time = '" + trades.excecutedDateTime + "', " +
                "balance_quantity = balance_quantity - " + trades.quantity + ", buy_status = " +
                "CASE WHEN balance_quantity - " + trades.quantity + " = 0 THEN 'E' ELSE buy_status END " +
                "WHERE buy_order_id = " + trades.buyOrderId;

            sqlConfig.ExecuteCUD(sqlBuyOrder);

            return maxID;
        }

        public void UpdateCashWalletForSeller(int user_id, int trade_id, double amount, DateTime date)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sqlHistory = "INSERT INTO CashWalletHistory (user_id, previous_balance, cash_update_type, update_balance, update_date, " +
                "reference_id, entry_user, entry_date) VALUES (" + user_id + ", " +
                "(SELECT cash_balance FROM CashWallet WHERE user_id = " + user_id + ") ,3 ," + amount + " ,'" + date + "' , " + trade_id + ","
                + user_id + " ,'" + date + "');";
            sQLConfig.ExecuteCUD(sqlHistory);

            string sqlUpdate = "UPDATE CashWallet SET cash_balance = cash_balance + " + amount + ", last_update_date = '" + date + "' " +
                "WHERE user_id = " + user_id;
            sQLConfig.ExecuteCUD(sqlUpdate);
        }

        public void UpdateCashWalletForBuyer(int user_id, int trade_id, double amount, DateTime date)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sqlHistory = "INSERT INTO CashWalletHistory (user_id, previous_balance, cash_update_type, update_balance, update_date, " +
                "reference_id, entry_user, entry_date) VALUES (" + user_id + ", " +
                "(SELECT cash_balance FROM CashWallet WHERE user_id = " + user_id + ") ,2 ," + amount + " ,'" + date + "' ," + trade_id + " ,"
                + user_id + " ,'" + date + "');";
            sQLConfig.ExecuteCUD(sqlHistory);

            string sqlUpdate = "UPDATE CashWallet SET cash_balance = cash_balance - " + amount + ", last_update_date = '" + date + "' " +
                "WHERE user_id = " + user_id;
            sQLConfig.ExecuteCUD(sqlUpdate);
        }

        public void UpdateCCWalletForSeller(int user_id, int trade_id, double amount, DateTime date)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sqlHistory = "INSERT INTO CarbonCreditMasterHistory(user_id, previous_cc_balance, cc_update_type, update_quantity, " +
                "update_date, reference_id, entry_user, entry_date) VALUES (" + user_id + " ," +
                "(SELECT carbon_creditbalance FROM CarbonCreditMaster WHERE user_id = " + user_id + ") ,3 ," + amount + " ,'" + date + "' ,"
                + trade_id + " ," + user_id + " , '" + date + "')";
            sQLConfig.ExecuteCUD(sqlHistory);

            string sqlUpdate = "UPDATE CarbonCreditMaster SET carbon_creditbalance = carbon_creditbalance - " + amount + " ," +
                "last_update_date = '" + date + "' WHERE user_id = " + user_id;
            sQLConfig.ExecuteCUD(sqlUpdate);
        }

        public void UpdateCCWalletForBuyer(int user_id, int trade_id, double amount, DateTime date)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sqlHistory = "INSERT INTO CarbonCreditMasterHistory(user_id, previous_cc_balance, cc_update_type, update_quantity, " +
                "update_date, reference_id, entry_user, entry_date) VALUES (" + user_id + " ," +
                "(SELECT carbon_creditbalance FROM CarbonCreditMaster WHERE user_id = " + user_id + ") ,2 ," + amount + " ,'" + date + "' ,"
                + trade_id + " ," + user_id + " , '" + date + "')";
            sQLConfig.ExecuteCUD(sqlHistory);

            string sqlUpdate = "UPDATE CarbonCreditMaster SET carbon_creditbalance = carbon_creditbalance + " + amount + " ," +
                "last_update_date = '" + date + "' WHERE user_id = " + user_id;
            sQLConfig.ExecuteCUD(sqlUpdate);
        }
    }
}