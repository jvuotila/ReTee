<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberExport.aspx.cs" MasterPageFile="~/umbraco/masterpages/umbracoPage.Master" Inherits="MemberExport.Pages.MemberExport" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<%@ Register Src="UserControls/MemberExport.ascx" TagName="Export" TagPrefix="MemberExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <umbraco:UmbracoPanel ID="UmbracoPanel" runat="server">
        <asp:PlaceHolder ID="MemberExportPlaceHolder" runat="server">
            <MemberExport:Export ID="Export" runat="server" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="SavePlaceHolder" runat="server">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SaveState" CssClass="error" />
            <umbraco:pane id="SavePane" runat="server">
                <umbraco:PropertyPanel runat="server" ID="SavePropertyPanel"><asp:TextBox ID="SaveAsTextbox" ValidationGroup="SaveState" MaxLength="250" runat="server" /><asp:RequiredFieldValidator
                                ID="SaveRequiredFieldValidator" ControlToValidate="SaveAsTextbox" ValidationGroup="SaveState"
                                runat="server" Text="*"></asp:RequiredFieldValidator></umbraco:PropertyPanel>
                </umbraco:pane>
             <umbraco:pane id="CreateCopyPane" runat="server">
                <umbraco:PropertyPanel runat="server" ID="CreateCopyPanel"><asp:CheckBox runat="server" ID="CreateCopyCheckBox"/></umbraco:PropertyPanel>
                </umbraco:pane>
        </asp:PlaceHolder>
    </umbraco:UmbracoPanel>
</asp:Content>
