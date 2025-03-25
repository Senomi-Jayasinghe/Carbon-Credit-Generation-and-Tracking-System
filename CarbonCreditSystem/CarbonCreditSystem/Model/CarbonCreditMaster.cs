using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class CarbonCreditMaster
    {
        public int userId { get; set; }
        public double carbonCreditBalance { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}