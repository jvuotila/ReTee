<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberExport.ascx.cs" Inherits="MemberExport.Usercontrols.MemberExport" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbraco" %>
<umbraco:Feedback id="ProEditionInfo" runat="server" Visible="false" Text=""></umbraco:Feedback>
<umbraco:Pane ID="MembergroupPane" runat="server">
<asp:CustomValidator ID="FormValidator" OnServerValidate="ValidateCheckboxes"  runat="server" Text="*"  />
<umbraco:PropertyPanel ID="MembergroupPanel" runat="server" ><asp:Literal ID="NoGroupsDefinedLiteral" runat="server" /><asp:CheckBoxList ID="MemberGroupCheckBoxList" runat="server" /> </umbraco:PropertyPanel>
</umbraco:Pane>
<umbraco:Pane ID="MemberTypePane" runat="server">
<umbraco:PropertyPanel ID="MemberTypePropertyPanel" runat="server" ><asp:Literal ID="NoTypeDefinedLiteral" runat="server" />
    <asp:DropDownList ID="MemberTypeDropDown" runat="server" AutoPostBack="true" CausesValidation="false" 
        onselectedindexchanged="MemberTypeDropDown_SelectedIndexChanged" /></umbraco:PropertyPanel>
</umbraco:Pane>
<umbraco:Pane ID="MemberExportFieldsPane" runat="server">
<umbraco:PropertyPanel ID="MemberExportFieldsPanel" runat="server" ><asp:Literal ID="NoFieldsToExportLiteral" runat="server" />
    <asp:CheckBoxList ID="MemberExportFieldCheckBoxList" RepeatColumns="2" 
        runat="server" /></umbraco:PropertyPanel>
</umbraco:Pane>
<umbraco:Pane ID="ExportOptionsPane" runat="server">
<umbraco:PropertyPanel ID="ExportAsPanel" runat="server" ><asp:DropDownList ID="ExportAsDropDownList" AutoPostBack="true" CausesValidation="false" runat="server"/></umbraco:PropertyPanel>
<asp:PlaceHolder ID="ExportProviderPlaceholder" runat="server" />
</umbraco:Pane>
<umbraco:Pane ID="ExportPane" runat="server">
<asp:Button ID="ExportButton" runat="server" OnClick="ExportButton_Click" CssClass="btn btn-success"/>
</umbraco:Pane>