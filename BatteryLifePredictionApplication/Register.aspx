<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BatteryLifePredictionApplication.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>Register</h1>
            </div>
        </div>
    </div>

    <!-- Username -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Username</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="UsernameTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UsernameValidator" CssClass="validationText" runat="server" ControlToValidate="UsernameTb" ErrorMessage="Please enter a Username" />
            </div>
        </div>
    </div>

    <!-- Password -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Password</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="PasswordTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordValidator" CssClass="validationText" runat="server" ControlToValidate="PasswordTb" ErrorMessage="Please enter a Password" />
            </div>
        </div>
    </div>

    <!-- Job Title -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Job Title</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="JobTitleTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="JobTitleValidator" CssClass="validationText" runat="server" ControlToValidate="JobTitleTb" ErrorMessage="Please enter a Job Title" />
            </div>
        </div>
    </div>

    <!-- Email Address -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Email Address</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="EmailAddressTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailAddressValidator" CssClass="validationText" runat="server" ControlToValidate="EmailAddressTb" ErrorMessage="Please enter an Email Address" />
            </div>
        </div>
    </div>

    <!-- Buttons -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="RegisterBtn" class="btn btn-success" runat="server" Text="Register" OnClick="RegisterBtn_Click" />
            </div>
        </div>
    </div>

    <%--Message--%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="MessageLabel" CssClass="validationText" runat="server" Visible="false">No Error</asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
