<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="CashWalletUI.aspx.cs" Inherits="CarbonCreditSystem.View.CashWalletUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-inner">
        <div class="page-header">
            <ul class="breadcrumbs mb-3">
                <li class="nav-home">
                    <a href="HomeUI.aspx">
                        <i class="icon-home"></i>
                    </a>
                </li>
                <li class="separator">
                    <i class="icon-arrow-right"></i>
                </li>
                <li class="nav-item">
                    <a href="CashWalletUI.aspx">Cash Wallet
                    </a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Cash Wallet" />
                        </div>
                        <div class="card-category">
                            Securely manage and track your cash transactions in one place.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-6 col-md-5">
                                <div class="card card-stats card-round">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-5">
                                                <div class="icon-big text-center">
                                                    <i class="icon-wallet text-success"></i>
                                                </div>
                                            </div>
                                            <div class="col-7 col-stats"> <%--CASH WALLET CARD--%>
                                                <div class="numbers">
                                                    <p class="card-category">Cash Balance</p>
                                                    <h4 class="card-title">$<asp:Label runat="server" ID="lblCashBalance"></asp:Label>
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5"> <%--TOPUP AND WITHDRAW BUTTONS--%>
                                <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modal-topup">Top Up Balance</button>
                                <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#modal-withdraw">Withdraw Balance</button>
                            </div>
                            <%-- TOPUP MODAL --%>
                            <div class="modal fade" id="modal-topup" tabindex="-1" role="dialog" aria-labelledby="modal-form"
                                aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                                    <div class="modal-content">
                                        <div class="modal-body p-0">
                                            <div class="card mb-0">
                                                <div class="card-body">
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Account No:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtAccNoT">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Branch:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtBranchT">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Name:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtNameT">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Amount:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtAmountT" required
                                                        type="number" step="any">
                                                    </asp:TextBox>
                                                    <br /> <%--TOPUP--%>
                                                    <asp:Button runat="server" ID="btnTopUp" class="btn btn-success" Text="Top Up Balance" 
                                                        OnClick="btnTopUp_Click" UseSubmitBehavior="false"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- WITHDRAW MODEL --%>
                            <div class="modal fade" id="modal-withdraw" tabindex="-1" role="dialog" aria-labelledby="modal-form"
                                aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-md" role="document">
                                    <div class="modal-content">
                                        <div class="modal-body p-0">
                                            <div class="card mb-0">
                                                <div class="card-body">
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Account No:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtAccNoW">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Branch:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtBranchW">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Name:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtNameW">
                                                    </asp:TextBox>
                                                    <label for="inlineinput" class="col-md-3 col-form-label">Amount:</label>
                                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtAmountW" required
                                                        type="number" step="any">
                                                    </asp:TextBox>
                                                    <br /> <%--WITHDRAW--%>
                                                    <asp:Button runat="server" ID="btnWithdraw" class="btn btn-warning" Text="Withdraw Balance"
                                                        OnClick="btnWithdraw_Click" UseSubmitBehavior="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- TRANSACTION TABLE --%>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="grdCashHistory" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                    OnRowDataBound="grdCashHistory_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="update_date" HeaderText="Date" />
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("cash_update_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="update_balance" HeaderText="Updated Amount" />
                                        <asp:BoundField DataField="previous_balance" HeaderText="Previous Balance" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <asp:Label ID="lblrecords" Text="* The last 10 records are shown here" class="text-danger" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
