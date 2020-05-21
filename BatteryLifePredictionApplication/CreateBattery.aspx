<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBattery.aspx.cs" Inherits="BatteryLifePredictionApplication.CreateBattery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>Create Battery</h1>
            </div>
        </div>
    </div>

    <!-- Battery Reference -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Battery Reference</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="BatteryReferenceTb" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="BatteryReferenceValidator" CssClass="validationText" runat="server" ControlToValidate="BatteryReferenceTb" ErrorMessage="Please enter a Battery Reference" />
            </div>
        </div>
    </div>

    <!--Cycle Index -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Cycle Index</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="CycleIndexTb" CssClass="form-control" runat="server" type="number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CycleIndexValidator" CssClass="validationText" runat="server" ControlToValidate="CycleIndexTb" ErrorMessage="Please enter a Cycle Index" />
            </div>
        </div>
    </div>

    <!--Charge Capacity -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Charge Capacity</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="ChargeCapacityTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ChargeCapacityValidator" CssClass="validationText" runat="server" ControlToValidate="ChargeCapacityTb" ErrorMessage="Please enter a Charge Capacity" />
            </div>
        </div>
    </div>

    <!--Discharge Capacity -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Discharge Capacity</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="DischargeCapacityTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DischargeCapacityValidator" CssClass="validationText" runat="server" ControlToValidate="DischargeCapacityTb" ErrorMessage="Please enter a Discharge Capacity" />
            </div>
        </div>
    </div>

    <!--Charge Energy -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Charge Energy</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="ChargeEnergyTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ChargeEnergyValidator" CssClass="validationText" runat="server" ControlToValidate="ChargeEnergyTb" ErrorMessage="Please enter a Charge Energy" />
            </div>
        </div>
    </div>

    <!--Discharge Energy -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Discharge Energy</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="DischargeEnergyTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DischargeEnergyValidator" CssClass="validationText" runat="server" ControlToValidate="DischargeEnergyTb" ErrorMessage="Please enter a Discharge Energy" />
            </div>
        </div>
    </div>

    <!--dvdt -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>dvdt</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="dvdtTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="dvdtValidator" CssClass="validationText" runat="server" ControlToValidate="dvdtTb" ErrorMessage="Please enter a dvdt" />
            </div>
        </div>
    </div>

    <!--Internal Resistance -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Internal Resistance</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="InternalResistanceTb" CssClass="form-control" runat="server" type="number" Step="0.0000001"></asp:TextBox>
                <asp:RequiredFieldValidator ID="InternalResistanceValidator" CssClass="validationText" runat="server" ControlToValidate="InternalResistanceTb" ErrorMessage="Please enter an Internal Resistance" />
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
