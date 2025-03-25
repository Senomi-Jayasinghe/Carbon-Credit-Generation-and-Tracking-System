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
    public partial class SellOrderUI : System.Web.UI.Page
    {
        string mode = "X";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getOrderTradeTypeData();
                ViewState["Mode"] = mode;
                if (Request.QueryString["orderID"] != null)
                {
                    int orderID;
                    if (int.TryParse(Request.QueryString["orderID"], out orderID))
                    {
                        getOrder(orderID);

                        if (Request.QueryString["mode"] == "C")
                        {
                            cancelOrder();
                            mode = "C";
                            ViewState["Mode"] = mode;
                        }
                    }
                }
            }
        }

        protected void getOrder(int orderID)
        {
            TradeController tradeController = new TradeController();
            SellOrder sellOrder = tradeController.getSellOrder(orderID);

            hdnOrderId.Value = orderID.ToString();
            txtSellQuantity.Text = sellOrder.sellQuantity.ToString();
            txtMinQuantity.Text = sellOrder.minimumQuantity.ToString();
            txtMinPrice.Text = sellOrder.minimumPrice.ToString();
            ddlOrderType.SelectedIndex = sellOrder.orderTradeType;
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CarbonCreditWalletController carbonCreditWalletController = new CarbonCreditWalletController();
            double balance = carbonCreditWalletController.getAvailableBalance(user_id); //GET AVAILABLE CARBON CREDITS FOR TRADES
            double amount = Convert.ToDouble(txtSellQuantity.Text);
            mode = ViewState["Mode"].ToString();
            if (amount > balance && mode != "C")
            {   //CHECK IF THE AMOUNT ENTERED EXCEEDS THE AVAILABLE BALANCE
                lblMsg.Text = "Amount Exceeding Balance, Your Balance Available is " + balance;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
                double minQty = Convert.ToDouble(txtMinQuantity.Text);
                if (amount % minQty != 0)
                {   //ORDER QTY MUST BE DIVISIBLE BY MINIMUM QTY
                    lblMsg.Text = $"Minimum Quantity should be a divisor of Order Quantity. " +
                                   $"For the Order Quantity of {amount}, please enter a Minimum Quantity that divides it evenly.";
                    lblMsg.Visible = true;
                }
                else if (ddlOrderType.SelectedIndex == 1)
                {
                    txtMinQuantity.Text = txtSellQuantity.Text;
                }
            }

            SellOrder sellOrder = new SellOrder();
            sellOrder.UserId = Convert.ToInt32(Session["USER_ID"]);
            sellOrder.sellQuantity = Convert.ToDouble(txtSellQuantity.Text);
            sellOrder.minimumQuantity = Convert.ToDouble(txtMinQuantity.Text);
            sellOrder.orderTradeType = ddlOrderType.SelectedIndex;
            sellOrder.minimumPrice = Convert.ToDouble(txtMinPrice.Text);
            sellOrder.sellStatus = "A";
            sellOrder.expireTime = DateTime.Now.AddYears(1);
            sellOrder.orderDateTime = DateTime.Now;
            sellOrder.balanceQuantity = Convert.ToDouble(txtSellQuantity.Text);
            sellOrder.entryUser = Convert.ToInt32(Session["USER_ID"]);
            sellOrder.entryDate = DateTime.Now;

            TradeController tradeController = new TradeController();

            if (lblMsg.Visible == false && btnPlaceOrder.Text == "Place Order")
            {
                sellOrder.sellOrderId = tradeController.placeSellOrder(sellOrder);
                FindMatchingTrade(sellOrder);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Sell Order Placed Successfuly', " +
                        "showConfirmButton: true});", true);
            }
            else if (lblMsg.Visible == false && btnPlaceOrder.Text == "Cancel")
            {
                int orderId = Convert.ToInt32(hdnOrderId.Value);
                tradeController.cancelSellOrder(orderId);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Sell Order Cancelled Successfuly', " +
                        "showConfirmButton: true});", true);
            }
        }

        protected void FindMatchingTrade(SellOrder sellOrder)
        {   //MATCHING ALGORITHM TO MATCH THE SELL ORDER WITH ACTIVE BUY ORDERS
            MatchingAlgorithmController controller = new MatchingAlgorithmController();
            List<BuyOrder> buyOrders = controller.getBuyOrders();
            foreach (var buyOrder in buyOrders)
            {
                double balance = controller.getSellOrderBalance(sellOrder.sellOrderId);
                if (balance > 0)
                {
                    // Check if the sell order price is less than or equal to the buy order's maximum price
                    if (buyOrder.maximumPrice >= sellOrder.minimumPrice && buyOrder.userId != sellOrder.UserId)
                    {
                        Trades trades = new Trades();
                        trades.pricePercc = sellOrder.minimumPrice;
                        trades.sellOrderId = sellOrder.sellOrderId;
                        trades.buyOrderId = buyOrder.buyOrderId;
                        trades.excecutedDateTime = DateTime.Now;

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
                            else continue;
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
                            else continue;
                        }

                        int trade_id = controller.SaveTrade(trades);
                        int sell_user = sellOrder.UserId;
                        int buy_user = buyOrder.userId;
                        controller.UpdateCashWalletForSeller(sell_user, trade_id, trades.totalPrice, trades.excecutedDateTime);
                        controller.UpdateCashWalletForBuyer(buy_user, trade_id, trades.totalPrice, trades.excecutedDateTime);

                        controller.UpdateCCWalletForSeller(sell_user, trade_id, trades.quantity, trades.excecutedDateTime);
                        controller.UpdateCCWalletForBuyer(buy_user, trade_id, trades.quantity, trades.excecutedDateTime);
                    }
                }
            }
        }

        protected void getOrderTradeTypeData()
        {
            OrderTradeTypeController orderTradeTypeController = new OrderTradeTypeController();
            DataTable dt = new DataTable();
            dt = orderTradeTypeController.GetOrderTradeType();

            ddlOrderType.DataSource = dt;
            ddlOrderType.DataTextField = "order_type_description";
            ddlOrderType.DataValueField = "order_type_id";
            ddlOrderType.DataBind();

            ddlOrderType.Items.Insert(0, new ListItem("Select Order Type", "0"));
        }

        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrderType.SelectedIndex == 1)
            {
                txtMinQuantity.Text = txtSellQuantity.Text;
            }
        }

        protected void cancelOrder()
        {
            txtMinPrice.ReadOnly = true;
            txtSellQuantity.ReadOnly = true;
            txtMinQuantity.ReadOnly = true;
            ddlOrderType.Enabled = false;

            btnPlaceOrder.Text = "Cancel";
            btnPlaceOrder.CssClass = "btn btn-danger";
        }
    }
}