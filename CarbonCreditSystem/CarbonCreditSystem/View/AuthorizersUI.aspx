<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="AuthorizersUI.aspx.cs" Inherits="CarbonCreditSystem.View.AuthorizersUI" %>

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
                    <a href="AuthorizersUI.aspx">Manage Authorizers</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Manage Authorizers</div>
                            <div class="card-category">Easily oversee and manage the list of authorizers.</div>
                        </div>
                        <div>
                            <asp:Button runat="server" ID="btnAdd" class="btn btn-success" Text="Add Authorizer" OnClick="btnAdd_Click" />
                        </div>

                    </div>
                    <div class="card-body"> <%--AUTHORIZERS TABLE--%>
                        <asp:HiddenField ID="hdnAuthorizerId" runat="server" />
                        <div class="table-responsive">
                            <asp:GridView ID="grdAuthorizers" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnSelectedIndexChanging="grdAuthorizers_SelectedIndexChanging" OnRowDeleting="grdAuthorizers_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="user_fullname" HeaderText="Name" />
                                    <asp:BoundField DataField="user_mobileno" HeaderText="Mobile No" />
                                    <asp:BoundField DataField="user_email" HeaderText="Email" />
                                    <asp:BoundField DataField="user_nic" HeaderText="NIC" />
                                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnGrdAuthorizerId" runat="server" ClientIDMode="Predictable" Value='<%# Bind("user_id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
