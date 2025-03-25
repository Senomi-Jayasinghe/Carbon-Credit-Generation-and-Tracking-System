using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using CarbonCreditSystem.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.Controller
{
    public class CarbonCreditsController
    {
        public void SendtoAuthorize(CarbonCreditDetails carbonCreditDetails)
        {   //SEND FOR AUTHORIZATION AND INSERT DETAILS OF CALCULATION TO CarbonCreditDetails TABLE
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "Insert into CarbonCreditDetails (user_id, tree_id, cc_generated, total_green_weight, dry_weight, carbon_weight, co2_sequestered, " +
                "co2_sequesteredPerYear, cc_authorizedStatus, cc_expiredate, entry_user, entry_date) " +
                "VALUES (" + carbonCreditDetails.userId + ", " + carbonCreditDetails.treeId + ", " + carbonCreditDetails.ccGenerated + ", "
                + carbonCreditDetails.totalGreenWeight + ", " + carbonCreditDetails.dryWeight + ", " + carbonCreditDetails.carbonWeight + ", "
                + carbonCreditDetails.cO2Sequestered + ", " + carbonCreditDetails.co2SequesteredPerYear + ", 'P', '" + carbonCreditDetails.ccExpireDate + "', "
                + carbonCreditDetails.userId + ", '" + carbonCreditDetails.entryDate + "')";
            sqlconfig.ExecuteCUD(sql);
        }

        public DataTable GetGeneratedCCDetails(int user_id)
        {   //GET THE USER'S CARBON CREDIT RECORDS FOR THE TABLE
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT TOP (10) cc_generated_id, cc_generated, FORMAT(entry_date, 'yyyy-MM-dd') AS entry_date, " +
                "FORMAT(cc_expiredate, 'yyyy-MM-dd') AS cc_expiredate, cc_authorizedStatus FROM CarbonCreditDetails " +
                "WHERE user_id = " + user_id + "ORDER BY entry_date DESC";
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }

        public void expireCC()
        {   //EXPIRE CARBON CREDITS ON PAGE LOAD
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "UPDATE CarbonCreditDetails SET cc_authorizedStatus = 'X' WHERE cc_expiredate < GETDATE() " +
                "AND cc_authorizedStatus = 'P';";
            sQLConfig.ExecuteCUD(sql);
        }

        public DataTable getCCDetails(int ccGeneratedID)
        {
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT ccd.cc_generated_id, ccd.tree_id, ccd.cc_generated, ccd.total_green_weight, ccd.dry_weight, ccd.carbon_weight, " +
                "ccd.co2_sequestered, ccd.co2_sequesteredPerYear, ccd.reject_reason, td.tree_picture, td.tree_picture_format, " +
                "td.tree_height, td.tree_width, td.tree_age FROM CarbonCreditDetails ccd " +
                "JOIN TreeDetails td ON ccd.tree_id = td.tree_id WHERE ccd.cc_generated_id = " + ccGeneratedID;
            DataTable dt = sqlconfig.ExecuteSelect(sql);

            return dt;
        }

        public void withdrawFromApproval(int ccGeneratedID)
        {
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "DELETE FROM CarbonCreditDetails WHERE cc_generated_id =" + ccGeneratedID;
            sqlconfig.ExecuteCUD(sql);
        }

        public DataTable SearchCCDetails(string status, string FromDate, string ToDate, int user_id)
        {   //SEARCH FUNCTIONALITY
            SQLConfig sqlconfig = new SQLConfig();
            string sql = "SELECT cc_generated_id, cc_generated, FORMAT(entry_date, 'yyyy-MM-dd') AS entry_date, " +
                "FORMAT(cc_expiredate, 'yyyy-MM-dd') AS cc_expiredate, cc_authorizedStatus FROM CarbonCreditDetails " +
                "WHERE (cc_authorizedStatus = '" + status + "' OR '' = '" + status + "') AND (entry_date BETWEEN '" + FromDate + "' AND '"
                + ToDate + "') AND user_id = " + user_id + " ORDER BY entry_date DESC";
            DataTable dt = sqlconfig.ExecuteSelect(sql);
            return dt;
        }
    }
}