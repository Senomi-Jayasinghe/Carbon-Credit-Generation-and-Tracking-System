<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="testingmatchalgo.aspx.cs" Inherits="CarbonCreditSystem.View.testingmatchalgo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header d-flex justify-content-between">
                        <div>
                            <div class="card-title">Trade Matching Algorithm</div>
                            <div class="card-category">Manually Run the Trade Matching Algorithm</div>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:Button runat="server" class="btn btn-success" ID="btnMatch" OnClick="btnMatch_Click" Text="Match" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
