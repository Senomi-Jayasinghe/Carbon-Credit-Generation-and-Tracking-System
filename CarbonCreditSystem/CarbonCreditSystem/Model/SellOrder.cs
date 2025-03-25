using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class SellOrder
    {
        public int sellOrderId { get; set; }
        public int UserId { get; set;}
        public double sellQuantity { get; set; }
        public double minimumQuantity { get; set; }
        public int orderTradeType { get; set; }
        public double minimumPrice { get; set;}
        public string sellStatus { get; set; }
        public DateTime expireTime { get; set; }
        public DateTime orderDateTime{ get; set; }
        public DateTime lastSellDateTime { get; set; }  
        public double balanceQuantity { get; set; }
        public int entryUser { get; set; }
        public DateTime entryDate { get; set; }
    }
}