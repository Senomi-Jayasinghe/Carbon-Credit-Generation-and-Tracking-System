<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="TradesTableUI.aspx.cs" Inherits="CarbonCreditSystem.View.TradesTableUI" %>

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
                    <a href="OrdersUI.aspx">Track your Trades</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Track your Trades</div>
                            <div class="card-category">
                                View your Trades
                            </div>
                        </div>
                        <button class="btn btn-black h-25" onclick="printDiv('printableContent')">
                            <span class="btn-label">
                                <i class="fa fa-print"></i>
                            </span>
                            Print
                        </button>
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
                                <%-- SEARCH BUTTON --%>
                                <div class="col-md-4">
                                    <label for="btnSearch"></label>
                                    <br />
                                    <asp:Button runat="server" ID="btnSearch" class="btn btn-black" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="grdTrades" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnRowDataBound="grdTrades_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="excecuted_date_time" HeaderText="Excecuted Time" />
                                    <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="total_price" HeaderText="Total Price" />
                                    <asp:BoundField DataField="price_per_cc" HeaderText="Price per Carbon Credit" />
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSource" runat="server" Text='<%# Eval("source_table") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblrecords" Text="* The last 10 records are shown here" class="text-danger" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
