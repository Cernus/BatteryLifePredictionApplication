<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBatch.aspx.cs" Inherits="BatteryLifePredictionApplication.CreateBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>Create Batch</h1>
            </div>
        </div>
    </div>

    <!-- Batch Reference -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Batch Reference</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="BatchReferenceTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="BatchReferenceValidator" CssClass="validationText" runat="server" ControlToValidate="BatchReferenceTb" ErrorMessage="Please enter a Batch Reference" />
            </div>
        </div>
    </div>

    <!-- Buttons -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="CreateBtn" class="btn btn-success" runat="server" Text="Create" OnClick="CreateBtn_Click" />
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
