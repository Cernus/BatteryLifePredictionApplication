<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="BatteryLifePredictionApplication.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <%--Title--%>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <h1>Log In</h1>
                </div>
            </div>
        </div>

        <%--Error Message--%>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="MessageLabel" CssClass="validationText" runat="server" Visible="false">No Error</asp:Label>
                </div>
            </div>
        </div>

        <!-- Username -->
        <div class="form-group">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server"><strong>Username: </strong></asp:Label>
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="usernameLabel" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UsernameValidator" CssClass="validationText" runat="server" ControlToValidate="usernameLabel" ErrorMessage="Please enter a Username" />
                </div>
            </div>
        </div>

        <!-- Password -->
        <div class="form-group">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label runat="server"><strong>Password: </strong></asp:Label>
                </div>
                <div class="col-md-8">
                    <asp:TextBox ID="passwordLabel" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordValidator" CssClass="validationText" runat="server" ControlToValidate="passwordLabel" ErrorMessage="Please enter a Password" />
                </div>
            </div>
        </div>

        <%--Submit Button--%>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="LogInBtn" class="btn btn-success" runat="server" Text="Sign In" OnClick="LogInBtn_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
