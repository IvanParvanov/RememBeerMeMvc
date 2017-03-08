<%@ Page Title="Top Beers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Beers.aspx.cs" Inherits="RememBeer.WebClient.Top.Beers" %>
<%@ Register TagPrefix="uc" TagName="TopBeers" Src="~/Top/TopBeers.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc:TopBeers runat="server" ID="TopBeers"></uc:TopBeers>
</asp:Content>