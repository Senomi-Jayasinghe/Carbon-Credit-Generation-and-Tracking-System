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
    public partial class AuthorizeCCUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ccGeneratedID"] != null)
                {
                    int ccGeneratedID;
                    if (int.TryParse(Request.QueryString["ccGeneratedID"], out ccGeneratedID))
                    {
                        GetData(ccGeneratedID); //GET DETAILS
                    }
                }
            }
        }

        protected void GetData(int ccGeneratedID)
        {   //GET DETAILS OF CARBON CREDIT GENERATED FROM DATABASE
            AuthorizeController authorizeController = new AuthorizeController();
            DataTable dt = authorizeController.getCCDetails(ccGeneratedID);
            foreach (DataRow dr in dt.Rows)
            {
                txtUserID.Text = (dr["user_id"]).ToString();
                txtName.Text = (dr["user_fullname"]).ToString();
                txtUserNIC.Text = (dr["user_nic"]).ToString();

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

                txtGeneratedDay.Text = Convert.ToDateTime(dr["entry_date"]).ToString("dd/MM/yyyy");
                txtExpiryDay.Text = Convert.ToDateTime(dr["cc_expiredate"]).ToString("dd/MM/yyyy");

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
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {   //APPROVE 
            CarbonCreditDetails carbonCreditDetails = new CarbonCreditDetails();
            carbonCreditDetails.ccGeneratedId = Convert.ToInt32(hdnCCGeneratedID.Value);
            carbonCreditDetails.ccAuthorizedUser = Convert.ToInt32(Session["USER_ID"]);
            carbonCreditDetails.ccAuthorizedDate = DateTime.Now;
            carbonCreditDetails.userId = Convert.ToInt32(txtUserID.Text);
            carbonCreditDetails.ccGenerated = Convert.ToDouble(txtCarbonCreditsTonnes.Text);

            AuthorizeController authorizeController = new AuthorizeController();
            authorizeController.authorize(carbonCreditDetails);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Authorized Successfuly', " +
                    "showConfirmButton: true});", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {   //OPEN TEXT BOX TO ENTER REJECT REASON
            lblReject.Visible = true;
            txtRejectReason.Visible = true;
            btnConfirm.Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {   //REJECT
            int cc_generatedId = Convert.ToInt32(hdnCCGeneratedID.Value);
            int authorizerId = Convert.ToInt32(Session["USER_ID"]);
            DateTime authrorizedDate = DateTime.Now;
            string reason = txtRejectReason.Text;
            AuthorizeController authorizeController = new AuthorizeController();
            authorizeController.reject(cc_generatedId, authorizerId, authrorizedDate, reason);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Rejected Successfuly', " +
                    "showConfirmButton: true});", true);
        }
    }
}