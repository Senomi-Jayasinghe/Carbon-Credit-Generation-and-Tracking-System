using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class User
    {
        public int userId { get; set; }
        public int titleId { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userFullName { get; set; }
        public string userAddress { get; set; }
        public string userTelephoneNo { get; set; }
        public string userMobileNo { get; set; }
        public string userEmail { get; set; }
        public long userNIC { get; set; }
        public int userType { get; set; }
        public int entryUser {  get; set; }
        public DateTime entryDate { get; set; }
    }
}