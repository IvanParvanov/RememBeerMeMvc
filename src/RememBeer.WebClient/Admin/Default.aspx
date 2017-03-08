<%@ Page Title="Admin - Breweries" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RememBeer.WebClient.Admin.Admin" %>
<%@ Import Namespace="RememBeer.WebClient.Utils" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-group form-inline">
                <asp:TextBox CssClass="form-control" runat="server" ID="SearchTb" placeholder="Brewery name/country"></asp:TextBox>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Search" OnClick="Search_OnClick"/>
            </div>
            <div class="">
                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    CssClass="table table-bordered table-striped table-hover table-responsive"
                    AutoGenerateColumns="False"
                    ItemType="RememBeer.Models.Contracts.IBrewery"
                    AllowPaging="True"
                    OnPageIndexChanging="GridView1_OnPageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id">
                            <ControlStyle CssClass="form-control"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country">
                            <ControlStyle CssClass="form-control"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ControlStyle CssClass="form-control"></ControlStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# this.Eval("Description").ToString().Truncate(50) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl='<%# "Brewery.aspx?id=" + this.Eval("Id") %>'>
                                    <i class="fa fa-arrow-right" aria-hidden="true"></i>
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings
                        NextPageText="Next"
                        PreviousPageText="Back"
                        Mode="NumericFirstLast"
                        PageButtonCount="10">
                    </PagerSettings>
                    <PagerTemplate>
                        <div class="text-center">
                            <asp:LinkButton ID="lb1" Text="Previous" CssClass="btn btn-inverse btn-xs"
                                            CommandName="Page" CommandArgument="Prev" runat="server"/>
                            <asp:LinkButton ID="lb2" Text="Next" CommandName="Page" CssClass="btn btn-inverse btn-xs"
                                            CommandArgument="Next" runat="server"/>
                        </div>
                    </PagerTemplate>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>