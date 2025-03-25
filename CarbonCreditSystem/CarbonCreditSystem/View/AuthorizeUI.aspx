<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="AuthorizeUI.aspx.cs" Inherits="CarbonCreditSystem.View.AuthorizeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-inner">
        <div class="page-header">
            <ul class="breadcrumbs mb-3">
                <li class="nav-home">
                    <a href="AdminHomeUI.aspx">
                        <i class="icon-home"></i>
                    </a>
                </li>
                <li class="separator">
                    <i class="icon-arrow-right"></i>
                </li>
                <li class="nav-item">
                    <a href="AuthorizeUI.aspx">Authorize Carbon Credits</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">Authorize Carbon Credits</div>
                        <div class="card-category">Review and approve carbon credits submitted for authorization.</div>
                    </div>
                    <div class="card-body">  <%--DISPLAY THE CARBON CREDITS THAT ARE TO BE AUTHORIZED--%>
                        <asp:HiddenField ID="hdnCCId" runat="server" />
                        <div class="table-responsive">
                            <asp:GridView ID="grdCarbonCredits" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnSelectedIndexChanging="grdCarbonCredits_SelectedIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="user_id" HeaderText="User ID" />
                                    <asp:BoundField DataField="cc_generated" HeaderText="Amount Generated" />
                                    <asp:BoundField DataField="entry_date" HeaderText="Generated Day" />
                                    <asp:BoundField DataField="cc_expiredate" HeaderText="Expiry Date" />
                                    <asp:CommandField SelectText="Authorize" ShowSelectButton="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnGrdCCId" runat="server" ClientIDMode="Predictable" Value='<%# Bind("cc_generated_id") %>' />
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
