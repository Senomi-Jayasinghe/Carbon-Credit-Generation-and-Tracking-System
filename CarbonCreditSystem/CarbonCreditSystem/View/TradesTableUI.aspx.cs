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
    public partial class TradesTableUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            TradeController tradeController = new TradeController();
            DataTable dt = tradeController.getTradeHistory(user_id);//GET LAST 10 TRADE RECORDS
            grdTrades.DataSource = dt;
            grdTrades.DataBind();
        }

        protected void grdTrades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string source = DataBinder.Eval(e.Row.DataItem, "source_table").ToString();
                Label lblSource = (Label)e.Row.FindControl("lblSource");
                if (lblSource != null)
                {
                    // Change text and color based on the status
                    switch (source)
                    {
                        case "SellOrder":
                            lblSource.Text = "Sell Order";
                            lblSource.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "BuyOrder":
                            lblSource.Text = "Buy Order";
                            lblSource.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            lblSource.Text = "Unknown";
                            lblSource.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {   //SEARCH FUNCTIONALITY
            lblrecords.Visible = false;
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

            TradeController tradeController = new TradeController();
            DataTable dt = tradeController.SearchTradeDetails(FromDate, ToDate, user_id);
            grdTrades.DataSource = dt;
            grdTrades.DataBind();
        }
    }
}