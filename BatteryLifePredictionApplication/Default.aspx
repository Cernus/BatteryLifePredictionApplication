<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BatteryLifePredictionApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%--Title--%>
    <div class="jumbotron">
        <h1>Home</h1>
        <p class="lead">This application uses a Machine Learning Model trained in Azure Machine Learning Studio</p>
    </div>

    <%--Message--%>
    <div class="form-group">
        <div class="row">
            <asp:Label ID="MessageLabel" CssClass="validationText" runat="server" Visible="false">No Error</asp:Label>
        </div>
    </div>

    <%--System Information--%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">

                <h2>Users</h2>
                <p>
                    To view the information for the current user, navigate to the User Detail page by clicking on Profile.
                </p>
                <p>
                    The Admin can view the profiles of all users on the system by navigating to the User Details page by clicking on Users.
                </p>
            </div>
            <div class="col-md-4">
                <h2>Batteries</h2>
                <p>
                    Batteries have multiple paramters, which are used by the Machine Learning Algorithm to make a prediction on remaining lifetime.
                </p>
                <p>
                    By navigating to the Batch Detail page, the user can perform a single prediction (one battery) or a batch prediction (all batteries in a batch).
                </p>
            </div>
            <div class="col-md-4">
                <h2>Batches</h2>
                <p>
                    A batch contains many batteries.
                </p>
            </div>
        </div>
    </div>
</asp:Content>
