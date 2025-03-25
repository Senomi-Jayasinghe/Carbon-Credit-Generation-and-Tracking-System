<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="CalculatorUI.aspx.cs" Inherits="CarbonCreditSystem.View.CalculatorUI" MaintainScrollPositionOnPostback="true" %>

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
                    <asp:HyperLink ID="navLink" runat="server" Text="Calculator"></asp:HyperLink>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Carbon Credit Calculator" />
                        </div>
                        <div class="card-category">
                            A Carbon Credit Calculator helps you estimate the Carbon Sequestration of your trees.
                            <br />
                            Enter the Height, Diameter(Width), and Age of your tree.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdnTreeID" />
                                <% if (Mode != "C")
                                    { %>
                                <div class="col-md-6">
                                    <%-- ONLY ALLOW THE USER TO SELECT A TREE IF THEY NEED TO GENERATE CARBON CREDITS --%>
                                    <div class="form-group ">
                                        <asp:Label runat="server" class="col-md-3 col-form-label" Visible="false" ID="lblDDLText">
                                            Select Tree from Tree Repository</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlTrees" class="form-select form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlTrees_SelectedIndexChanged" Visible="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group ">
                                        <div class="input-file input-file-image">
                                            <asp:Image ID="imgTree" runat="server" class="img-upload-preview" Width="190" alt="preview" ImageUrl="~/assets/img/preview.jpg" />
                                        </div>
                                    </div>
                                </div>
                                <%} %>
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label for="txtHeight" class="col-md-3 col-form-label">Height (in Feet):</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtHeight" type="number" step="any" placeholder="Enter Height" 
                                            required></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtDiameter" class="col-md-3 col-form-label">Diameter/Width (in Inches):</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtDiameter" type="number" step="any" required 
                                            placeholder="Enter Diameter"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtAge" class="col-md-3 col-form-label">Age:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtAge" type="number" step="any" required 
                                            placeholder="Enter Age"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="card-action">
                                    <asp:Button ID="btnCalculate" runat="server" class="btn btn-success" Text="Calculate" OnClick="btnCalculate_Click" />
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label for="txtGreenW" class="col-md-3 col-form-label">Total Green Weight:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtGreenW" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtCarbonW" class="col-md-3 col-form-label">Carbon Weight:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtCarbonW" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label for="txtDryW" class="col-md-3 col-form-label">Dry Weight:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtDryW" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtCO2SW" class="col-md-3 col-form-label">Weight of CO2 Sequestered (Pounds):</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtCO2SW" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group ">
                                        <label for="txtCO2SWPYear" class="col-md-3 col-form-label">C02 Sequestered Per Year (Pounds)</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtCO2SWPYear" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-6 ms-auto me-auto">
                                    <label for="txtCarbonCreditsTonnes" class="col-md-3 col-form-label">Carbon Credits per Year (1 Carbon Credit = 1 tonne of CO2 Sequestered):</label>
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" runat="server" ID="txtCarbonCreditsTonnes" aria-describedby="basic-addon1" ReadOnly="true"></asp:TextBox>
                                        <%-- SHOW ADD TO WALLET BUTTON ONLY IF THE uSER NEED TO GENERATE CARBON CREDITS--%>
                                        <asp:Button class="btn btn-success btn-border" ID="btnAdd" type="button" runat="server" Text="Add to Wallet" Visible="false" 
                                            OnClick="btnAdd_Click" />
                                        <asp:HiddenField ID="hdnCCGeneratedID" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <%-- SHOW REJECT REASON IF AUTHORIZED CARBON CREDITS WERE REJECTED --%>
                                        <asp:Label runat="server" ID="lblReject" Visible="false" for="txtRejectReason" class="col-md-3 col-form-label"
                                            Text="Reason for Rejection:"></asp:Label>
                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRejectReason" class="form-control" Visible="false" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
