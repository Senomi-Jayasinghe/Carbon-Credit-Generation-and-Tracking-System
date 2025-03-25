using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class CarbonCreditDetails
    {
        public int ccGeneratedId { get; set; }
        public int userId { get; set; }
        public double ccGenerated {  get; set; }
        public double totalGreenWeight { get; set; }
        public double dryWeight { get; set;}
        public double carbonWeight { get; set;}
        public double cO2Sequestered { get; set;}
        public double co2SequesteredPerYear { get; set;}
        public int treeId { get; set; }
        public DateTime ccExpireDate { get; set; }
        public string ccAuthorizedStatus { get; set;}
        public string rejectReason { get; set;}
        public DateTime ccAuthorizedDate { get; set; }
        public int ccAuthorizedUser { get; set;}
        public string entryUser { get; set; }
        public DateTime entryDate { get; set;}
    }
}