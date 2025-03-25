using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class Trades
    {
        public int tradeId { get; set; }
        public int buyOrderId { get; set; }
        public int sellOrderId { get; set; }
        public double pricePercc {  get; set; }
        public double totalPrice { get; set;}
        public double quantity {  get; set; }
        public DateTime excecutedDateTime { get; set; }
    }
}