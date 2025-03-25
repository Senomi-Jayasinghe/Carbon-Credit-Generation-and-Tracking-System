using CarbonCreditSystem.Controller;
using CarbonCreditSystem.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace CarbonCreditSystem.View
{
    public partial class Dashboard : System.Web.UI.Page
    {   
        public int roleId {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            roleId = Convert.ToInt32(Session["USER_ROLE"]);
            int user_id = Convert.ToInt32(Session["USER_ID"]);
            //LOAD CHARTS
            SetLabels(user_id);
            DailyWatch();
            WeeklyWatch();
            MonthlyWatch();
            BuySellGraph(user_id);
        }

        protected void DailyWatch()
        { 
            ReportController reportController = new ReportController(); // Initialize the controller and fetch the data
            string[] dates = reportController.getPriceWatchDailyDates(); // Extract dates and prices
            object[] prices = reportController.getPriceWatchDailyPrices();
            Highcharts chart = new Highcharts("dailychart") // Initialize the chart
            .InitChart(new Chart { PlotShadow = false })
            .SetTitle(new DotNet.Highcharts.Options.Title { Text = "Daily Carbon Credit Price Watch" })
            .SetTooltip(new Tooltip
            {
                Formatter = "function() { return '<b>' + this.x + '</b>: ' + this.y + ' USD'; }",
                Shared = true // Shows data when hovered over
            })
            .SetXAxis(new XAxis
            {
                Categories = dates, // Set the date labels for the x-axis from the data
                Title = new XAxisTitle { Text = "Date" }
            })
            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle { Text = "Price (USD)" }
            })
            .SetPlotOptions(new PlotOptions
            {
                Line = new PlotOptionsLine
                {
                    DataLabels = new PlotOptionsLineDataLabels
                    {
                        Enabled = false // Disable data labels by default
                    },
                    EnableMouseTracking = true // Enable the hover effect
                }
            })
            .SetSeries(new Series
            {
                Type = ChartTypes.Line,
                Name = "Carbon Credit Price",
                Data = new Data(prices) // Set the price data for the chart
            });

            // Render the chart
            ltrDailyChart.Text = chart.ToHtmlString();
        }

        protected void WeeklyWatch()
        {
            ReportController reportController = new ReportController();
            string[] dates = reportController.getPriceWatchWeeklyDates();// Extract dates and prices
            object[] prices = reportController.getPriceWatchWeeklyPrices();
            Highcharts chart = new Highcharts("weeklychart")// Initialize the chart
            .InitChart(new Chart
            {
                PlotShadow = false,
            })
            .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
            .SetTooltip(new Tooltip
            {
                Formatter = "function() { return '<b>' + this.x + '</b>: ' + this.y + ' USD'; }",
                Shared = true // Shows data when hovered over
            })
            .SetXAxis(new XAxis
            {
                Categories = dates, // Set the date labels for the x-axis from the data
                Title = new XAxisTitle { Text = "" }
            })
            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle { Text = "" }
            })
            .SetPlotOptions(new PlotOptions
            {
                Line = new PlotOptionsLine
                {
                    DataLabels = new PlotOptionsLineDataLabels
                    {
                        Enabled = false // Disable data labels by default
                    },
                    EnableMouseTracking = true // Enable the hover effect
                }
            })
            .SetSeries(new Series
            {
                Type = ChartTypes.Spline,
                Name = "Carbon Credit Price",
                Data = new Data(prices) // Set the price data for the chart
            });
            ltrWeeklyChart.Text = chart.ToHtmlString(); // Render the chart
        }

        protected void MonthlyWatch()
        {
            ReportController reportController = new ReportController();
            string[] dates = reportController.getPriceWatchMonthlyDates(); // Extract dates and prices
            object[] prices = reportController.getPriceWatchMonthlyPrices();
            Highcharts chart = new Highcharts("monthlychart") // Initialize the chart
            .InitChart(new Chart
            {
                PlotShadow = false,
            })
            .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
            .SetTooltip(new Tooltip
            {
                Formatter = "function() { return '<b>' + this.x + '</b>: ' + this.y + ' USD'; }",
                Shared = true // Shows data when hovered over
            })
            .SetXAxis(new XAxis
            {
                Categories = dates, // Set the date labels for the x-axis from the data
                Title = new XAxisTitle { Text = "" }
            })
            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle { Text = "" }
            })
            .SetPlotOptions(new PlotOptions
            {
                Line = new PlotOptionsLine
                {
                    DataLabels = new PlotOptionsLineDataLabels
                    {
                        Enabled = false // Disable data labels by default
                    },
                    EnableMouseTracking = true // Enable the hover effect
                }
            })
            .SetSeries(new Series
            {
                Type = ChartTypes.Spline,
                Name = "Carbon Credit Price",
                Data = new Data(prices) // Set the price data for the chart
            });
            ltrMonthlyChart.Text = chart.ToHtmlString(); // Render the chart
        }

        protected void BuySellGraph(int user_id)
        {
            ReportController reportController = new ReportController();
            string[] months = getBuySellMonthlyDates(); // Extract month names for the x-axis

            object[] buyOrders = reportController.getMonthlyBuyOrderCounts(user_id); // Get monthly counts
            object[] sellOrders = reportController.getMonthlySellOrderCounts(user_id);

            var seriesData = new List<Series> // Create a list for series
            {
                new Series
                {
                    Type = ChartTypes.Column,
                    Name = "Buy Orders",
                    Data = new Data(buyOrders) // Set the buy order data
                },
                new Series
                {
                    Type = ChartTypes.Column,
                    Name = "Sell Orders",
                    Data = new Data(sellOrders) // Set the sell order data
                }
            };
            
            Highcharts chart = new Highcharts("buysellchart") // Initialize the chart
            .InitChart(new Chart
            {
                PlotShadow = false,
            })
            .SetTitle(new DotNet.Highcharts.Options.Title { Text = "" })
            .SetTooltip(new Tooltip
            {
                Formatter = "function() { return '<b>' + this.x + '</b>: ' + this.y + ' orders'; }",
                Shared = true // Shows data when hovered over
            })
            .SetXAxis(new XAxis
            {
                Categories = months, // Set the month labels for the x-axis
                Title = new XAxisTitle { Text = "Month" }
            })
            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle { Text = "Number of Orders" }
            })
            .SetPlotOptions(new PlotOptions
            {
                Column = new PlotOptionsColumn
                {
                    DataLabels = new PlotOptionsColumnDataLabels
                    {
                        Enabled = true // Show data labels
                    },
                    EnableMouseTracking = true // Enable the hover effect
                }
            })
            .SetSeries(seriesData.ToArray()); // Set the series data

            // Render the chart
            ltrBuySell.Text = chart.ToHtmlString();
        }


        protected void SetLabels(int user_id)
        {
            DateTime today = DateTime.Today;

            // Calculate the start date as 7 days before today
            DateTime weekStart = today.AddDays(-6);

            // Format the dates as "Month day"
            string formattedRange = $"{weekStart:MMMM dd} - {today:MMMM dd}";

            // Assuming lblWeekRange is a Label control to display the date range
            lblWeekRange.Text = formattedRange;

            CashWalletController cashWalletController = new CashWalletController();
            lblcash.Text = cashWalletController.getBalance(user_id).ToString();

            CarbonCreditWalletController carbonCreditWalletController = new CarbonCreditWalletController();
            double balance = carbonCreditWalletController.getBalance(user_id);
            lblCCBalance.Text = (Math.Round(balance, 8)).ToString();

            ReportController reportController = new ReportController();
            lblSellOrders.Text = reportController.getSellOrders(user_id);
            lblBuyOrders.Text = reportController.getBuyOrders(user_id);
        }

        protected string[] getBuySellMonthlyDates()
        {
            // Assuming you want the names of the months for the current year
            return new string[]
            {
            "J", "F", "M", "A", "M", "J",
            "J", "A", "S", "O", "N", "D"
            };
        }
    }
}