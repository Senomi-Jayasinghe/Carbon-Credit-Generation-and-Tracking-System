using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class CalculatorUI : System.Web.UI.Page
    {
        public string Mode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Mode = Request.QueryString["mode"];

            if (!IsPostBack)
            {
                TreeDetailsController treeDetailsController = new TreeDetailsController();
                treeDetailsController.UpdateTreeAge(); //UPDATE TREE AGE
                GetTrees(); /*GET AVAILABLE TREES FROM WHICH CARBON CREDITS HAVE NOT BEEN
                            GENERATED YET TO THE DROP DOWN LIST*/
                
                if (Request.QueryString["mode"] == "G") //GENERATE CARBON CREDITS MODE
                {
                    ddlTrees.Visible = true;
                    lblDDLText.Visible = true;
                    btnCalculate.Visible = false;
                    ViewState["Mode"] = "G";
                    txtAge.ReadOnly = true;
                    txtHeight.ReadOnly = true;
                    txtDiameter.ReadOnly = true;
                    lblheader.Text = "Generate Carbon Credits";
                    navLink.Text = "Generate Carbon Credits";
                    navLink.NavigateUrl = "CalculatorUI.aspx?mode=G";
                }
                if (Request.QueryString["ccGeneratedID"] != null)
                {
                    int ccGeneratedID;
                    if (int.TryParse(Request.QueryString["ccGeneratedID"], out ccGeneratedID))
                    {
                        txtAge.ReadOnly = true;
                        txtHeight.ReadOnly = true;
                        txtDiameter.ReadOnly = true;
                        getData(ccGeneratedID);

                        if (Request.QueryString["mode"] == "V") //VIEW GENERATED CARBON CREDITS
                        {
                            btnCalculate.Visible = false;
                            btnAdd.Visible = false;
                            lblheader.Text = "View Generated Carbon Credits";
                        }
                        else if (Request.QueryString["mode"] == "D") //WITHDRAW GENERATED CARBON CREDITS BEFORE AUTHORIZATION
                        {
                            btnCalculate.Visible = false;
                            btnAdd.Visible = true;
                            btnAdd.CssClass = "btn btn-danger btn-border";
                            btnAdd.Text = "Withdraw from Authorization";
                            lblheader.Text = "Delete Generated Carbon Credits";
                        }
                    }
                }
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {   //CARBON CREDIT CALCULATION
            double height = Convert.ToDouble(txtHeight.Text);
            double diameter = Convert.ToDouble(txtDiameter.Text);
            double age = Convert.ToDouble(txtAge.Text);
            double weight;
            if (diameter < 11)
            {
                weight = 0.25 * Math.Pow(diameter, 2) * height;
            }
            else
            {
                weight = 0.15 * Math.Pow(diameter, 2) * height;
            }

            double greenWeight = weight * 1.2;
            txtGreenW.Text = greenWeight.ToString();

            double dryWeight = greenWeight * 0.725;
            txtDryW.Text = dryWeight.ToString();

            double carbonWeight = dryWeight * 0.5;
            txtCarbonW.Text = carbonWeight.ToString();

            double CO2SeqWeight = carbonWeight * 3.6663;
            txtCO2SW.Text = CO2SeqWeight.ToString();

            txtCO2SWPYear.Text = ((CO2SeqWeight / age)).ToString();

            txtCarbonCreditsTonnes.Text = ((CO2SeqWeight / age) * 0.000453592).ToString();

        }

        protected void ddlTrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTrees.SelectedValue != "0") // Ensure it's not the default "Select Tree"
            {
                btnAdd.Visible = true;
                string selectedTreeId = ddlTrees.SelectedValue;
                var treeDetails = (Dictionary<string, Dictionary<string, string>>)ViewState["TreeDetails"];// Retrieve tree details from ViewState

                if (treeDetails.ContainsKey(selectedTreeId))
                {   // Populate text fields with tree details
                    hdnTreeID.Value = treeDetails[selectedTreeId]["tree_id"];
                    txtDiameter.Text = treeDetails[selectedTreeId]["tree_width"];
                    txtHeight.Text = treeDetails[selectedTreeId]["tree_height"];
                    txtAge.Text = treeDetails[selectedTreeId]["tree_age"];
                    byte[] treePicture = Convert.FromBase64String(treeDetails[selectedTreeId]["tree_picture"]);// Convert the Base64 string back to byte[]

                    TreeDetails treeDetailsPic = new TreeDetails();
                    treeDetailsPic.treePicture = treePicture;
                    treeDetailsPic.treePictureFormat = treeDetails[selectedTreeId]["tree_picture_format"];
                    string mimeType = "image/jpeg";

                    if (treeDetailsPic.treePictureFormat.ToLower() == ".png")
                    {
                        mimeType = "image/png";
                    }
                    else if (treeDetailsPic.treePictureFormat.ToLower() == ".jpg" || treeDetailsPic.treePictureFormat.ToLower() == ".jpeg")
                    {
                        mimeType = "image/jpeg";
                    }

                    imgTree.Visible = true;
                    string base64String = Convert.ToBase64String(treeDetailsPic.treePicture, 0, treeDetailsPic.treePicture.Length);
                    imgTree.ImageUrl = $"data:{mimeType};base64,{base64String}";
                }
            }
            if (ViewState["Mode"].ToString() == "G")
            {
                btnCalculate_Click(sender, e); //CALCULATE CARBON CREDITS AND FILL FIELDS
            }
        }

        protected void GetTrees()
        {   // GET TREE DATA TO DROP DOWN
            int userId = Convert.ToInt32(Session["USER_ID"]);
            TreeDetailsController treeDetailsController = new TreeDetailsController();
            DataTable dt = treeDetailsController.GetTreesforCalc(userId);
            ddlTrees.Items.Clear();

            // Create a dictionary to hold additional tree details
            Dictionary<string, Dictionary<string, string>> treeDetails = new Dictionary<string, Dictionary<string, string>>();

            foreach (DataRow row in dt.Rows)
            {
                string treeDisplay = row["tree_name"].ToString() + " - " + row["tree_location"].ToString();
                string treeId = row["tree_id"].ToString();

                // Convert tree picture (byte[]) to Base64 string for ViewState storage
                string treePictureBase64 = Convert.ToBase64String((byte[])row["tree_picture"]);

                // Add tree details including picture as Base64 string
                treeDetails[treeId] = new Dictionary<string, string>
                {
                    { "tree_id", row["tree_id"].ToString() },
                    { "tree_width", row["tree_width"].ToString() },
                    { "tree_height", row["tree_height"].ToString() },
                    { "tree_age", row["tree_age"].ToString() },
                    { "tree_picture", treePictureBase64 },  // Store as Base64 string
                    { "tree_picture_format", row["tree_picture_format"].ToString() }
                };

                ddlTrees.Items.Add(new ListItem(treeDisplay, treeId));
            }
            // Store tree details in ViewState for later use
            ViewState["TreeDetails"] = treeDetails;
            ddlTrees.Items.Insert(0, new ListItem("Select Tree", "0"));
            ddlTrees.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {   //SEND DATA FOR AUTHORIZATION
            if (btnAdd.Text == "Add to Wallet")
            {
                CarbonCreditDetails carbonCreditDetails = new CarbonCreditDetails();
                carbonCreditDetails.userId = Convert.ToInt32(Session["USER_ID"]);
                carbonCreditDetails.treeId = Convert.ToInt32(hdnTreeID.Value);
                carbonCreditDetails.ccGenerated = Convert.ToDouble(txtCarbonCreditsTonnes.Text);
                carbonCreditDetails.totalGreenWeight = Convert.ToDouble(txtGreenW.Text);
                carbonCreditDetails.dryWeight = Convert.ToDouble(txtDryW.Text);
                carbonCreditDetails.carbonWeight = Convert.ToDouble(txtCarbonW.Text);
                carbonCreditDetails.cO2Sequestered = Convert.ToDouble(txtCO2SW.Text);
                carbonCreditDetails.co2SequesteredPerYear = Convert.ToDouble(txtCO2SWPYear.Text);
                carbonCreditDetails.entryDate = DateTime.Now;
                carbonCreditDetails.ccExpireDate = carbonCreditDetails.entryDate.AddYears(1);
                CarbonCreditsController carbonCreditsController = new CarbonCreditsController();
                carbonCreditsController.SendtoAuthorize(carbonCreditDetails);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Sent for Authorization', " +
                    "showConfirmButton: true});", true);
            }
            else if (btnAdd.Text == "Withdraw from Authorization")
            {
                CarbonCreditsController carbonCreditsController = new CarbonCreditsController();
                int ccGeneratedId = Convert.ToInt32(hdnCCGeneratedID.Value);
                carbonCreditsController.withdrawFromApproval(ccGeneratedId);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Withdrawn from Authorization', " +
                    "showConfirmButton: true});", true);
            }
        }

        protected void getData(int ccGeneratedID)
        {   //GET DATA FOR VIEW AND DELETE MODES
            CarbonCreditsController carbonCreditsController = new CarbonCreditsController();
            DataTable dt = carbonCreditsController.getCCDetails(ccGeneratedID);
            foreach (DataRow dr in dt.Rows)
            {
                hdnCCGeneratedID.Value = dr["cc_generated_id"].ToString();
                txtGreenW.Text = (dr["total_green_weight"]).ToString();
                txtDryW.Text = (dr["dry_weight"]).ToString();
                txtCarbonW.Text = (dr["carbon_weight"]).ToString();
                txtCO2SW.Text = (dr["co2_sequestered"]).ToString();
                txtCO2SWPYear.Text = (dr["co2_sequesteredPerYear"]).ToString();
                txtCarbonCreditsTonnes.Text = (dr["cc_generated"]).ToString();
                txtHeight.Text = (dr["tree_height"]).ToString();
                txtDiameter.Text = (dr["tree_width"]).ToString();
                txtAge.Text = (dr["tree_age"]).ToString();
                string rejectreason = (dr["reject_reason"]).ToString();

                string mimeType = "image/jpeg";

                if (dr["tree_picture_format"].ToString().ToLower() == ".png")
                {
                    mimeType = "image/png";
                }
                else if (dr["tree_picture_format"].ToString().ToLower() == ".jpg" || dr["tree_picture_format"].ToString().ToLower() == ".jpeg")
                {
                    mimeType = "image/jpeg";
                }

                byte[] image = (byte[])dr["tree_picture"];
                string base64String = Convert.ToBase64String(image, 0, image.Length);
                imgTree.ImageUrl = $"data:{mimeType};base64,{base64String}";

                if (rejectreason != "")
                {
                    txtRejectReason.Text = rejectreason;
                    lblReject.Visible = true;
                    txtRejectReason.Visible = true;
                }
            }
        }
    }
}