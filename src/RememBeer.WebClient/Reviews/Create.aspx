<%@ Page Title="Create a new review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="RememBeer.WebClient.Reviews.Create" %>
<%@ Register TagPrefix="uc" TagName="BeerRatingSelect" Src="~/UserControls/BeerRatingSelect.ascx" %>
<%@ Register TagPrefix="uc" Namespace="RememBeer.WebClient.UserControls" Assembly="RememBeer.WebClient" %>
<%@ Register TagPrefix="uc" TagName="Notifier" Src="~/UserControls/UserNotifications.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc:Notifier runat="server" ID="Notifier"></uc:Notifier>
    <div class="container text-center">
        <div class="col-md-3"></div>
        <div class="col-md-6 text-left center well">
            <h4>Create a new review</h4>
            <div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="BeerNameTextbox" Text="Beer: "></asp:Label>
                    <asp:TextBox runat="server"
                                 ID="BeerNameTextbox"
                                 ValidationGroup="Create"
                                 Text=''
                                 ClientIDMode="Predictable"
                                 CssClass="form-control">
                    </asp:TextBox>
                    <uc:ValidatedHiddenField
                        runat="server"
                        ID="HiddenBeerId"
                        ClientIDMode="Predictable">
                    </uc:ValidatedHiddenField>
                    <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="HiddenBeerId"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Create"
                                                ErrorMessage="Please select a beer from the dropdown">
                    </asp:RequiredFieldValidator>
                </div>
                <script src="/Scripts/devbridge-autocomplete.min.js"></script>
                <script src="/Scripts/autocomplete.js"></script>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="TbPlace" Text="Place: "></asp:Label>
                    <asp:TextBox runat="server"
                                 ID="TbPlace"
                                 MaxLength="128"
                                 ValidationGroup="Create"
                                 CssClass="form-control">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TbPlace"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Create"
                                                ErrorMessage="Location is required">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                                                    Display="Dynamic"
                                                    ValidationExpression="^[\s\S]{1,128}$"
                                                    ControlToValidate="TbPlace"
                                                    CssClass="text-danger"
                                                    ValidationGroup="Create"
                                                    ErrorMessage="Location must be between 1 and 128 characters long">
                    </asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="TbDescription" Text="Description: "></asp:Label>
                    <asp:TextBox runat="server"
                                 ID="TbDescription"
                                 CssClass="form-control"
                                 ValidationGroup="Create"
                                 TextMode="MultiLine"
                                 Rows="6">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator runat="server"
                                                ControlToValidate="TbDescription"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Create"
                                                ErrorMessage="Description is required">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server"
                                                    Display="Dynamic"
                                                    ValidationExpression="^[\s\S]{10,1000}$"
                                                    ControlToValidate="TbDescription"
                                                    CssClass="text-danger"
                                                    ValidationGroup="Create"
                                                    ErrorMessage="Description must be between 10 and 1000 characters long">
                    </asp:RegularExpressionValidator>
                </div>
                <ul class="list-inline">
                    <li>
                        <label>
                            Overall:
                            <uc:BeerRatingSelect ID="BeerRatingSelect5" runat="server">
                            </uc:BeerRatingSelect>
                        </label>
                    </li>
                    <li>
                        <label>
                            Taste:
                            <uc:BeerRatingSelect ID="BeerRatingSelect6" runat="server">
                            </uc:BeerRatingSelect>
                        </label>
                    </li>
                    <li>
                        <label>
                            Look:
                            <uc:BeerRatingSelect ID="BeerRatingSelect7" runat="server">
                            </uc:BeerRatingSelect>
                        </label>
                    </li>
                    <li>
                        <label>
                            Aroma:
                            <uc:BeerRatingSelect ID="BeerRatingSelect8" runat="server">
                            </uc:BeerRatingSelect>
                        </label>
                    </li>
                </ul>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="ImageUpload" Text="Label image: ">
                    </asp:Label>
                    <asp:FileUpload runat="server"
                                    ID="ImageUpload"
                                    accept="image/*"
                                    CssClass="form-control-static"
                                    AllowMultiple="False"/>
                </div>
            </div>
            <div class="text-center">
                <asp:Button runat="server"
                            CssClass="btn btn-success btn-lg"
                            ID="InsertButton"
                            ValidationGroup="Create"
                            OnClick="InsertButton_OnClick"
                            Text="Save"/>
            </div>
        </div>
        <div class="col-md-3"></div>

    </div>

</asp:Content>