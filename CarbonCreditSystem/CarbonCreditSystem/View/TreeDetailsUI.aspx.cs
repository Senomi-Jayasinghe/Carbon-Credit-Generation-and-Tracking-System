using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class TreeDetailsUI : System.Web.UI.Page
    {
        public string UserID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = Session["USER_ID"].ToString();

            if (!IsPostBack)
            {
                if (Request.QueryString["treeID"] != null)
                {
                    int treeID;
                    if (int.TryParse(Request.QueryString["treeID"], out treeID))
                    {
                        getTree(treeID); //GET TREE DETAILS

                        if (Request.QueryString["mode"] == "U")
                        {
                            updateTree();//UPDATE MODE
                        }
                        else if (Request.QueryString["mode"] == "D")
                        {
                            deleteTree();//DELETE MODE
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //GET FIELD INFO TO TREE DETAILS CLASS
                TreeDetailsController treeDetailsController = new TreeDetailsController();
                TreeDetails treeDetails = new TreeDetails();

                treeDetails.treeName = txtTreeName.Text;
                treeDetails.treeLocation = txtLocation.Text;
                treeDetails.treeAge = Convert.ToDouble(txtAge.Text);
                treeDetails.treeWidth = Convert.ToDouble(txtDiameter.Text);
                treeDetails.treeHeight = Convert.ToDouble(txtHeight.Text);
                treeDetails.entrydate = DateTime.Now;
                treeDetails.entryuser = Convert.ToInt32(Session["USER_ID"]);

                if (upTree.HasFile || (btnSave.Text == "Update") || (btnSave.Text == "Delete"))
                {   
                    if (upTree.HasFile)
                    {
                        HttpPostedFile postedFile = upTree.PostedFile;
                        string fileExtension = Path.GetExtension(postedFile.FileName);

                        if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png")
                        {
                            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                            {
                                treeDetails.treePicture = br.ReadBytes(postedFile.ContentLength);
                                treeDetails.treePictureFormat = fileExtension;
                            }
                        }
                        else
                        {
                            throw new Exception("Only images (.jpg, .jpeg, .png) are allowed.");
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {   //UPDATE TREE PICTURE
                        int tree_id = Convert.ToInt32(hdnTreeId.Value);
                        TreeDetails previousTree = treeDetailsController.getTreeByID(tree_id);
                        treeDetails.treePicture = previousTree.treePicture;
                        treeDetails.treePictureFormat = previousTree.treePictureFormat;
                    }

                    if (btnSave.Text == "Save")
                    {   //SAVE
                        treeDetailsController.SaveTree(treeDetails);
                        reloadImage(treeDetails);
                        lblPnlMsg.Text = "Save Success";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Tree Succesfuly Saved', " +
                    "showConfirmButton: true});", true);
                    }
                    else if (btnSave.Text == "Update")
                    {   //UPDATE
                        treeDetails.treeId = Convert.ToInt32(hdnTreeId.Value);
                        treeDetailsController.UpdateTree(treeDetails);
                        getTree(Convert.ToInt32(hdnTreeId.Value));
                        lblPnlMsg.Text = "Tree Details Updated Successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Tree Details Successfuly Updated', " +
                    "showConfirmButton: true});", true);
                    }
                    else if (btnSave.Text == "Delete")
                    {   //DELETE
                        int treeId = Convert.ToInt32(hdnTreeId.Value);
                        treeDetailsController.DeleteTree(treeId);
                        lblPnlMsg.Text = "Tree Deleted Successfully";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Tree Details Successfuly Deleted', " +
                    "showConfirmButton: true});", true);
                    }
                    lblPnlMsg.Visible = true;
                    lblPnlMsg.CssClass = "alert alert-success text-success";
                }
                else
                {
                    lblPnlMsg.Visible = true;
                    lblPnlMsg.Text = "Upload an Image";
                    lblPnlMsg.CssClass = "alert alert-danger";
                }
            }

            catch (Exception ex)
            {
                lblPnlMsg.Visible = true;
                lblPnlMsg.Text = "Save Error!" + ex.Message;
                lblPnlMsg.CssClass = "alert alert-danger";
            }
        }

        protected void getTree(int treeID)
        {   //GET TREE DETAILS FROM DATABASE FOR UPDATE AND DELETE MODES
            TreeDetailsController treeDetailsController = new TreeDetailsController();
            TreeDetails treeDetails = treeDetailsController.getTreeByID(treeID);

            hdnTreeId.Value = treeDetails.treeId.ToString();
            txtTreeName.Text = treeDetails.treeName;
            txtLocation.Text = treeDetails.treeLocation;
            txtDiameter.Text = treeDetails.treeWidth.ToString();
            txtHeight.Text = treeDetails.treeHeight.ToString();
            txtAge.Text = treeDetails.treeAge.ToString();

            string mimeType = "image/jpeg";

            if (treeDetails.treePictureFormat.ToLower() == ".png")
            {
                mimeType = "image/png";
            }
            else if (treeDetails.treePictureFormat.ToLower() == ".jpg" || treeDetails.treePictureFormat.ToLower() == ".jpeg")
            {
                mimeType = "image/jpeg";
            }

            string base64String = Convert.ToBase64String(treeDetails.treePicture, 0, treeDetails.treePicture.Length);
            imgTree.ImageUrl = $"data:{mimeType};base64,{base64String}";
            
        }

        protected void updateTree()
        {
            btnSave.Text = "Update";
        }

        protected void deleteTree()
        {
            txtTreeName.Enabled = false;
            txtLocation.Enabled = false;
            txtDiameter.Enabled = false;
            txtHeight.Enabled = false;
            txtAge.Enabled = false;
            upTree.Enabled = false;

            btnSave.Text = "Delete";
            btnSave.CssClass = "btn btn-danger";
        }

        protected void reloadImage(TreeDetails treeDetails)
        {
            string mimeType = "image/jpeg";

            if (treeDetails.treePictureFormat.ToLower() == ".png")
            {
                mimeType = "image/png";
            }
            else if (treeDetails.treePictureFormat.ToLower() == ".jpg" || treeDetails.treePictureFormat.ToLower() == ".jpeg")
            {
                mimeType = "image/jpeg";
            }

            string base64String = Convert.ToBase64String(treeDetails.treePicture, 0, treeDetails.treePicture.Length);
            imgTree.ImageUrl = $"data:{mimeType};base64,{base64String}";
        }
    }
}