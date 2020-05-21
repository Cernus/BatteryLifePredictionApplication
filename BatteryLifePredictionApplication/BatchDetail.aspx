<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchDetail.aspx.cs" Inherits="BatteryLifePredictionApplication.BatchDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>Batch Detail</h1>
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
                <asp:TextBox ID="BatchReferenceTb" CssClass="form-control" runat="server" ValidationGroup="BatchGroup"></asp:TextBox>
                <asp:RequiredFieldValidator ID="BatchReferenceValidator" CssClass="validationText" runat="server" ControlToValidate="BatchReferenceTb" ValidationGroup="BatchGroup" ErrorMessage="Please enter a Batch Reference" />
            </div>
        </div>
    </div>

    <!-- Buttons (Upload/Delete) -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-6" style="margin-bottom: 10px">
                <asp:Button ID="UpdateBtn" CssClass="btn btn-warning" runat="server" ValidationGroup="BatchGroup" Text="Update" OnClick="UpdateBtn_Click" />
            </div>
            <div class="col-md-6" style="margin-bottom: 10px">
                <asp:Button ID="DeleteBtn" CssClass="btn btn-danger" runat="server" CausesValidation="false" Text="Delete" OnClick="DeleteBtn_Click" />
            </div>
        </div>
    </div>

    <hr />

    <%-- Create Button --%>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Click the button below to create a new Battery</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:Button ID="CreateBatteryBtn" CssClass="btn btn-info" runat="server" Text="Create Battery" OnClick="CreateBatteryBtn_Click" />
            </div>
        </div>
    </div>

    <hr />

    <!-- Upload Elements -->
    <div class="form-group">
        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server"><strong>Upload</strong></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:FileUpload ID="BatteryUpload" CssClass="form-control" runat="server" ValidationGroup="UploadGroup" />
                <asp:RequiredFieldValidator ID="BatteryUploadValidator" CssClass="validationText" runat="server" ControlToValidate="BatteryUpload" ValidationGroup="UploadGroup" ErrorMessage="Please select a file to upload" />
            </div>
        </div>
    </div>

    <%-- Upload Button --%>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Click the button below to upload batteries to this batch</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:Button ID="UploadBtn" CssClass="btn btn-info" runat="server" Text="Upload" ValidationGroup="UploadGroup" OnClick="UploadBtn_Click" />
            </div>
        </div>
    </div>

    <%-- Download Button --%>
    <div id="DownloadDiv" class="form-group" runat="server">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Click the button below to download all the battery data as an Excel document</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:Button ID="DownloadBtn" CssClass="btn btn-info" runat="server" Text="Download Spreadsheet" CausesValidation="false" OnClick="DownloadBtn_Click" />

            </div>
        </div>
    </div>

    <hr />

    <%-- Batch Request Button --%>
    <div class="form-group">
        <div class="row">
            <div class="col-xs-12">
                <p><strong>Click the button below to get predictions for all batteries in the batch</strong></p>
            </div>
            <div class="col-xs-12">
                <asp:Button ID="BatchPredictBtn" CssClass="btn btn-info" runat="server" Text="Predict Batch" CausesValidation="false" OnClick="BatchPredictBtn_Click" />
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

    <hr />

    <%-- Subtitle --%>
    <div class="form-group">
        <div id="SubtitleDiv" class="row" runat="server">
            <div class="col-md-12">
                <h2>Batteries in this Batch</h2>
            </div>
        </div>
    </div>

    <%-- GridView --%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView
                ID="BatteryGridView"
                CssClass="table table-condensed table-hover cellSurround"
                runat="server"
                UseAccessibleHeader="true"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Battery Reference">
                        <ItemTemplate>
                            <asp:HyperLink ID="BatteryRefLink" runat="server" DataNavigateUrlFields="Battery_Ref" Text='<%# Eval("Battery_Ref") %>'
                                NavigateUrl='<%# string.Format("~/BatteryDetail.aspx?Id={0}", HttpUtility.UrlEncode(Eval("BatteryId").ToString())) %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Lifetime" HeaderText="Lifetime" SortExpression="Lifetime" ItemStyle-CssClass="LifetimeColumn" />
                    <asp:TemplateField HeaderText="Predict" SortExpression="BatteryId">
                        <ItemTemplate>
                            <asp:LinkButton ID="PredictLB" runat="server"
                                Text="Predict"
                                CommandName="PredictLifetime"
                                CommandArgument='<%#Eval("BatteryId")%>'
                                OnCommand="PredictLB_Command">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <%-- No Batteries in Batch --%>
    <div class="form-group">
        <div id="hiddenDiv" class="row" runat="server" visible="false">
            <div class="col-xs-12">
                <p>No Batteries in batch</p>
            </div>
        </div>
    </div>
</asp:Content>
