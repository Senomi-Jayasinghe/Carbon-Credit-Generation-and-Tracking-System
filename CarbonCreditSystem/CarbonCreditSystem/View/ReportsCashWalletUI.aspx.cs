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
    public partial class ReportsCashWalletUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CashWalletController cashWalletController = new CashWalletController();
            lblCashBalance.Text = cashWalletController.getBalance(user_id).ToString();
            getHistory(user_id);
        }

        protected void getHistory(int user_id)
        {
            CashWalletController cashWalletController = new CashWalletController();
            DataTable dt = cashWalletController.getFullHistory(user_id);
            grdCashHistory.DataSource = dt;
            grdCashHistory.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);

            string inputFromDate = txtFrom.Text;
            string inputToDate = txtTo.Text;

            string FromDate, ToDate;

            if (!string.IsNullOrEmpty(inputFromDate)) // If the user provided a 'From' date
            {
                DateTime parsedFromDate = DateTime.Parse(inputFromDate);
                FromDate = parsedFromDate.ToString("yyyy-MM-dd");
            }
            else
            {
                FromDate = "1000-01-01"; // Default start date
            }

            if (!string.IsNullOrEmpty(inputToDate)) // If the user provided a 'To' date
            {
                DateTime parsedToDate = DateTime.Parse(inputToDate);
                ToDate = parsedToDate.ToString("yyyy-MM-dd");
            }
            else
            {
                ToDate = "9999-12-31"; // Default end date
            }

            CashWalletController cashWalletController = new CashWalletController();
            DataTable dt = cashWalletController.getSearchInfo(user_id, FromDate, ToDate);
            grdCashHistory.DataSource = dt;
            grdCashHistory.DataBind();
        }
    }
}