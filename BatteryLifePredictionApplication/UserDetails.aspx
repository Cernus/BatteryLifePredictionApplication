<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="BatteryLifePredictionApplication.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Title --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <h1>User Details</h1>
            </div>
        </div>
    </div>

    <%-- GridView --%>
    <div class="form-group">
        <div class="row">
            <div class="col-md-12">
                <asp:GridView
                    ID="UserGridView"
                    CssClass="table table-condensed table-hover cellSurround"
                    runat="server"
                    UseAccessibleHeader="true"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Username">
                            <ItemTemplate>
                                <asp:HyperLink ID="UsernameLink" runat="server" DataNavigateUrlFields="Username" Text='<%# Eval("Username") %>'
                                    NavigateUrl='<%# string.Format("~/UserDetail.aspx?Id={0}", HttpUtility.UrlEncode(Eval("UserId").ToString())) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" ItemStyle-CssClass="PasswordColumn" />
                        <asp:BoundField DataField="JobTitle" HeaderText="Job Title" SortExpression="JobTitle" ItemStyle-CssClass="JobTitleColumn" />
                        <asp:BoundField DataField="EmailAddress" HeaderText="Email Address" SortExpression="EmailAddress" ItemStyle-CssClass="EmailAddressColumn" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%--No Users--%>
    <div class="form-group">
        <div id="hiddenDiv" class="row" runat="server" visible="false">
            <div class="col-xs-12">
                <p>No Users</p>
            </div>
        </div>
    </div>
</asp:Content>
