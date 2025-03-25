<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="CarbonCreditWalletUI.aspx.cs" Inherits="CarbonCreditSystem.View.CarbonCreditWalletUI" %>

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
                    <a href="CarbonCreditWalletUI.aspx">Carbon Credit Wallet
                    </a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Carbon Credit Wallet" />
                        </div>
                        <div class="card-category">
                            Easily track and manage your earned carbon credits in one place.
                        </div>
                    </div>
                    <div class="card-body">
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
                                                <%-- CARBON CREDIT WALLET CARD --%>
                                                <p class="card-category">Carbon Credits</p>
                                                <h4 class="card-title">
                                                    <asp:Label runat="server" ID="lblCCBalance"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- TRANSACTION HISTORY TABLE --%>
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
                        <asp:Label ID="lblrecords" Text="* The last 10 records are shown here" class="text-danger" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
