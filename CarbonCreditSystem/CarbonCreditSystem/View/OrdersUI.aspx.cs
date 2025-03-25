using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class OrdersUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TradeController tradeController = new TradeController();
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            tradeController.expireOrders(); //SET STATUS AS EXPIRED IF THE ORDERS ARE EXPIRED ON PAGE LOAD
            DataTable dt = tradeController.getOrderHistory(user_id);

            grdOrders.DataSource = dt;
            grdOrders.DataBind();
        }

        protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                if (lblStatus != null)
                {
                    // Change text and color based on the status
                    switch (status)
                    {
                        case "A":
                            lblStatus.Text = "Placed";
                            lblStatus.ForeColor = System.Drawing.Color.Orange;
                            break;
                        case "E":
                            lblStatus.Text = "Excecuted";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "X":
                            lblStatus.Text = "Expired";
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "C":
                            lblStatus.Text = "Cancelled";
                            lblStatus.ForeColor = System.Drawing.Color.Gray;
                            break;
                        default:
                            lblStatus.Text = "Unknown";
                            lblStatus.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }

                string type = DataBinder.Eval(e.Row.DataItem, "order_trade_type").ToString();
                Label lblTrade = (Label)e.Row.FindControl("lblTrade");

                if (lblTrade != null)
                {
                    // Change text and color based on the status
                    switch (type)
                    {
                        case "1":
                            lblTrade.Text = "Single";
                            break;
                        case "2":
                            lblTrade.Text = "Multiple";
                            break;
                        default:
                            lblTrade.Text = "Unknown";
                            lblTrade.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }

                string order = DataBinder.Eval(e.Row.DataItem, "order_type").ToString();
                Label lblOrder = (Label)e.Row.FindControl("lblOrder");

                if (lblOrder != null)
                {
                    // Change text and color based on the status
                    switch (order)
                    {
                        case "Sell Order":
                            lblOrder.Text = "Sell Order";
                            lblOrder.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "Buy Order":
                            lblOrder.Text = "Buy Order";
                            lblOrder.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            lblOrder.Text = "Unknown";
                            lblOrder.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }

                // Get the cancel button and hide it if the status is "E"
                LinkButton lnkCancel = (LinkButton)e.Row.FindControl("lnkCancel");
                if (lnkCancel != null && status != "A")
                {
                    lnkCancel.Visible = false;
                }
            }
        }

        protected void grdOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string orderType = grdOrders.DataKeys[e.RowIndex]["order_type"].ToString();
            HiddenField field = (HiddenField)grdOrders.Rows[e.RowIndex].FindControl("hdnOrderId");
            int order_id = Convert.ToInt32(field.Value);

            // Check the value of 'order_type' and redirect to the appropriate page
            if (orderType == "Sell Order")
            {
                // Redirect to SellOrderUI.aspx with mode 'c'
                Response.Redirect("SellOrderUI.aspx?orderID=" + order_id + "&mode=C");
            }
            else if (orderType == "Buy Order")
            {
                // Redirect to BuyOrderUI.aspx with mode 'c'
                Response.Redirect("BuyOrderUI.aspx?orderID=" + order_id + "&mode=C");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {   //SEARCH FUNCTIONALITY
            lblrecords.Visible = false;
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            string status = ddlStatus.SelectedValue;
            string type = ddlOrder.SelectedValue;
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
            DataTable dt = tradeController.SearchOrderDetails(status, type, FromDate, ToDate, user_id);
            grdOrders.DataSource = dt;
            grdOrders.DataBind();
        }
    }
}