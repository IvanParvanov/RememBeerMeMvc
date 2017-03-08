<%@ Page Title="Brewery Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brewery.aspx.cs" Inherits="RememBeer.WebClient.Admin.Brewery" %>
<%@ Import Namespace="RememBeer.WebClient.Utils" %>
<%@ Register tagPrefix="uc" tagName="Notifier" src="../UserControls/UserNotifications.ascx" %>
<%@ Register TagPrefix="uc" Namespace="RememBeer.WebClient.UserControls" Assembly="RememBeer.WebClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel runat="server">
<ContentTemplate>


<uc:Notifier runat="server" ID="Notifier" ViewStateMode="Disabled"/>
<asp:PlaceHolder runat="server" ID="Content">
<div class="container">
<div class="row">
    <div class="col-md-9">
        <h6>Brewery details</h6>
        <asp:DetailsView runat="server"
                         ID="BreweryDetails"
                         AutoGenerateRows="False"
                         OnModeChanging="BreweryDetails_OnModeChanging"
                         OnItemUpdating="BreweryDetails_OnItemUpdating"
                         ItemType="RememBeer.Models.Contracts.IBrewery"
                         CssClass="table table-responsive table-bordered table-striped table-hover table-condensed">
            <Fields>
                <asp:TemplateField HeaderText="Id" AccessibleHeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="LabelId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="LabelId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" AccessibleHeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server"
                                     ID="NameTb"
                                     CssClass="form-control"
                                     Text='<%# Bind("Name") %>'
                                     ValidationGroup="Edit">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                                                    CssClass="text-danger"
                                                    ValidationGroup="Edit"
                                                    Display="Dynamic"
                                                    ErrorMessage="Name is required"
                                                    ControlToValidate="NameTb">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server"
                                                        ValidationGroup="Edit"
                                                        Display="Dynamic"
                                                        ValidationExpression="^[\s\S]{1,512}$"
                                                        ControlToValidate="NameTb"
                                                        CssClass="text-danger"
                                                        ErrorMessage="Name must be between 1 and 512 characters long">
                        </asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country" AccessibleHeaderText="Country">
                    <ItemTemplate>
                        <asp:Label ID="LabelCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="CountryTb"
                                     ValidationGroup="Edit"
                                     CssClass="form-control" Text='<%# Bind("Country") %>'>
                        </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                                                    ValidationGroup="Edit"
                                                    CssClass="text-danger"
                                                    Display="Dynamic"
                                                    ErrorMessage="Country is required"
                                                    ControlToValidate="CountryTb">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server"
                                                        ValidationGroup="Edit"
                                                        Display="Dynamic"
                                                        ValidationExpression="^[\s\S]{1,128}$"
                                                        ControlToValidate="CountryTb"
                                                        CssClass="text-danger"
                                                        ErrorMessage="Country must be between 1 and 128 characters long">
                        </asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" AccessibleHeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="LabelDescr" runat="server"
                                   Text='<%# this.Eval("Description")?.ToString().Truncate(200) %>'>
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="DescrTb"
                                     ValidationGroup="Edit"
                                     CssClass="form-control"
                                     TextMode="MultiLine"
                                     Rows="3"
                                     Text='<%# Bind("Description") %>'>
                        </asp:TextBox>
                        <asp:RequiredFieldValidator runat="server"
                                                    CssClass="text-danger"
                                                    ValidationGroup="Edit"
                                                    Display="Dynamic"
                                                    ErrorMessage="Description is required"
                                                    ControlToValidate="DescrTb">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server"
                                                        ValidationGroup="Edit"
                                                        Display="Dynamic"
                                                        ValidationExpression="^[\s\S]{1,2048}$"
                                                        ControlToValidate="DescrTb"
                                                        CssClass="text-danger"
                                                        ErrorMessage="Description must be between 1 and 2048 characters long">
                        </asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Edit" CssClass="btn btn-warning" CommandName="Edit"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button runat="server" Text="Update" CssClass="btn btn-primary" CommandName="Update" ValidationGroup="Edit"/>
                        <asp:Button runat="server" Text="Cancel" CssClass="btn btn-danger" CommandName="Cancel"/>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
    </div>
</div>
<div class="row">
    <div class="col-md-9">
        <h6>Brewery beers</h6>
        <table class="table table-bordered table-hover table-striped">
            <thead>
            <tr>
                <td>Name</td>
                <td>Style</td>
                <td></td>
            </tr>
            </thead>
            <asp:Repeater runat="server"
                          ID="BeersRepeater"
                          ItemType="RememBeer.Models.Contracts.IBeer">
                <ItemTemplate>
                    <tr runat="server" Visible='<%# !Item.IsDeleted %>'>
                        <td><%# Item.Name %> </td>
                        <td>
                            <em>
                                <small><%# Item.BeerType.Type %></small>
                            </em>
                        </td>
                        <td>
                            <asp:Button runat="server" CommandName="DeleteBeer" CssClass="btn btn-danger btn-xs" CommandArgument='<%# Item.Id %>' Text="Delete" OnCommand="OnBeerCommand"/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="col-md-3">
        <h6>Add a new beer</h6>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BeerTypeTextbox" Text="Beer Type: "></asp:Label>
            <asp:TextBox runat="server"
                         ID="BeerTypeTextbox"
                         ValidationGroup="Create"
                         Text=''
                         ClientIDMode="Predictable"
                         CssClass="form-control">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server"
                                        ControlToValidate="BeerTypeTextbox"
                                        ValidationGroup="Create"
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ErrorMessage="">
            </asp:RequiredFieldValidator>
            <uc:ValidatedHiddenField
                runat="server"

                ID="HiddenBeerTypeId"
                ClientIDMode="Predictable">
            </uc:ValidatedHiddenField>
            <asp:RequiredFieldValidator runat="server"
                                        ControlToValidate="HiddenBeerTypeId"
                                        ValidationGroup="Create"
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ErrorMessage="Please select a beer from the dropdown">
            </asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BeerNameTb" Text="Beer Name: "></asp:Label>
            <asp:TextBox runat="server"
                         ViewStateMode="Disabled"
                         ID="BeerNameTb"
                         ValidationGroup="Create"
                         CssClass="form-control">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server"
                                        ControlToValidate="BeerNameTb"
                                        CssClass="text-danger"
                                        Display="Dynamic"
                                        ValidationGroup="Create"
                                        ErrorMessage="Beer name is required">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server"
                                            Display="Dynamic"
                                            ValidationExpression="^[\s\S]{1,512}$"
                                            ControlToValidate="BeerNameTb"
                                            CssClass="text-danger"
                                            ValidationGroup="Create"
                                            ErrorMessage="Beer name must be between 1 and 512 characters long">
            </asp:RegularExpressionValidator>
        </div>
        <asp:Button runat="server" ID="CreateBeer" CssClass="btn btn-primary" Text="Add beer" ValidationGroup="Create" OnClick="CreateBeer_OnClick"/>
    </div>
    <script src="/Scripts/devbridge-autocomplete.min.js"></script>
    <script src="/Scripts/beertypes-autocomplete.js"></script>
</div>
</div>
</asp:PlaceHolder>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>