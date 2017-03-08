<%@ Page Title="Admin - User Reviews" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" ValidateRequest="false" Inherits="RememBeer.WebClient.Admin.UserDetails" %>
<%@ Register tagPrefix="uc" tagName="Notifier" src="../UserControls/UserNotifications.ascx" %>
<%@ Register TagPrefix="uc" TagName="ReviewsEditView" Src="~/UserControls/ReviewsEditView.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc:Notifier runat="server" ID="Notifier" ViewStateMode="Disabled" ></uc:Notifier>
    <h6>Now managing reviews for: <asp:Label runat="server" ID="UserNameLabel"></asp:Label></h6>
    <uc:ReviewsEditView ID="EditReviews" runat="server" ></uc:ReviewsEditView>
</asp:Content>
