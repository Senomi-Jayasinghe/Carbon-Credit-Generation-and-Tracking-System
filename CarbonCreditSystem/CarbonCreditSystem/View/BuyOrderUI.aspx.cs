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
    public partial class BuyOrderUI : System.Web.UI.Page
    {
        string mode = "X";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getOrderTradeTypeData();//GET DATA FOR DROP DOWN LIST
                ViewState["Mode"] = mode;
                if (Request.QueryString["orderID"] != null)
                {
                    int orderID; //GET ID FROM URL
                    if (int.TryParse(Request.QueryString["orderID"], out orderID))
                    {
                        getOrder(orderID);

                        if (Request.QueryString["mode"] == "C")
                        {   //GET CANCEL MODE
                            mode = "C";
                            ViewState["Mode"] = mode;
                            cancelOrder();//CANCEL ORDER
                        }
                    }
                }
            }
        }

        protected void getOrder(int orderID)
        {   //GET ORDER DETAILS FROM DATABASE AND FILL FORM
            TradeController tradeController = new TradeController();
            BuyOrder buyOrder = tradeController.getBuyOrder(orderID);

            hdnOrderId.Value = orderID.ToString();
            txtBuyQuantity.Text = buyOrder.buyQuantity.ToString();
            txtMinQuantity.Text = buyOrder.minimumQuantity.ToString();
            txtMaxPrice.Text = buyOrder.maximumPrice.ToString();
            ddlOrderType.SelectedIndex = buyOrder.orderTradeType;
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CashWalletController cashWalletController = new CashWalletController();
            //GET AVAILABLE CASH WALLET BALANCE I.E., CASH WALLET BALANCE - AMOUNT PLACED IN ACTIVE BUY ORDERS
            double balance = cashWalletController.getAvailableBalance(user_id); 
            double cashamount = Convert.ToDouble(txtMaxPrice.Text);
            double amount = Convert.ToDouble(txtBuyQuantity.Text);
            mode = ViewState["Mode"].ToString();
            if (cashamount > balance && mode != "C")
            {
                if (balance == 0)
                {   //CHECK IF THE BALANCE IS 0
                    lblMsg.Text = "Amount Exceeding Balance, Your Balance Available is " + balance + ". " +
                        "Try registering your bank account if not registered yet.";
                }
                else
                {   //CHECK IF THE PLACING PRICE EXCEEDS THE AVAILABLE BALANCE IN THE CASH WALLER
                    lblMsg.Text = "Amount Exceeding Balance, Your Balance Available is " + balance;
                }
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Visible = false;
                double minQty = Convert.ToDouble(txtMinQuantity.Text);
                if (amount % minQty != 0)
                {   //THE TOTAL QUANTITY MUST BE DIVISIBLE BY THE MINIMUM QUANTITY
                    lblMsg.Text = $"Minimum Quantity should be a divisor of Order Quantity. " +
                                   $"For the Order Quantity of {amount}, please enter a Minimum Quantity that divides it evenly.";
                    lblMsg.Visible = true;
                }
                else if (ddlOrderType.SelectedIndex == 1)
                {   // IF THE ORDER TRADE TYPE IS A SINGLE TRADE
                    txtMinQuantity.Text = txtBuyQuantity.Text;
                }
            }

            BuyOrder buyOrder = new BuyOrder();
            buyOrder.userId = Convert.ToInt32(Session["USER_ID"]);
            buyOrder.buyQuantity = Convert.ToDouble(txtBuyQuantity.Text);
            buyOrder.minimumQuantity = Convert.ToDouble(txtMinQuantity.Text);
            buyOrder.maximumPrice = Convert.ToDouble(txtMaxPrice.Text);
            buyOrder.orderTradeType = ddlOrderType.SelectedIndex;
            buyOrder.orderDateTime = DateTime.Now;
            buyOrder.buyStatus = "A";
            buyOrder.entryDate = DateTime.Now;
            buyOrder.entryUser = Convert.ToInt32(Session["USER_ID"]);
            buyOrder.balanceQuantity = Convert.ToDouble(txtBuyQuantity.Text);
            buyOrder.expireTime = DateTime.Now.AddYears(1);

            TradeController tradeController = new TradeController();

            if (lblMsg.Visible == false && btnPlaceOrder.Text == "Place Order")
            {
                buyOrder.buyOrderId = tradeController.placeBuyOrder(buyOrder);
                FindMatchingTrade(buyOrder);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Buy Order Placed Successfuly', " +
                        "showConfirmButton: true});", true);
            }
            else if (lblMsg.Visible == false && btnPlaceOrder.Text == "Cancel")
            {
                int orderId = Convert.ToInt32(hdnOrderId.Value);
                tradeController.cancelBuyOrder(orderId);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Buy Order Cancelled Successfuly', " +
                        "showConfirmButton: true});", true);
            }
        }

        protected void FindMatchingTrade(BuyOrder buyOrder)
        {   //MATCHING ALGORITHM FOR BUY ORDERS
            MatchingAlgorithmController controller = new MatchingAlgorithmController();
            List<SellOrder> sellOrders = controller.getSellOrders();
            foreach (var sellOrder in sellOrders)
            {
                double balance = controller.getBuyOrderBalance(buyOrder.buyOrderId);
                if (balance > 0)
                {
                    if (buyOrder.maximumPrice >= sellOrder.minimumPrice && buyOrder.userId != sellOrder.UserId)
                    {
                        Trades trades = new Trades();
                        trades.pricePercc = buyOrder.maximumPrice;
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
            //GET ORDER TYPES TO THE DROP DOWN LIST
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
            {   //IF THE ORDER TRADE DATA TYPE IS SINGLE, THE ORDER QTY AND MIN QTY SHOULD BE THE SAME
                txtMinQuantity.Text = txtBuyQuantity.Text;
                txtMinQuantity.ReadOnly = true;
            }
            if (ddlOrderType.SelectedIndex == 2)
            {   
                txtMinQuantity.ReadOnly = false;
            }
        }

        protected void cancelOrder()
        {
            txtMaxPrice.ReadOnly = true;
            txtBuyQuantity.ReadOnly = true;
            txtMinQuantity.ReadOnly = true;
            ddlOrderType.Enabled = false;

            btnPlaceOrder.Text = "Cancel";
            btnPlaceOrder.CssClass = "btn btn-danger";
        }
    }
}