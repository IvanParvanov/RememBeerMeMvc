<%@ Page Title="Top Breweries" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Breweries.aspx.cs" Inherits="RememBeer.WebClient.Top.Breweries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView runat="server"
              CssClass="table table-bordered table-hover table-striped"
              ID="RankingGridView"
              ItemType="RememBeer.Models.Dtos.IBreweryRank"
              AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="Position">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Brewery Name"/>
        <asp:BoundField DataField="TotalBeersCount" HeaderText="Total Reviews"/>
        <asp:BoundField DataField="AveragePerBeer" HeaderText="Average Score"/>
    </Columns>
</asp:GridView>
</asp:Content>