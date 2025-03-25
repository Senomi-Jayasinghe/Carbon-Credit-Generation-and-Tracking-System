<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="HomeUI.aspx.cs" Inherits="CarbonCreditSystem.View.HomeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-3">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-3 px-1">
                <a href="CalculatorUI.aspx?mode=C">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/Green tea wallpaper.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Carbon Credit Calculator</h5>
                            <p class="card-text">Estimate your carbon footprint and offset emissions easily.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-6 col-lg-3 px-1">
                <a href="TreeRepositoryUI.aspx">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/examples/treerepo.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Your Tree Repository</h5>
                            <p class="card-text">Manage and grow your personal forest to fight climate change.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-6 col-lg-3 px-1">
                <a href="OrdersUI.aspx">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/tradingplatform.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Track Trades</h5>
                            <p class="card-text">A comprehensive trading platform for buying and selling carbon credits.</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-3 px-1">
                <a href="TrackCarbonCreditsUI.aspx">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/examples/Reports.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Track your Carbon Credits</h5>
                            <p class="card-text">Track your carbon credit transactions and performance.</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-6 col-lg-3 px-1">
                <a href="BankDetailsUI.aspx">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/examples/product4.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Register your Bank Account</h5>
                            <p class="card-text">Register your bank account to perform transactions</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-6 col-lg-3 px-1">
                <a href="EditProfile.aspx">
                    <div class="card m-3" style="width: 18rem; height: 22rem;">
                        <img class="card-img-top" style="height: 11rem;" src='<%= ResolveUrl("~/assets/img/examples/profile.jpg") %>'>
                        <div class="card-body">
                            <h5 class="card-title mb-2 fw-mediumbold">Your Profile</h5>
                            <p class="card-text">Take a look at your profile!</p>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>
</asp:Content>
