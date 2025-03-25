using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Model
{
    public class UserLogin
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string userStatus { get; set; }
        public int userRoleId { get; set; }
        public int attempts { get; set;}
        public DateTime lastPasswordChangeDate { get; set; }
    }
}