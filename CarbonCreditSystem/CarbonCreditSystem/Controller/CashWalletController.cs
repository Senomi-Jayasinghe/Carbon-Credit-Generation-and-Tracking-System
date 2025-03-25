using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using CarbonCreditSystem.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class CashWalletController
    {
        public void registerBankAccount(CashWallet cashWallet)
        {   //REGISTER BANK ACCOUNT AND CREATE A CASH WALLET FOR USER
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "INSERT into CashWallet (user_id, cash_balance, last_update_date, bank_account_no, bank_branch, bank_id) " +
                "VALUES (" + cashWallet.userId + " ,0 ,'" + cashWallet.lastUpdateDate + "' ," + cashWallet.bankAccountNo + " ,'"
                + cashWallet.bankBranch + "' ," + cashWallet.bankId + ")";
            sQLConfig.ExecuteCUD(sql);
        }

        public DataTable getBankData(int user_id)
        {   //GET BANK DETAILS
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT cw.bank_account_no, cw.bank_branch, b.bank_name FROM CashWallet cw JOIN Bank b ON cw.bank_id = b.bank_id " +
                "WHERE cw.user_id = " + user_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }

        public double getBalance(int user_id)
        {   //GET CASH WALLET BALANCE
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT cash_balance FROM CashWallet WHERE user_id = " + user_id;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            double balance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                balance = Convert.ToDouble(dr["cash_balance"]);
            }
            return balance;
        }

        public double getAvailableBalance(int user_id)
        {   //GET AVAILABLE BALANCE
            SQLConfig Sqlconfig = new SQLConfig();
            string sql = "SELECT cw.cash_balance - COALESCE(SUM(bo.maximum_price * bo.buy_quantity), 0) AS available_balance " +
             "FROM CashWallet cw " +
             "LEFT JOIN BuyOrder bo ON cw.user_id = bo.user_id AND bo.buy_status = 'A' " +
             "WHERE cw.user_id = " + user_id + " " +
             "GROUP BY cw.cash_balance";
            DataTable dt = Sqlconfig.ExecuteSelect(sql);
            double availableBalance = 0;
            foreach (DataRow dr in dt.Rows)
            {
                availableBalance = Convert.ToDouble(dr["available_balance"]);
            }
            return availableBalance;
        }

        public void topUp(int user_id, double amount, DateTime date)
        {   //TOP UP CASH
            SQLConfig sqlConfig = new SQLConfig();
            string sqlInsert = "INSERT INTO CashWalletHistory (user_id, previous_balance, cash_update_type, update_balance, update_date, " +
                "entry_user, entry_date) VALUES (" + user_id + ", (SELECT cash_balance FROM CashWallet WHERE user_id = " + user_id + ") ,1 ," +
                "" + amount + " ,'" + date + "' ," + user_id + " ,'" + date + "')";
            sqlConfig.ExecuteCUD(sqlInsert);
            string sqlUpdate = "UPDATE CashWallet SET cash_balance = cash_balance + " + amount + ", last_update_date = '" + date + "' " +
                "WHERE user_id = " + user_id;
            sqlConfig.ExecuteCUD(sqlUpdate);
        }

        public void withdraw(int user_id, double amount, DateTime date)
        {   //WITHDRAW CASH
            SQLConfig sqlConfig = new SQLConfig();
            string sqlInsert = "INSERT INTO CashWalletHistory (user_id, previous_balance, cash_update_type, update_balance, update_date, " +
                "entry_user, entry_date) VALUES (" + user_id + ", (SELECT cash_balance FROM CashWallet WHERE user_id = " + user_id + ") ,4 ," +
                "" + amount + " ,'" + date + "' ," + user_id + " ,'" + date + "')";
            sqlConfig.ExecuteCUD(sqlInsert);

            string sqlUpdate = "UPDATE CashWallet SET cash_balance = cash_balance - " + amount + ", last_update_date = '" + date + "' " +
                "WHERE user_id = " + user_id;
            sqlConfig.ExecuteCUD(sqlUpdate);
        }

        public DataTable getHistory(int user_id)
        {   //GET LAST 10 TRANSACTIONS FOR TABLE
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT TOP (10) previous_balance, cash_update_type, update_balance, FORMAT(update_date, 'yyyy-MM-dd') " +
                "AS update_date " +
                "FROM CashWalletHistory WHERE user_id = " + user_id + " ORDER BY update_date DESC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable getFullHistory(int user_id)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT previous_balance, cash_update_type, update_balance, FORMAT(update_date, 'yyyy-MM-dd') " +
                "AS update_date " +
                "FROM CashWalletHistory WHERE user_id = " + user_id + " ORDER BY update_date DESC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable getSearchInfo(int user_id, string FromDate, string ToDate)
        {
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "SELECT previous_balance, cash_update_type, update_balance, FORMAT(update_date, 'yyyy-MM-dd') AS update_date " +
                "FROM CashWalletHistory WHERE CAST(update_date AS DATE) BETWEEN CAST('" + FromDate + "' AS DATE) AND CAST('" + ToDate + "' AS DATE) " +
                "AND user_id = " + user_id + " ORDER BY update_date DESC";
            DataTable dt = sqlConfig.ExecuteSelect(sql);
            return dt;
        }
    }
}