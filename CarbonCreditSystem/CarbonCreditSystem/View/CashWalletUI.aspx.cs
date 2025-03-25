using CarbonCreditSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class CashWalletUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CashWalletController cashWalletController = new CashWalletController();
            lblCashBalance.Text = cashWalletController.getBalance(user_id).ToString(); //GET WALLET BALANCE
            getBankDetails(user_id);
            getHistory(user_id);
        }

        protected void getHistory(int user_id)
        {   //GET LAST 10 TRANSACTIONS FOR TABLE
            CashWalletController cashWalletController = new CashWalletController();
            DataTable dt = cashWalletController.getHistory(user_id);
            grdCashHistory.DataSource = dt;
            grdCashHistory.DataBind();
        }

        protected void getBankDetails(int user_id)
        {   //GET BANK DETAILS
            CashWalletController cashWalletController = new CashWalletController();
            DataTable dt = cashWalletController.getBankData(user_id);
            foreach (DataRow dr in dt.Rows)
            {
                txtAccNoT.Text = dr["bank_account_no"].ToString();
                txtBranchT.Text = dr["bank_branch"].ToString();
                txtNameT.Text = dr["bank_name"].ToString();
                txtAccNoW.Text = dr["bank_account_no"].ToString();
                txtBranchW.Text = dr["bank_branch"].ToString();
                txtNameW.Text = dr["bank_name"].ToString();
            }
        }

        protected void btnTopUp_Click(object sender, EventArgs e)
        {   //TOP UP CASH
            CashWalletController cashWalletController = new CashWalletController();
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            DateTime entrydate = DateTime.Now;
            double amount = Convert.ToDouble(txtAmountT.Text);
            cashWalletController.topUp(user_id, amount, entrydate);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnWithdraw_Click(object sender, EventArgs e)
        {   //WITHDRAW CASH
            CashWalletController cashWalletController = new CashWalletController();
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            DateTime entrydate = DateTime.Now;
            double amount = Convert.ToDouble(txtAmountW.Text);
            cashWalletController.withdraw(user_id, amount, entrydate);
            Response.Redirect(Request.RawUrl);
        }

        protected void grdCashHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the Type value from the current row
                string type = DataBinder.Eval(e.Row.DataItem, "cash_update_type").ToString();
                Label lblType = (Label)e.Row.FindControl("lblType");

                if (lblType != null)
                {
                    // Change text and color based on the status
                    switch (type)
                    {
                        case "1":
                            lblType.Text = "Top-up";
                            lblType.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "2":
                            lblType.Text = "Buy";
                            lblType.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "3":
                            lblType.Text = "Sell";
                            lblType.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "4":
                            lblType.Text = "Withdraw";
                            lblType.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            lblType.Text = "Unknown";
                            lblType.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }
            }
        }
    }
}