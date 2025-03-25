<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUI.aspx.cs" Inherits="CarbonCreditSystem.View.LoginUI" %>

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
    <!-- CSS Files -->
    <link href="<%= ResolveUrl("~/assets/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/assets/css/plugins.min.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/assets/css/kaiadmin.min.css") %>" rel="stylesheet" />
</head>
<body> <%--LOGIN PAGE--%>
    <div class="card mb-0">
        <div class="card-header pb-0 text-left">
            <h3 class="fw-bolder text-primary">Welcome Back :)</h3>
            <p class="mb-0">Empowering you to make a difference, one tree at a time.</p>
        </div>
        <div class="card-body pb-3">
            <form id="frmLogin" runat="server">
                <label>Email</label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" type="email" class="form-control" placeholder="Email" aria-label="Email"
                        aria-describedby="email-addon" required></asp:TextBox>
                </div>
                <label>Password</label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control" placeholder="Password" aria-label="Password"
                        aria-describedby="password-addon" required></asp:TextBox>
                </div>
                <asp:Label runat="server" class="text-danger" ID="lblerror" Visible="false">You have entered the wrong credentials</asp:Label>
                <div class="form-check form-check-info text-left">
                    <a href="ForgotPasswordUI.aspx">Forgot Password?</a>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-lg btn-rounded w-100 mt-4 mb-0" Text="LogIn" OnClick="btnLogin_Click" />
                </div>
                <div class="text-center pt-0 px-sm-4 px-1">
                    <p class="mt-3 mb-4 mx-auto">
                        Don't have an account yet?
                      <a href="SignUpUI.aspx" class="text-primary fw-bolder">SignUp here</a>
                    </p>
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
