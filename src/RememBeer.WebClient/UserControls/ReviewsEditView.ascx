<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewsEditView.ascx.cs" Inherits="RememBeer.WebClient.UserControls.ReviewsEditView" %>

<%@ Register TagPrefix="uc" TagName="BeerRatingSelect" Src="~/UserControls/BeerRatingSelect.ascx" %>
<%@ Register TagPrefix="uc" TagName="Notifier" Src="~/UserControls/UserNotifications.ascx" %>
<%@ Register TagPrefix="uc" TagName="BeerReview" Src="~/UserControls/SingleBeerReview.ascx" %>
<asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <uc:Notifier runat="server" ID="Notifier"/>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
                <asp:ListView ID="ReviewsListView" runat="server"
                              InsertItemPosition="FirstItem"
                              ItemType="RememBeer.Models.Contracts.IBeerReview"
                              SelectMethod="Select"
                              UpdateMethod="UpdateReview"
                              DeleteMethod="DeleteReview"
                              DataKeyNames="BeerId, CreatedAt, Id, IsDeleted, IsPublic, ModifiedAt, ApplicationUserId">
                    <LayoutTemplate>

                        <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <EmptyDataTemplate>
                        <p class="text-center">You dont have any reviews yet!</p>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <uc:BeerReview runat="server" IsEdit="True" Review="<%# Item %>"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="container">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <%#: Item.Beer.Brewery.Name %>
                                    <small class="pull-right"><%#: Item.CreatedAt.ToShortDateString() %></small>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-3">
                                        <img class="img-responsive center" src='<%# Item.ImgUrl %>'>
                                    </div>
                                    <div class="col-md-9 text-left">
                                        <h6 class="media-heading">
                                            <asp:HiddenField runat="server" ID="ImageUrlField" Value='<%# Bind("ImgUrl") %>'/>
                                            <%#: Item.Beer.Name %>
                                            <span>
                                                @<asp:TextBox runat="server"
                                                              ID="PlaceTextBox"
                                                              ValidationGroup="Edit"
                                                              Text='<%#Bind("Place") %>'
                                                              CssClass="form-control">
                                                </asp:TextBox>
                                            </span>
                                        </h6>
                                        <asp:RequiredFieldValidator runat="server"
                                                                    ControlToValidate="PlaceTextBox"
                                                                    CssClass="text-danger"
                                                                    Display="Dynamic"
                                                                    ValidationGroup="Edit"
                                                                    ErrorMessage="Location is required">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server"
                                                                        ValidationExpression="^[\s\S]{1,128}$"
                                                                        ControlToValidate="PlaceTextBox"
                                                                        CssClass="text-danger"
                                                                        ValidationGroup="Edit"
                                                                        ErrorMessage="Location must be between 1 and 128 characters long">
                                        </asp:RegularExpressionValidator>
                                        <p>
                                        <asp:TextBox runat="server"
                                                     ID="DescriptionTextBox"
                                                     CssClass="form-control"
                                                     TextMode="MultiLine"
                                                     ValidationGroup="Edit"
                                                     Rows="3"
                                                     Text='<%#Bind("Description") %>'>
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server"
                                                                    ControlToValidate="DescriptionTextBox"
                                                                    CssClass="text-danger"
                                                                    Display="Dynamic"
                                                                    ValidationGroup="Edit"
                                                                    ErrorMessage="Description is required">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server"
                                                                        ValidationExpression="^[\s\S]{10,1000}$"
                                                                        ControlToValidate="DescriptionTextBox"
                                                                        CssClass="text-danger"
                                                                        ValidationGroup="Edit"
                                                                        ErrorMessage="Description must be between 10 and 1000 characters long">
                                        </asp:RegularExpressionValidator>
                                        <p>
                                            <small>
                                                <em>Bottom line:</em>
                                            </small>
                                        </p>
                                        <ul class="list-inline">
                                            <li>
                                                Overall:
                                                <uc:BeerRatingSelect ID="BeerRatingSelect9" runat="server" SelectedValue='<%#:Bind("Overall") %>'>
                                                </uc:BeerRatingSelect>
                                            </li>
                                            <li>
                                                Taste:
                                                <uc:BeerRatingSelect ID="BeerRatingSelect10" runat="server" SelectedValue='<%#:Bind("Taste") %>'>
                                                </uc:BeerRatingSelect>
                                            </li>
                                            <li>
                                                Look:
                                                <uc:BeerRatingSelect ID="BeerRatingSelect11" runat="server" SelectedValue='<%#:Bind("Look") %>'>
                                                </uc:BeerRatingSelect>
                                            </li>
                                            <li>
                                                Aroma:
                                                <uc:BeerRatingSelect ID="BeerRatingSelect12" runat="server" SelectedValue='<%#:Bind("Smell") %>'>
                                                </uc:BeerRatingSelect>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <asp:Button runat="server" ID="Button1" ValidationGroup="Edit" CssClass="btn btn-success" CommandName="Update" Text="Save"/>
                                    <asp:Button runat="server" ID="Button2" CssClass="btn btn-warning" CommandName="Cancel" Text="Cancel"/>
                                </div>
                            </div>
                        </div>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <script src="/Scripts/devbridge-autocomplete.min.js"></script>
                        <script src="/Scripts/autocomplete.js"></script>
                    </InsertItemTemplate>
                </asp:ListView>
                <div class="text-center">
                            <asp:DataPager runat="server" PagedControlID="ReviewsListView" PageSize="5" ID="Pagination">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonCssClass="btn btn-inverse btn-xs"
                                                                ButtonType="Link"
                                                                ShowFirstPageButton="false"
                                                                ShowPreviousPageButton="true"
                                                                ShowNextPageButton="false"/>
                                    <asp:NumericPagerField ButtonType="Link"
                                                           CurrentPageLabelCssClass="btn btn-primary btn-xs"
                                                           NumericButtonCssClass="btn btn-inverse btn-xs"/>
                                    <asp:NextPreviousPagerField ButtonCssClass="btn btn-inverse btn-xs"
                                                                ButtonType="Link"
                                                                ShowNextPageButton="true"
                                                                ShowLastPageButton="false"
                                                                ShowPreviousPageButton="false"/>
                                </Fields>
                            </asp:DataPager>
                        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>