<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchDetails.aspx.cs" Inherits="BatteryLifePredictionApplication.BatchDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>Batch Details</h1>
            </div>
        </div>
    </div>

    <%-- DropDownList --%>
    <div id="DDLdiv" class="form-group" runat="server" visible="false">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Select user</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:DropDownList ID="UserDDL" CssClass-="form-control" runat="server" OnSelectedIndexChanged="UserDDL_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    </div>

    <%-- Create Button --%>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Click the button below to create a new batch</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:Button ID="CreateBatchBtn" CssClass="btn btn-info" runat="server" Text="Create Batch" OnClick="CreateBatchBtn_Click" />
            </div>
        </div>
    </div>

    <%-- Subtitle --%>
    <div class="form-group">
        <div id="SubtitleDiv" class="row" runat="server">
            <div class="col-md-12">
                <h2>Batches</h2>
            </div>
        </div>
    </div>

    <%-- GridView --%>
    <div class="form-group">
        <asp:GridView
            ID="BatchGridView"
            CssClass="table table-condensed table-hover cellSurround"
            runat="server"
            UseAccessibleHeader="true"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="Batch Reference">
                    <ItemTemplate>
                        <asp:HyperLink ID="BatchRefLink" runat="server" DataNavigateUrlFields="Batch_Ref" Text='<%# Eval("Batch_Ref") %>'
                            NavigateUrl='<%# string.Format("~/BatchDetail.aspx?Id={0}", HttpUtility.UrlEncode(Eval("BatchId").ToString())) %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BatteryCount" HeaderText="BatteryCount" SortExpression="BatteryCount" ItemStyle-CssClass="BatteryCountColumn" />
                <asp:BoundField DataField="PredictionStatus" HeaderText="Batch Prediction Status" SortExpression="PredictionStatus" ItemStyle-CssClass="PredictionStatusColumn" />
            </Columns>
        </asp:GridView>
    </div>

    <%-- No Batches --%>
    <div class="form-group">
        <div id="hiddenDiv" class="row" runat="server" visible="false">
            <div class="col-xs-12">
                <p>No Batches</p>
            </div>
        </div>
    </div>
</asp:Content>
