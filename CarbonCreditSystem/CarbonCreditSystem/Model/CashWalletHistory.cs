using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class CashWalletHistory
    {
        public int userId { get; set; }
        public double previousBalance { get; set; }
        public int cashUpdateType { get; set; }
        public double updateBalance { get; set; }
        public DateTime updateDate { get; set; }
        public int referenceId { get; set; }
        public string entryUser { get; set; }
        public DateTime entryDate { get; set; }
    }
}