using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class CarbonCreditMasterHistory
    {
        public int userId {  get; set; }
        public double previousccBalance { get; set; }
        public int ccUpdateType { get; set; }
        public double updateQuantity { get; set; }
        public DateTime updateDate { get; set; }
        public int referenceId { get; set; }
        public string entryUser { get; set; }
        public DateTime entryDate { get; set; }
    }
}