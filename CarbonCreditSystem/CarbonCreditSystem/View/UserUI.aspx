<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="UserUI.aspx.cs" Inherits="CarbonCreditSystem.View.UserUI" MaintainScrollPositionOnPostback="true" %>

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
                    <a href="UserUI.aspx">User Details</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">User Details</div>
                        <div class="card-category">
                            Enter your Details
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Title:</label>
                                        <asp:DropDownList class="form-control input-full" ID="ddlTitle" runat="server" required></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">First Name:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtFirstName" required placeholder="FirstName"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Last Name:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtLastName" required 
                                            placeholder="LastName" OnTextChanged="txtLastName_TextChanged" AutoPostBack="True">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Full Name:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtFullName" required placeholder="FullName"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Address:</label>
                                        <asp:TextBox class="form-control input-full" TextMode="MultiLine" runat="server" ID="txtAddress" required
                                            placeholder="Address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Telephone No:</label>
                                        <asp:TextBox ID="txtTelephoneNo" runat="server" class="form-control input-full" pattern="^0\d{9}$"
                                            placeholder="TelephoneNo" title="Please enter a telephone number starting with '0' followed by nine digits."></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Mobile No:</label>
                                        <asp:TextBox ID="txtMobileNo" runat="server" class="form-control input-full" pattern="^0\d{9}$"
                                            placeholder="MobileNo" title="Please enter a mobile number starting with '0' followed by nine digits."></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Email:</label>
                                        <asp:TextBox class="form-control input-full" type="email" runat="server" ID="txtEmail" required placeholder="Email"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">NIC:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtNIC" required placeholder="NIC"></asp:TextBox>
                                    </div>
                                    
                                </div>
                                <div class="card-action">
                                    <asp:HiddenField ID="hdnUserId" runat="server" />
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" OnClick="btnSave_Click"/>
                                    <asp:Label ID="lblPnlMsg" runat="server" Text="Label" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
