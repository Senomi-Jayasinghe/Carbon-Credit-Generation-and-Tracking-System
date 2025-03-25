<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="OrdersUI.aspx.cs" Inherits="CarbonCreditSystem.View.OrdersUI" %>

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
                    <a href="OrdersUI.aspx">Track your Orders</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Track your Orders</div>
                            <div class="card-category">
                                Monitor the status of all your purchase and sales transactions in one place.
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
                        <%--SEARCH--%>
                        <div class="container-fluid mb-2">
                            <div class="row">
                                <div class="col-md-2">
                                    <label for="ddlStatus">Status:</label>
                                    <asp:DropDownList class="form-control" ID="ddlStatus" runat="server">
                                        <asp:ListItem Text="All Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Placed" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Excecuted" Value="E"></asp:ListItem>
                                        <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Expired" Value="X"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label for="ddlOrder">Type:</label>
                                    <asp:DropDownList class="form-control" ID="ddlOrder" runat="server">
                                        <asp:ListItem Text="All" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Buy Order" Value="Buy Order"></asp:ListItem>
                                        <asp:ListItem Text="Sell Order" Value="Sell Order"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3">
                                    <label for="txtFrom">From:</label>
                                    <asp:TextBox ID="txtFrom" runat="server" type="date" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtFrom">To:</label>
                                    <asp:TextBox ID="txtTo" runat="server" type="date" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="btnSearch"></label>
                                    <br />
                                    <asp:Button runat="server" ID="btnSearch" class="btn btn-black" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <%--TABLE--%>
                        <div class="table-responsive">
                            <asp:GridView ID="grdOrders" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnRowDataBound="grdOrders_RowDataBound" OnRowDeleting="grdOrders_RowDeleting" DataKeyNames="order_type">
                                <Columns>
                                    <asp:BoundField DataField="order_date_time" HeaderText="Order Day" />
                                    <asp:BoundField DataField="balance_quantity" HeaderText="Balance" />
                                    <asp:TemplateField HeaderText="Trade Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrade" runat="server" Text='<%# Eval("order_trade_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="expire_time" HeaderText="Expiry Day" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrder" runat="server" Text='<%# Eval("order_type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Delete" Text="Cancel" Visible="True">
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnOrderId" runat="server" ClientIDMode="Predictable" Value='<%# Bind("order_id") %>' />
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
