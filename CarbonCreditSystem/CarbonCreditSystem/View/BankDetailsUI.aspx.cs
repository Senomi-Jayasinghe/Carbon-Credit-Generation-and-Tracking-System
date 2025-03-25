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
    public partial class BankDetailsUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBankData(); //GET BANK NAMES FROM DB
            }
        }

        protected void getBankData()
        {
            BankController bankController = new BankController();
            DataTable dt = new DataTable();
            dt = bankController.GetBanks(); //GET BANK NAMES FROM DB

            ddlBankName.DataSource = dt;
            ddlBankName.DataTextField = "bank_name";
            ddlBankName.DataValueField = "bank_id";
            ddlBankName.DataBind();

            ddlBankName.Items.Insert(0, new ListItem("Select Bank Name", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CashWalletController cashWalletController = new CashWalletController();
            CashWallet cashWallet = new CashWallet(); 

            cashWallet.userId = Convert.ToInt32(Session["USER_ID"]);
            cashWallet.bankAccountNo = Convert.ToInt64(txtBankAccountNo.Text);
            cashWallet.bankBranch = ddlBranch.Text;
            cashWallet.bankId = Convert.ToInt32(ddlBankName.SelectedValue);
            cashWallet.lastUpdateDate = DateTime.Now;

            cashWalletController.registerBankAccount(cashWallet); //REGISTER BANK ACCOUNT AND CREATE A CASH WALLET
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "Swal.fire({ icon: 'success', title: 'Bank Registered Successfuly', " +
                        "showConfirmButton: true});", true);
        }
    }
}