<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="TrackCarbonCreditsUI.aspx.cs" Inherits="CarbonCreditSystem.View.TrackCarbonCredits" %>

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
                    <a href="TrackCarbonCreditsUI.aspx">Track Carbon Credits</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card" id="printableContent">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Track Carbon Credits</div>
                            <div class="card-category">Keep track of your generated Carbon Credits here.</div>
                        </div>
                        <div>
                            <asp:Button runat="server" ID="btnGenerate" class="btn btn-success" OnClick="btnGenerate_Click" Text="Generate Carbon Credits" />
                        </div>

                    </div>
                    <div class="card-body">
                        <%-- SEARCH --%>
                        <div class="container-fluid mb-2">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="ddlStatus">Status:</label>
                                    <asp:DropDownList class="form-control" ID="ddlStatus" runat="server">
                                        <asp:ListItem Text="All Status" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="P"></asp:ListItem>
                                        <asp:ListItem Text="Accepted" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>
                                        <asp:ListItem Text="Expired" Value="X"></asp:ListItem>
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
                                <div class="col-md-3"><%-- SEARCH BUTTON--%>
                                    <label for="btnSearch"></label>
                                    <br />
                                    <asp:Button runat="server" ID="btnSearch" class="btn btn-black" Text="Search" OnClick="btnSearch_Click" />
                                    <%-- PRINT BUTTON--%>
                                    <button class="btn btn-black" onclick="printDiv('printableContent')">
                                        <span class="btn-label">
                                            <i class="fa fa-print"></i>
                                        </span>
                                        Print
                                    </button>
                                </div>
                            </div>
                        </div>
                        <%-- TABLE --%>
                        <asp:HiddenField ID="hdnCCId" runat="server" />
                        <div class="table-responsive">
                            <asp:GridView ID="grdCarbonCredits" runat="server" class="table table table-hover" AutoGenerateColumns="False"
                                OnSelectedIndexChanging="grdCarbonCredits_SelectedIndexChanging" OnRowDeleting="grdCarbonCredits_RowDeleting"
                                OnRowDataBound="grdCarbonCredits_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="cc_generated" HeaderText="Amount Generated" />
                                    <asp:BoundField DataField="entry_date" HeaderText="Generated Day" />
                                    <asp:BoundField DataField="cc_expiredate" HeaderText="Expiry Date" />
                                    <asp:TemplateField HeaderText="Authorization Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("cc_authorizedStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="View" ShowSelectButton="True" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" Text="Delete" Visible="True"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnGrdCCId" runat="server" ClientIDMode="Predictable" Value='<%# Bind("cc_generated_id") %>' />
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
