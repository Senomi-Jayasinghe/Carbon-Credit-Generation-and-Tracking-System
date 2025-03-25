using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class TreeRepositoryUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TreeDetailsController treeDetailsController = new TreeDetailsController();
            treeDetailsController.UpdateTreeAge(); //UPDATE TREE AGE BY ONE IF MORE THAN ONE YEAR HAS PASSED AFTER ENTRY DATE
            getTreesFromDatabase(); //GET TREES
        }

        public void getTreesFromDatabase()
        { //GET TREE DATA FOR THE USER'S TREES
            try
            {
                int user_id = Convert.ToInt32(Session["USER_ID"]);
                TreeDetailsController treeDetailsController = new TreeDetailsController();
                DataTable dt = new DataTable();
                dt = treeDetailsController.GetTrees(user_id);

                grdTrees.DataSource = dt;
                grdTrees.DataBind();

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void grdTrees_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //EDIT BUTTON
            HiddenField field = (HiddenField)grdTrees.Rows[e.NewSelectedIndex].FindControl("hdnGrdTreeId");
            TreeDetails treeDetails = new TreeDetails();
            hdnTreeId.Value = field.Value;
            treeDetails.treeId = Convert.ToInt32(field.Value);
            //REDIRECT TO TREE DETAILS PAGE IN UPDATE MODE
            Response.Redirect("TreeDetailsUI.aspx?treeID=" + treeDetails.treeId + "&mode=U");
        }

        protected void grdTrees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //DELETE BUTTON
            HiddenField field = (HiddenField)grdTrees.Rows[e.RowIndex].FindControl("hdnGrdTreeId");
            TreeDetails treeDetails = new TreeDetails();
            hdnTreeId.Value = field.Value;
            treeDetails.treeId = Convert.ToInt32(field.Value);
            //REDIRECT TO TREE DETAILS PAGE IN DELETE MODE
            Response.Redirect("TreeDetailsUI.aspx?treeID=" + treeDetails.treeId + "&mode=D");
        }
    }
}