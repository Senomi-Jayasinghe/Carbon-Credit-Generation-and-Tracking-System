<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="UpdatePasswordUI.aspx.cs" Inherits="CarbonCreditSystem.View.UpdatePassword" %>

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
                    <a href="UpdatePasswordUI.aspx">Update Password</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Update Password</div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Old Password:</label>
                                        <asp:TextBox class="form-control input-full" ID="txtOldPsw" runat="server" required></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">New Password:</label>
                                        <asp:TextBox class="form-control input-full" ID="txtNewPsw" runat="server" required></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="inlineinput" class="col-md-3 col-form-label">Confirm Password:</label>
                                        <asp:TextBox class="form-control input-full" ID="txtConfirmPsw" runat="server" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class="card-action">
                                    <asp:Button ID="btnUpdate" runat="server" class="btn btn-success" Text="Update" OnClick="btnUpdate_Click" />
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
