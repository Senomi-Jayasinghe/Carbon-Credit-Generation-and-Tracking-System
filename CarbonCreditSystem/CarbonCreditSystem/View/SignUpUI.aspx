<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpUI.aspx.cs" Inherits="CarbonCreditSystem.View.SignUpUI" %>

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
            <h3 class="fw-bolder text-primary">Sign Up</h3>
        </div>
        <div class="card-body pb-3">
            <form id="frmLogin" runat="server">
                <label>First Name</label>
                <div class="input-group mb-3">
                    <asp:TextBox runat="server" ID="txtFirstName" type="text" class="form-control" placeholder="FirstName" 
                        aria-label="Name" required></asp:TextBox>
                </div>
                <label>Last Name</label>
                <div class="input-group mb-3">
                    <asp:TextBox runat="server" ID="txtLastName" type="text" class="form-control" placeholder="LastName" 
                        aria-label="LastName" required></asp:TextBox>
                </div>
                <label>Email</label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtEmail" runat="server" type="email" class="form-control" placeholder="Email" aria-label="Email"
                        aria-describedby="email-addon" required></asp:TextBox>
                </div>
                <label>Create Password</label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtCreatePsw" runat="server" type="password" class="form-control" placeholder="Create Password" 
                        required></asp:TextBox>
                </div>
                <label>Confirm Password</label>
                <div class="input-group mb-3">
                    <asp:TextBox ID="txtConfirmPsw" runat="server" type="password" class="form-control" placeholder="Confirm Password" 
                        required></asp:TextBox>
                </div>
                <asp:Label runat="server" class="text-danger" ID="lblerror"></asp:Label>
                <div class="text-center">
                    <asp:Button ID="btnSignUp" runat="server" class="btn btn-primary btn-lg btn-rounded w-100 mt-4 mb-0" Text="SignUp" 
                        OnClick="btnSignUp_Click" />
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
