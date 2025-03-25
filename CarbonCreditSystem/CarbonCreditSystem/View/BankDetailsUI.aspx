<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="BankDetailsUI.aspx.cs" Inherits="CarbonCreditSystem.View.BankDetailsUI" %>

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
                    <a href="BankDetailsUI.aspx">Bank Details</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Register your Bank Account</div>
                        <div class="card-category">
                            Register your bank account to perform transactions.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Bank Account No: </label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtBankAccountNo" type="number" placeholder="Enter Bank Account No"
                                            required></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Bank Branch: </label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" required>
                                            <asp:ListItem Text="Select Branch" Value="0" />
                                            <asp:ListItem Text="Ampara" Value="Ampara" />
                                            <asp:ListItem Text="Anuradhapura" Value="Anuradhapura" />
                                            <asp:ListItem Text="Badulla" Value="Badulla" />
                                            <asp:ListItem Text="Batticaloa" Value="Batticaloa" />
                                            <asp:ListItem Text="Colombo" Value="Colombo" />
                                            <asp:ListItem Text="Galle" Value="Galle" />
                                            <asp:ListItem Text="Gampaha" Value="Gampaha" />
                                            <asp:ListItem Text="Hambantota" Value="Hambantota" />
                                            <asp:ListItem Text="Jaffna" Value="Jaffna" />
                                            <asp:ListItem Text="Kalutara" Value="Kalutara" />
                                            <asp:ListItem Text="Kandy" Value="Kandy" />
                                            <asp:ListItem Text="Kegalle" Value="Kegalle" />
                                            <asp:ListItem Text="Kilinochchi" Value="Kilinochchi" />
                                            <asp:ListItem Text="Kurunegala" Value="Kurunegala" />
                                            <asp:ListItem Text="Mannar" Value="Mannar" />
                                            <asp:ListItem Text="Matale" Value="Matale" />
                                            <asp:ListItem Text="Matara" Value="Matara" />
                                            <asp:ListItem Text="Monaragala" Value="Monaragala" />
                                            <asp:ListItem Text="Mullaitivu" Value="Mullaitivu" />
                                            <asp:ListItem Text="Nuwara Eliya" Value="Nuwara Eliya" />
                                            <asp:ListItem Text="Polonnaruwa" Value="Polonnaruwa" />
                                            <asp:ListItem Text="Puttalam" Value="Puttalam" />
                                            <asp:ListItem Text="Ratnapura" Value="Ratnapura" />
                                            <asp:ListItem Text="Trincomalee" Value="Trincomalee" />
                                            <asp:ListItem Text="Vavuniya" Value="Vavuniya" />
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Bank Name:</label>
                                        <asp:DropDownList class="form-control input-full" ID="ddlBankName" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="card-action">
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
