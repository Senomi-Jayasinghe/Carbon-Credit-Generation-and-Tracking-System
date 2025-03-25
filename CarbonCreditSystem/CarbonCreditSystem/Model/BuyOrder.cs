using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class BuyOrder
    {
        public int buyOrderId { get; set; }
        public int userId { get; set; }
        public double buyQuantity { get; set; }
        public double minimumQuantity { get; set;}
        public int orderTradeType { get; set;}
        public double maximumPrice { get; set; }
        public string buyStatus { get; set; }
        public DateTime expireTime { get; set; }
        public DateTime lastBuyDateTime { get; set; }
        public DateTime orderDateTime { get; set;}
        public double balanceQuantity { get; set; }
        public int entryUser { get; set; }
        public DateTime entryDate { get; set; }
    }
}