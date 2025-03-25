<%@ Page Title="" Language="C#" MasterPageFile="~/CarbonCreditsMaster.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CarbonCreditSystem.View.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-inner" id="printableContent">
        <div class="d-flex align-items-left align-items-md-center flex-column flex-md-row pt-2 pb-4">
            <div>
                <h3 class="fw-bold mb-3">Dashboard</h3>
                <% if (roleId == 1)
                    { %>
                <h6 class="op-7 mb-2">View real-time carbon credit prices and monitor your wallet balance at a glance.</h6>
                <%} %>
            </div>
        </div>
        <% if (roleId == 1)
            { %>
        <div class="row">
            <%-- CARDS --%>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body ">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div class="icon-big text-center icon-primary bubble-shadow-small">
                                    <i class="fas fa-money-bill-wave"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Cash Wallet</p>
                                    <%-- CASH WALLET--%>
                                    <h4 class="card-title">$<asp:Label ID="lblcash" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div class="icon-big text-center icon-info bubble-shadow-small">
                                    <i class="fas fa-hand-holding-usd"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Carbon Credits</p>
                                    <%--CARBON CREDIT WALLET--%>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblCCBalance" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div class="icon-big text-center icon-success bubble-shadow-small">
                                    <i class="fas fa-luggage-cart"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Sell Orders</p>
                                    <%--SELL ORDERS--%>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblSellOrders" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div class="icon-big text-center icon-secondary bubble-shadow-small">
                                    <i class="far fa-check-circle"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Buy Orders</p>
                                    <%--BUY ORDERS--%>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblBuyOrders" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%} %>
        <%----------------------------------------------------MARKET WATCH/BUYSELL---------------------------------------------------%>
        <div class="row">
            <div class="col-md-12">
                <div class="card card-round">
                    <div class="card-header">
                        <div class="card-head-row">
                            <%--DAILY WATCH--%>
                            <div class="card-title">Daily Carbon Price Watch</div>
                            <div class="card-tools">
                                <button class="btn btn-label-info btn-round btn-sm" onclick="printDiv('printableContent')">
                                    <span class="btn-label">
                                        <i class="fa fa-print"></i>
                                    </span>
                                    Print
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="chart-container" style="min-height: 375px">
                            <asp:Literal ID="ltrDailyChart" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <%--WEEKLY WATCH--%>
                <div class="card card-round">
                    <div class="card-header">
                        <div class="card-head-row">
                            <div class="card-title">Weekly Carbon Price Watch</div>
                        </div>
                        <asp:Label ID="lblWeekRange" runat="server" class="card-category"></asp:Label>
                    </div>
                    <div class="card-body pb-0">
                        <div class="pull-in">
                            <asp:Literal ID="ltrWeeklyChart" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <%--MONTHLY WATCH--%>
                <div class="card card-round">
                    <div class="card-body pb-0">
                        <h4 class="mb-2">Monthly Carbon Price Watch</h4>
                        <div class="pull-in sparkline-fix">
                            <div id="lineChart">
                                <asp:Literal ID="ltrMonthlyChart" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <% if (roleId == 1)
            { %>
        <div class="row">
            <div class="col-md-12">
                <%--BUY/SELL CHART--%>
                <div class="card card-round">
                    <div class="card-header">
                        <div class="card-head-row">
                            <div class="card-title">Your Buy and Sell Orders</div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="chart-container" style="min-height: 375px">
                            <asp:Literal ID="ltrBuySell" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%} %>
    </div>
</asp:Content>
