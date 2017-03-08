<%@ Page Title="My Beers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="My.aspx.cs" Inherits="RememBeer.WebClient.Reviews.My" ValidateRequest="false" %>
<%@ Register TagPrefix="uc" TagName="ReviewsEditView" Src="~/UserControls/ReviewsEditView.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center spaced">
        <a type="button" class="btn btn-primary btn-lg" href="/Reviews/Create">Create new</a>
    </div>
    <uc:ReviewsEditView ID="EditReviews" runat="server" UserId='<%# this.Context.User?.Identity?.GetUserId() %>'></uc:ReviewsEditView>
</asp:Content>