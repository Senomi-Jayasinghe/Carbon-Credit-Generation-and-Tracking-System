<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="AuthorizeCCUI.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="CarbonCreditSystem.View.AuthorizeCCUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-inner">
        <div class="page-header">
            <ul class="breadcrumbs mb-3">
                <li class="nav-home">
                    <a href="AuthorizeUI.aspx">
                        <i class="icon-grid"></i>
                    </a>
                </li>
                <li class="separator">
                    <i class="icon-arrow-right"></i>
                </li>
                <li class="nav-item">
                    <a href="AuthorizeCCUI.aspx">View Details
                    </a>
                </li>
            </ul>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <div class="card-title">
                            <asp:Label ID="lblheader" runat="server" Text="Authorize Carbon Credits" />
                        </div>
                        <div class="card-category">
                            Review and approve carbon credits submitted for authorization.
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form">
                            <div class="row">
                                <div class="col-md-6"> <%--USER DETAILS--%>
                                    <div class="form-group ">
                                        <label for="txtUserID" class="col-md-3 col-form-label">User ID:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtUserID" type="number" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtName" class="col-md-3 col-form-label">User Name:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtName" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group ">
                                        <label for="txtUserNIC" class="col-md-3 col-form-label">User NIC:</label>
                                        <asp:TextBox class="form-control input-full" runat="server" ID="txtUserNIC" type="number" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">   <%--TREE DETAILS--%>
                            <div class="col-md-6">
                                <div class="form-group ">
                                    <div class="input-file input-file-image">
                                        <asp:Image ID="imgTree" runat="server" class="img-upload-preview" Width="190" alt="preview" ImageUrl="~/assets/img/preview.jpg" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group ">
                                    <label for="txtHeight" class="col-md-3 col-form-label">Height (in Feet):</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtHeight" type="number" step="any" ReadOnly="true" placeholder="Enter Height" required></asp:TextBox>
                                </div>
                                <div class="form-group ">
                                    <label for="txtDiameter" class="col-md-3 col-form-label">Diameter/Width (in Inches):</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtDiameter" type="number" step="any" ReadOnly="true" required placeholder="Enter Diameter"></asp:TextBox>
                                </div>
                                <div class="form-group ">
                                    <label for="txtAge" class="col-md-3 col-form-label">Age:</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtAge" type="number" step="any" required ReadOnly="true" placeholder="Enter Age"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-6"> <%--CARBON CREDIT DETAILS--%>
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
                                    <label for="txtCO2SWPYear" class="col-md-3 col-form-label">C02 Sequestered Per Year (Pounds):</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtCO2SWPYear" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="txtCarbonCreditsTonnes" class="col-md-3 col-form-label">
                                        Carbon Credits per Year (1 Carbon Credit = 1 tonne of CO2 Sequestered):</label>
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" runat="server" ID="txtCarbonCreditsTonnes" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group ">
                                    <label for="txtGeneratedDay" class="col-md-3 col-form-label">Generated Day:</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtGeneratedDay" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group ">
                                    <label for="txtExpiryDay" class="col-md-3 col-form-label">Expiry Date:</label>
                                    <asp:TextBox class="form-control input-full" runat="server" ID="txtExpiryDay" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mt-1"> <%--APPROVE AND REJECT BUTTONS--%>
                            <asp:Button runat="server" ID="btnApprove" class="btn btn-success btn-lg" Text="Approve" OnClick="btnApprove_Click" />
                            <asp:Button runat="server" ID="btnReject" class="btn btn-danger btn-lg" Text="Reject" OnClick="btnReject_Click"/>
                            <asp:HiddenField ID="hdnCCGeneratedID" runat="server" />
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <div class="form-group"> <%--DISPLAY TEXTBOX WHEN REJECT BUTTON IS CLICKED--%>
                                    <asp:Label runat="server" ID="lblReject" Visible="false" for="txtRejectReason" class="col-md-3 col-form-label"
                                        Text="Reason for Rejection:"></asp:Label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRejectReason" class="form-control" Visible="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="mt-1"> <%--CONFIRM REJECTION--%>
                            <asp:Button runat="server" ID="btnConfirm" class="btn btn-danger btn-lg" Text="Confirm" Visible="false" OnClick="btnConfirm_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
