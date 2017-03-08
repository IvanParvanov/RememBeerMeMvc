<%@ Page Title="Review Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RememBeer.WebClient.Reviews.Default" %>
<%@ Register TagPrefix="uc" TagName="SingleBeerReview" src="/UserControls/SingleBeerReview.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Visible="False" runat="server">
    <asp:PlaceHolder runat="server" ID="NotFound">
        <h3>The requested review cannot be found!</h3>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="ReviewPlaceholder" Visible="True">
        <uc:SingleBeerReview runat="server" Visible="True" IsEdit="False" Review='<%# this.Model.Review %>'/>
    </asp:PlaceHolder>
</asp:Content>