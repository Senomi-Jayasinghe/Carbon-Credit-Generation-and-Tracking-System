<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="TreeDetailsUI.aspx.cs" Inherits="CarbonCreditSystem.View.TreeDetailsUI" %>

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
                    <a href="TreeDetailsUI.aspx">Tree Details</a>
                </li>
            </ul>
        </div>
        <div class="row">
            <ul class="nav nav-pills nav-secondary mx-3" id="pills-tab" role="tablist">
                <li class="nav-item" role="presentation">
                    <a class="nav-link" id="pills-home-tab" data-bs-toggle="pill" href="#pills-home" role="tab" aria-controls="pills-home" aria-selected="false" tabindex="-1">Manually Enter Tree Width and Height </a>
                </li>
                <li class="nav-item submenu" role="presentation">
                    <a class="nav-link" id="pills-profile-tab" data-bs-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="false" tabindex="-1">Capture Tree Width and Height Automatically from Image</a>
                </li>
            </ul>
            <div>
                <div class="card" style="min-height: 600px; min-width: 100%">
                    <div class="card-header">
                        <div class="card-title">Tree Details</div>
                    </div>
                    <div class="card-body">
                        <div></div>
                        <div class="tab-content mt-2 mb-3" id="pills-tabContent">

                            <div class="form tab-pane fade active show" id="pills-home" role="tabpanel" aria-labelledby="pills-home-tab">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Name:</label>
                                            <asp:TextBox class="form-control input-full" runat="server" ID="txtTreeName" required placeholder="E.g., Mango"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Geo Location:</label>
                                            <asp:TextBox class="form-control input-full" runat="server" ID="txtLocation" required placeholder="Enter Geo Location"></asp:TextBox>
                                        </div>
                                        <div class="form-group ">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Height (in Feet):</label>
                                            <asp:TextBox class="form-control input-full" runat="server" ID="txtHeight" type="number" step="any" placeholder="Enter Height"
                                                required>
                                            </asp:TextBox>
                                        </div>
                                        <div class="form-group ">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Diameter/Width (in Inches):</label>
                                            <asp:TextBox class="form-control input-full" runat="server" ID="txtDiameter" type="number" step="any" required
                                                placeholder="Enter Diameter">
                                            </asp:TextBox>
                                        </div>
                                        <div class="form-group ">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Age:</label>
                                            <asp:TextBox class="form-control input-full" runat="server" ID="txtAge" type="number" step="any" required placeholder="Enter Age">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- UPLOAD IMAGE --%>
                                    <div class="col-md-6">
                                        <div class="form-group ">
                                            <label for="inlineinput" class="col-md-3 col-form-label">Upload Picture:</label>
                                            <div class="input-file input-file-image">
                                                <asp:Image ID="imgTree" runat="server" class="img-upload-preview" Width="190" alt="preview" ImageUrl="~/assets/img/preview.jpg" />
                                                <asp:FileUpload runat="server" class="form-control form-control-file" ID="upTree" ClientIDMode="Static" accept="image/*" />
                                                <label for="upTree" class="label-input-file btn btn-black btn-round">
                                                    <span class="btn-label">
                                                        <i class="fa fa-file-image"></i>
                                                    </span>
                                                    Upload a Image
                                                </label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="card-action">
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Label ID="lblPnlMsg" runat="server" Text="Label" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnTreeId" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="form tab-pane fade" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                                <iframe src="http://127.0.0.1:5000?id=<%=UserID%>" style="height: 600px; width: 1000px; overflow: scroll"></iframe>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
