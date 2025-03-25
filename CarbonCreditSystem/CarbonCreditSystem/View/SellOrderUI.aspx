<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="SellOrderUI.aspx.cs" Inherits="CarbonCreditSystem.View.SellOrderUI" %>

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
                    <a href="SellOrderUI.aspx">Sell Order</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Place your Sell Order</div>
                        <div class="card-category">
                            Place your Sell Order to start trading.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Order Quantity: </label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtSellQuantity" type="number" 
                                            step="any" placeholder="Enter Sell Quantity" required>
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Minimum Quantity: </label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtMinQuantity" type="number" step="any" placeholder="Enter Minimum Quantity" required>
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Minimum Price per Carbon Credit: </label>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text">$</span>
                                            <asp:TextBox type="number" class="form-control" runat="server" step="any" ID="txtMinPrice" placeholder="Enter Minimum Price" required>
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Order Trade Type: </label>
                                        <asp:DropDownList class="form-control input-full" ID="ddlOrderType" runat="server" 
                                            OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="card-action">
                                    <%-- PLACE ORDER BUTTON --%>
                                    <asp:Button ID="btnPlaceOrder" runat="server" class="btn btn-success" text="Place Order" OnClick="btnPlaceOrder_Click"/>
                                    <asp:Label ID="lblMsg" class="alert alert-danger" runat="server" Visible="false"></asp:Label>
                                    <asp:HiddenField ID="hdnOrderId" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
