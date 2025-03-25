using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class CashWallet
    {
        public int userId { get; set; }
        public double cashBalance { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public long bankAccountNo { get; set; }
        public string bankBranch { get; set; }
        public int bankId { get; set; }
    }
}