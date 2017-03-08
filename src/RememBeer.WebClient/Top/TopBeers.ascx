<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopBeers.ascx.cs" Inherits="RememBeer.WebClient.Top.TopBeers" %>
<asp:GridView runat="server"
              CssClass="table table-bordered table-hover table-striped"
              ID="RankingGridView"
              ItemType="RememBeer.Models.Dtos.IBeerRank"
              AutoGenerateColumns="False">
    <Columns>
        <asp:TemplateField HeaderText="Position">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Beer.Name" HeaderText="Beer"/>
        <asp:BoundField DataField="Beer.Brewery.Name" HeaderText="Brewery"/>
        <asp:BoundField DataField="Beer.Brewery.Country" HeaderText="Country"/>
        <asp:BoundField DataField="LookScore" HeaderText="Looks"/>
        <asp:BoundField DataField="SmellScore" HeaderText="Aroma"/>
        <asp:BoundField DataField="TasteScore" HeaderText="Taste"/>
        <asp:BoundField DataField="OverallScore" HeaderText="Overall"/>
        <asp:BoundField DataField="CompositeScore" HeaderText="Final Score"/>
    </Columns>
</asp:GridView>
