<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPasswordUI.aspx.cs" Inherits="CarbonCreditSystem.View.ForgotPasswordUI" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Login Page</title>
    <script src="<%= ResolveUrl("~/assets/js/plugin/webfont/webfont.min.js") %>"></script>
    <script>
        WebFont.load({
            google: { families: ["Public Sans:300,400,500,600,700"] },
            custom: {
                families: [
                    "Font Awesome 5 Solid",
                    "Font Awesome 5 Regular",
                    "Font Awesome 5 Brands",
                    "simple-line-icons",
                ],
                urls: ["<%= ResolveUrl("~/assets/css/fonts.min.css") %>"]
            },
            active: function () {
                sessionStorage.fonts = true;
            },
        });
    </script>
    <style>
        body, html {
            height: 100%;
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            font-family: 'Public Sans', sans-serif;
            background-color: transparent !important;
        }

        html {
            background-image: url('<%= ResolveUrl("~/assets/img/Trees.jpg") %>');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
        }

        .card {
            width: 30rem;
            background-color: rgba(255, 255, 255, 0.9) !important;
            padding: 5px;
            border-radius: 10px;
        }
    </style>
    <link href="<%= ResolveUrl("~/assets/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/assets/css/plugins.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/assets/css/kaiadmin.min.css") %>" rel="stylesheet" />
</head>
<body>
    <div class="card mb-0">
        <div class="card-header pb-0 text-left">
            <h3 class="fw-bolder text-primary">Forgot your Password?</h3>
            <p class="mb-0">Enter your Email Address, we will send you a new password.</p>
        </div>
        <div class="card-body pb-3">
            <form id="frmLogin" runat="server">
                <label>Email</label>
                <div class="input-group mb-1">
                    <asp:TextBox ID="txtEmail" runat="server" type="email" class="form-control" placeholder="Email" aria-label="Email"
                        aria-describedby="email-addon"></asp:TextBox>
                </div>
                <asp:Label runat="server" class="text-danger" ID="lblMsg" Visible="false" Text="We sent you a new password"></asp:Label>
                <div class="text-center d-flex justify-content-between">
                    <asp:Button class="btn btn-primary w-100 mt-4 mb-0 me-2" Text="Send" runat="server" ID="btnSend" OnClick="btnSend_Click"/>
                    <a href="LoginUI.aspx" class="btn btn-primary btn-border w-100 mt-4 mb-0">Back</a>
                </div>
            </form>
        </div>
    </div>
    <script src="<%= ResolveUrl("~/assets/js/core/jquery-3.7.1.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/js/core/popper.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/js/core/bootstrap.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/assets/js/kaiadmin.min.js") %>"></script>
</body>
</html>

