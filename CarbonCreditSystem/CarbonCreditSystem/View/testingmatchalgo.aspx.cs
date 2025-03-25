using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class testingmatchalgo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMatch_Click(object sender, EventArgs e)
        {
            MatchingAlgorithmController controller = new MatchingAlgorithmController();
            controller.MatchingAlgorithm();

        }

        //protected void btnMatch_Click(object sender, EventArgs e)
        //{
        //    MatchingAlgorithmController controller = new MatchingAlgorithmController();
        //    List<SellOrder> sellOrders = controller.getSellOrders();
        //    List<BuyOrder> buyOrders = controller.getBuyOrders();
        //    foreach (var sellOrder in sellOrders)
        //    {
        //        foreach (var buyOrder in buyOrders)
        //        {
        //            // Check if the sell order price is less than or equal to the buy order's maximum price
        //            if (buyOrder.maximumPrice >= sellOrder.minimumPrice && buyOrder.userId != sellOrder.UserId)
        //            {
        //                Trades trades = new Trades();
        //                trades.totalPrice = (buyOrder.maximumPrice + sellOrder.minimumPrice) / 2;
        //                trades.sellOrderId = sellOrder.sellOrderId;
        //                trades.buyOrderId = buyOrder.buyOrderId;
        //                trades.excecutedDateTime = DateTime.Now;

        //                // If the sell order trade type is single
        //                if (sellOrder.orderTradeType == 1)
        //                {
        //                    // If the buy order trade type is single and balances are equal
        //                    if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
        //                    {
        //                        trades.pricePercc = trades.totalPrice / sellOrder.balanceQuantity;
        //                        trades.quantity = sellOrder.balanceQuantity;
        //                    }
        //                    // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
        //                    else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.balanceQuantity)
        //                    {
        //                        trades.pricePercc = trades.totalPrice / buyOrder.minimumQuantity;
        //                        trades.quantity = buyOrder.minimumQuantity;
        //                    }
        //                }
        //                // If the sell order trade type is multiple
        //                else if (sellOrder.orderTradeType == 2)
        //                {
        //                    // If the buy order trade type is single and balances are equal
        //                    if (buyOrder.orderTradeType == 1 && buyOrder.balanceQuantity == sellOrder.balanceQuantity)
        //                    {
        //                        trades.pricePercc = trades.totalPrice / buyOrder.balanceQuantity;
        //                        trades.quantity = buyOrder.minimumQuantity;
        //                    }
        //                    // If the buy order trade type is multiple and minimum quantity is less than or equal to sell quantity
        //                    else if (buyOrder.orderTradeType == 2 && buyOrder.minimumQuantity <= sellOrder.minimumQuantity)
        //                    {
        //                        trades.pricePercc = trades.totalPrice / buyOrder.minimumQuantity;
        //                        trades.quantity = buyOrder.minimumQuantity;
        //                    }
        //                }

        //                int trade_id = controller.SaveTrade(trades);
        //                int sell_user = sellOrder.UserId;
        //                int buy_user = buyOrder.userId;
        //                controller.UpdateCashWalletForSeller(sell_user, trade_id, trades.totalPrice, trades.excecutedDateTime);
        //                controller.UpdateCashWalletForBuyer(buy_user, trade_id, trades.totalPrice, trades.excecutedDateTime);

        //                controller.UpdateCCWalletForSeller(sell_user, trade_id, trades.quantity, trades.excecutedDateTime);
        //                controller.UpdateCCWalletForBuyer(buy_user, trade_id, trades.quantity, trades.excecutedDateTime);
        //            }
        //        }
        //    }
        //}
    }
}