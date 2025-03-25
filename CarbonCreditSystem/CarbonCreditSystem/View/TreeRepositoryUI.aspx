<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="TreeRepositoryUI.aspx.cs" Inherits="CarbonCreditSystem.View.TreeRepositoryUI" %>

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
                    <a href="TreeDetailsUI.aspx">Tree Repository</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Tree Repository</div>
                            <div class="card-category">Keep track of your Trees Here</div>
                        </div>
                        <div>
                            <%-- PRINTABLE CONTENT --%>
                            <button class="btn btn-black" onclick="printDiv('printableContent')"> 
                                <span class="btn-label">
                                    <i class="fa fa-print"></i>
                                </span>
                               Print  <%--PRINT BUTTON--%> 
                            </button>
                            <a href="TreeDetailsUI.aspx" class="btn btn-success">
                                <%-- ADD NEW TREE --%>
                                <span class="btn-label">
                                    <i class="fa fa-plus"></i>
                                </span>
                                Add Tree
                            </a>
                        </div>
                    </div>
                    <div class="card-body">
                        <%-- TREE REPOSITORY --%>
                        <asp:HiddenField ID="hdnTreeId" runat="server" />
                        <div class="table-responsive">
                            <asp:GridView ID="grdTrees" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnSelectedIndexChanging="grdTrees_SelectedIndexChanging" OnRowDeleting="grdTrees_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="tree_name" HeaderText="Name" />
                                    <asp:BoundField DataField="tree_location" HeaderText="Geo Location" />
                                    <asp:BoundField DataField="tree_width" HeaderText="Width" />
                                    <asp:BoundField DataField="tree_height" HeaderText="Height" />
                                    <asp:BoundField DataField="tree_age" HeaderText="Age" />
                                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnGrdTreeId" runat="server" ClientIDMode="Predictable" Value='<%# Bind("tree_id") %>' />
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
