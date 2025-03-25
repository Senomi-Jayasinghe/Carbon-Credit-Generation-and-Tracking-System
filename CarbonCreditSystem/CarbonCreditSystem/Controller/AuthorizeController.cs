using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class AuthorizeController
    {
        public DataTable getPendingCC()
        {   //GET PENDING CARBON CREDITS FOR AUTHORIZATION
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT cc_generated_id, user_id, cc_generated, FORMAT(cc_expiredate, 'yyyy-MM-dd') AS cc_expiredate, cc_authorizedStatus, " +
                "FORMAT(entry_date, 'yyyy-MM-dd') AS entry_date FROM CarbonCreditDetails " +
                "WHERE cc_authorizedStatus = 'P'";
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable getCCDetails(int ccGeneratedID)
        {   //GET CARBON CREDIT DETAILS FOR AUTHORIZATION
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT ccd.cc_generated_id, ccd.user_id, ccd.tree_id, ccd.cc_generated, ccd.total_green_weight, ccd.dry_weight, ccd.carbon_weight, " +
                "ccd.co2_sequestered, ccd.co2_sequesteredPerYear, ccd.cc_expiredate, ccd.entry_date, u.user_fullname, u.user_nic, t.tree_height, t.tree_width, " +
                "t.tree_age, t.tree_picture, t.tree_picture_format FROM CarbonCreditDetails ccd JOIN TreeDetails t ON ccd.tree_id = t.tree_id JOIN Users u " +
                "ON ccd.user_id = u.user_id WHERE ccd.cc_generated_id = " + ccGeneratedID;
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public void authorize(CarbonCreditDetails carbonCreditDetails)
        {   //AUTHORIZE
            SQLConfig sqlconfig = new SQLConfig(); //UPDATE STATUS TO ACCEPTED (A)
            string sql = "Update CarbonCreditDetails SET cc_authorizedStatus = 'A', cc_authorized_date = '" + carbonCreditDetails.ccAuthorizedDate + "' , " +
                "cc_authorized_user = " + carbonCreditDetails.ccAuthorizedUser + "WHERE cc_generated_id = " + carbonCreditDetails.ccGeneratedId;
            sqlconfig.ExecuteCUD(sql);
            //INSERT TO CARBON CREDIT MASTER HISTORY
            string sqlAddtoHistory = "INSERT INTO CarbonCreditMasterHistory (user_id, previous_cc_balance, cc_update_type, update_quantity, update_date, entry_user, " +
                "entry_date) " +
                "VALUES (" + carbonCreditDetails.userId + " ," +
                "(SELECT carbon_creditbalance FROM CarbonCreditMaster WHERE user_id = " + carbonCreditDetails.userId + ") ,1 ," 
                + carbonCreditDetails.ccGenerated + " ,'" + carbonCreditDetails.ccAuthorizedDate + "' ," + carbonCreditDetails.ccAuthorizedUser + " ,'" 
                + carbonCreditDetails.ccAuthorizedDate + "');";
            sqlconfig.ExecuteCUD(sqlAddtoHistory);
            //UPDATE CARBON CREDIT WALLET
            string sqlAdd = "UPDATE CarbonCreditMaster SET carbon_creditbalance = carbon_creditbalance + " + carbonCreditDetails.ccGenerated + " ," +
                "last_update_date = '" + carbonCreditDetails.ccAuthorizedDate + "' WHERE user_id = " + carbonCreditDetails.userId;
            sqlconfig.ExecuteCUD(sqlAdd);
        }

        public void reject(int ccGeneratedID, int authorizerId, DateTime authorizedDate, string reason)
        {   //REJECT //UPDATE STATUS TO REJECTED (R)
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "Update CarbonCreditDetails SET cc_authorizedStatus = 'R', cc_authorized_date = '" + authorizedDate + "' , " +
                "cc_authorized_user = " + authorizerId + ", reject_reason = '" + reason + "' WHERE cc_generated_id = " + ccGeneratedID;
            sqlconfig.ExecuteCUD(sql);
        }
    }
}