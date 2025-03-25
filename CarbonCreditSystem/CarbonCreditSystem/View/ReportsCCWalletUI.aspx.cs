using CarbonCreditSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonCreditSystem.View
{
    public partial class ReportsCCWalletUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CarbonCreditWalletController controller = new CarbonCreditWalletController();
            lblCCBalance.Text = controller.getBalance(user_id).ToString();
            GetHistory(user_id);
        }

        protected void GetHistory(int user_id)
        {
            CarbonCreditWalletController carbonCreditWalletController = new CarbonCreditWalletController();
            DataTable dt = carbonCreditWalletController.GetFullHistory(user_id);
            grdCarbonCreditHistory.DataSource = dt;
            grdCarbonCreditHistory.DataBind();
        }

        protected void grdCarbonCreditHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the Type value from the current row
                string type = DataBinder.Eval(e.Row.DataItem, "cc_update_type").ToString();
                Label lblType = (Label)e.Row.FindControl("lblType");

                if (lblType != null)
                {
                    // Change text and color based on the status
                    switch (type)
                    {
                        case "1":
                            lblType.Text = "Generated";
                            lblType.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "2":
                            lblType.Text = "Buy";
                            lblType.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "3":
                            lblType.Text = "Sell";
                            lblType.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            lblType.Text = "Unknown";
                            lblType.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);

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

            CarbonCreditWalletController carbonCreditWalletController = new CarbonCreditWalletController();
            DataTable dt = carbonCreditWalletController.getSearchInfo(user_id, FromDate, ToDate);
            grdCarbonCreditHistory.DataSource = dt;
            grdCarbonCreditHistory.DataBind();
        }
    }
}