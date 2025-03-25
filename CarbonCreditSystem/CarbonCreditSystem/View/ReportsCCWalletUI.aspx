<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="ReportsCCWalletUI.aspx.cs" Inherits="CarbonCreditSystem.View.ReportsCCWalletUI" %>

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
                    <a href="ReportsCCWalletUI.aspx">Carbon Credit Wallet
                    </a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Carbon Credit Wallet" />
                        </div>
                        <div class="card-category">
                            Access a comprehensive report of all carbon credit transactions, detailing quantities, values, and transaction dates.
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
                                                <p class="card-category">Carbon Credits</p>
                                                <h4 class="card-title">
                                                    <asp:Label runat="server" ID="lblCCBalance"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <asp:GridView ID="grdCarbonCreditHistory" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                    OnRowDataBound="grdCarbonCreditHistory_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="update_date" HeaderText="Date" />
                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Eval("cc_update_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="update_quantity" HeaderText="Updated Quantity" />
                                        <asp:BoundField DataField="previous_cc_balance" HeaderText="Previous Balance" />
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
