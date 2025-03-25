using CarbonCreditSystem.Database;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Controller
{
    public class TreeDetailsController
    {
        public void SaveTree(TreeDetails treeDetails)
        {   //SAVE NEW TREE
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "INSERT INTO TreeDetails (tree_location, tree_name, tree_width, tree_height, tree_age, tree_picture, " +
                "tree_picture_format, entry_date, entry_user) " +
             $"VALUES ('{treeDetails.treeLocation}', '{treeDetails.treeName}', {treeDetails.treeWidth}, {treeDetails.treeHeight}, " +
             $"{treeDetails.treeAge}, 0x{BitConverter.ToString(treeDetails.treePicture).Replace("-", "")}, " +
             $"'{treeDetails.treePictureFormat}', '{treeDetails.entrydate}', {treeDetails.entryuser})";
            sQLConfig.ExecuteCUD(sql);
        }

        public void UpdateTree(TreeDetails treeDetails)
        {   //UPDATE EXISTING TREE
            SQLConfig sqlConfig = new SQLConfig();
            string sql = "UPDATE TreeDetails SET " +
                 $"tree_location = '{treeDetails.treeLocation}', " +
                 $"tree_name = '{treeDetails.treeName}', " +
                 $"tree_width = {treeDetails.treeWidth}, " +
                 $"tree_height = {treeDetails.treeHeight}, " +
                 $"tree_age = {treeDetails.treeAge}, " +
                 $"tree_picture = 0x{BitConverter.ToString(treeDetails.treePicture).Replace("-", "")}, " +
                 $"tree_picture_format = '{treeDetails.treePictureFormat}'" +
                 $"WHERE tree_id = {treeDetails.treeId}";
            sqlConfig.ExecuteCUD(sql);
        }

        public void DeleteTree(int treeId)
        {
            //DELETE EXISTING TREE
            SQLConfig SQLConfig = new SQLConfig();
            string sql = "Delete from TreeDetails where tree_id =" + treeId;
            SQLConfig.ExecuteCUD(sql);
        }

        public DataTable GetTrees(int userid)
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select tree_id, tree_location, tree_name, tree_width, tree_height, tree_age from TreeDetails where entry_user =" + userid;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }

        public DataTable GetTreesforCalc(int userid)
        {   //GET TREES FOR CALCULATION
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "SELECT T.tree_id, T.tree_name, T.tree_location, T.tree_height, T.tree_width, T.tree_age , T.tree_picture, T.tree_picture_format " +
                "FROM TreeDetails T " +
                "WHERE NOT EXISTS (SELECT 1 FROM CarbonCreditDetails C WHERE T.tree_id = C.tree_id " +
                "AND C.entry_date >= DATEADD(YEAR, -1, GETDATE()) AND c.cc_authorizedStatus in ('P','A')) AND t.entry_user = " + userid;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            return dt;
        }

        public TreeDetails getTreeByID(int treeID)
        {
            //GET TREE BY ID FOR DELETE AND UPDATE MODES
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "Select tree_id, tree_location, tree_name, tree_width, tree_height, tree_age, tree_picture, tree_picture_format " +
                "from TreeDetails " +
                "where tree_id = " + treeID;
            DataTable dt = sQLConfig.ExecuteSelect(sql);
            TreeDetails treeDetails = new TreeDetails();

            foreach (DataRow dr in dt.Rows)
            {
                treeDetails.treeId = Convert.ToInt32(dr["tree_id"]);
                treeDetails.treeLocation = dr["tree_location"].ToString();
                treeDetails.treeName = dr["tree_name"].ToString();
                treeDetails.treeWidth = Convert.ToDouble(dr["tree_width"]);
                treeDetails.treeHeight = Convert.ToDouble(dr["tree_height"]);
                treeDetails.treeAge = Convert.ToDouble(dr["tree_age"]);
                treeDetails.treePicture = (byte[])dr["tree_picture"];
                treeDetails.treePictureFormat = dr["tree_picture_format"].ToString();
            }

            return treeDetails;
        }

        public void UpdateTreeAge()
        {
            SQLConfig sQLConfig = new SQLConfig();
            string sql = "UPDATE TreeDetails SET tree_age = tree_age + 1, entry_date = GETDATE() WHERE entry_date < DATEADD(YEAR, -1, GETDATE());";
            sQLConfig.ExecuteCUD(sql);
        }
    }
}