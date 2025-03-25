<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="ReportsCashWalletUI.aspx.cs" Inherits="CarbonCreditSystem.View.ReportsCashWalletUI" %>

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
                    <a href="ReportsCashWalletUI.aspx">Cash Wallet
                    </a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Cash Transactions Report" />
                        </div>
                        <div class="card-category">
                            View a detailed report of all cash transactions, including amounts, dates, and transaction types.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="container-fluid mb-2">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="txtFrom">From:</label>
                                    <asp:TextBox ID="txtFrom" runat="server" type="date" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="txtFrom">To:</label>
                                    <asp:TextBox ID="txtTo" runat="server" type="date" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label for="btnSearch"></label>
                                    <br />
                                    <asp:Button runat="server" ID="btnSearch" class="btn btn-black" Text="Search" OnClick="btnSearch_Click" />
                                    <button class="btn btn-black" onclick="printDiv('printableContent')">
                                        <span class="btn-label">
                                            <i class="fa fa-print"></i>
                                        </span>
                                        Print
                                    </button>
                                </div>
                            </div>
                        </div>
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
                                            <div class="col-7 col-stats">
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

                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="grdCashHistory" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                    OnRowDataBound="grdCashHistory_RowDataBound" Width="80%">
                                    <Columns>
                                        <asp:BoundField DataField="update_date" HeaderText="Date" />
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("cash_update_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="update_balance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderText="Updated Amount" />
                                        <asp:BoundField DataField="previous_balance" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderText="Previous Balance" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
