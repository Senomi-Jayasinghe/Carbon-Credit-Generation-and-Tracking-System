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
    public partial class CarbonCreditWalletUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            CarbonCreditWalletController controller = new CarbonCreditWalletController();
            lblCCBalance.Text = controller.getBalance(user_id).ToString(); //GET CARBON CREDIT WALLET BALANCE
            GetHistory(user_id); //GET HISTORY
        }

        protected void GetHistory(int user_id)
        {
            CarbonCreditWalletController carbonCreditWalletController = new CarbonCreditWalletController();
            DataTable dt = carbonCreditWalletController.GetHistory(user_id); //GET LAST 10 CARBON CREDIT TRANSACTIONS
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
    }
}