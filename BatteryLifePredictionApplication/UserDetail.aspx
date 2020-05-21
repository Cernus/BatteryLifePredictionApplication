<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="BatteryLifePredictionApplication.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <%-- Title --%>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <h1>User Detail</h1>
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
                    <asp:RequiredFieldValidator ID="UsernameTbValidator" CssClass="validationText" runat="server" ControlToValidate="UsernameTb" ErrorMessage="Please enter a Username" />
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
                    <asp:RequiredFieldValidator ID="PasswordTbValidator" CssClass="validationText" runat="server" ControlToValidate="PasswordTb" ErrorMessage="Please enter a Password" />
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
                    <asp:RequiredFieldValidator ID="JobTitleTbValidator" CssClass="validationText" runat="server" ControlToValidate="JobTitleTb" ErrorMessage="Please enter a Job Title" />
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
                    <asp:RequiredFieldValidator ID="EmailAddressTbValidator" CssClass="validationText" runat="server" ControlToValidate="EmailAddressTb" ErrorMessage="Please enter an email" />
                </div>
            </div>
        </div>

        <!-- Buttons -->
        <div class="form-group">
            <div class="row">
                <div class="col-md-6" style="margin-bottom: 10px">
                    <asp:Button ID="UpdateBtn" CssClass="btn btn-warning" runat="server" Text="Update" OnClick="UpdateBtn_Click" />
                </div>
                <div class="col-md-6" style="margin-bottom: 10px">
                    <asp:Button ID="DeleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="false" Text="Delete" OnClick="DeleteBtn_Click" />
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
    </div>
</asp:Content>
