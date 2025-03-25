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
    public partial class TrackCarbonCredits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarbonCreditsController carbonCreditscontroller = new CarbonCreditsController();
            carbonCreditscontroller.expireCC(); //EXPIRE CARBON CREDITS ON PAGE LOAD IF EXPIRY DATE IS REACHED
            GetData(); //GET DATA TO TABLE
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            Response.Redirect("CalculatorUI.aspx?mode=G"); //DIRECT TO CALCULATOR IF USER WANTS TO GENERATE MORE
        }

        protected void GetData()
        {   //GET DATA FROM DATABASE
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CarbonCreditsController carbonCreditsController = new CarbonCreditsController();
            DataTable dt = carbonCreditsController.GetGeneratedCCDetails(user_id);

            grdCarbonCredits.DataSource = dt;
            grdCarbonCredits.DataBind();
        }

        protected void grdCarbonCredits_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {   //VIEW
            HiddenField field = (HiddenField)grdCarbonCredits.Rows[e.NewSelectedIndex].FindControl("hdnGrdCCId");
            CarbonCreditDetails carbonCreditDetails = new CarbonCreditDetails();
            hdnCCId.Value = field.Value;
            carbonCreditDetails.ccGeneratedId = Convert.ToInt32(field.Value);
            Response.Redirect("CalculatorUI.aspx?ccGeneratedID=" + carbonCreditDetails.ccGeneratedId + "&mode=V");
        }

        protected void grdCarbonCredits_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {   //DELETE
            HiddenField field = (HiddenField)grdCarbonCredits.Rows[e.RowIndex].FindControl("hdnGrdCCId");
            CarbonCreditDetails carbonCreditDetails = new CarbonCreditDetails();
            hdnCCId.Value = field.Value;
            carbonCreditDetails.ccGeneratedId = Convert.ToInt32(field.Value);
            Response.Redirect("CalculatorUI.aspx?ccGeneratedID=" + carbonCreditDetails.ccGeneratedId + "&mode=D");
        }

        protected void grdCarbonCredits_RowDataBound(object sender, GridViewRowEventArgs e)
        {   
            if (e.Row.RowType == DataControlRowType.DataRow)
            {   // Get the status value from the current row
                string status = DataBinder.Eval(e.Row.DataItem, "cc_authorizedStatus").ToString();
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                if (lblStatus != null)
                {
                    // Change text and color based on the status
                    switch (status)
                    {
                        case "P":
                            lblStatus.Text = "Pending";
                            lblStatus.ForeColor = System.Drawing.Color.Orange;
                            break;
                        case "R":
                            lblStatus.Text = "Rejected";
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "A":
                            lblStatus.Text = "Accepted";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "X":
                            lblStatus.Text = "Expired";
                            lblStatus.ForeColor = System.Drawing.Color.Gray;
                            break;
                        default:
                            lblStatus.Text = "Unknown";
                            lblStatus.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }
                // Get the delete button and hide it if the status is "A"
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (lnkDelete != null && status == "A")
                {
                    lnkDelete.Visible = false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblrecords.Visible = false;
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            string status = ddlStatus.SelectedValue;
            string inputFromDate = txtFrom.Text;
            string inputToDate = txtTo.Text;
            string FromDate, ToDate;

            if (!string.IsNullOrEmpty(inputFromDate)) // If the user provided a 'From' date
            {
                DateTime parsedFromDate = DateTime.Parse(inputFromDate);
                FromDate = parsedFromDate.ToString("yyyy-MM-dd");
            }
            else
            {
                FromDate = "1000-01-01"; // Default start date
            }

            if (!string.IsNullOrEmpty(inputToDate)) // If the user provided a 'To' date
            {
                DateTime parsedToDate = DateTime.Parse(inputToDate);
                ToDate = parsedToDate.ToString("yyyy-MM-dd");
            }
            else
            {
                ToDate = "9999-12-31"; // Default end date
            }

            CarbonCreditsController carbonCreditsController = new CarbonCreditsController();
            DataTable dt = carbonCreditsController.SearchCCDetails(status, FromDate, ToDate, user_id);
            grdCarbonCredits.DataSource = dt;
            grdCarbonCredits.DataBind();
        }
    }
}