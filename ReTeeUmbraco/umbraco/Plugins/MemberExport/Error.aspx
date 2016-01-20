<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" MasterPageFile="/umbraco/masterpages/umbracoPage.Master"  Inherits="MemberExport.Pages.Error" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <cc1:UmbracoPanel ID="UmbracoPanel" runat="server">
        <cc1:Pane ID="Pane1" runat="server">
        <cc1:Feedback ID="ErrorMessageFeedback" runat="server" />
</cc1:Pane></cc1:UmbracoPanel></asp:Content>
