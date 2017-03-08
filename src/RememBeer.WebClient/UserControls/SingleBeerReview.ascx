<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleBeerReview.ascx.cs" Inherits="RememBeer.WebClient.UserControls.SingleBeerReview" %>
<div class="container">
    <div class="panel panel-primary">
        <div class="panel-heading">
            <a class="text-primary" href='/Reviews/?id=<%#: Review.Id %>'>
                <%#: Review.Beer.Brewery.Name %>
            </a>
            <small class="pull-right"><%#: Review.CreatedAt.ToShortDateString() %></small>
        </div>
        <div class="panel-body">
            <div class="col-md-3">
                <img class="img-responsive center" src='<%# Review.ImgUrl %>'>
            </div>
            <div class="col-md-9 text-left">
                <h6 class="media-heading">
                    <%#: Review.Beer.Name %>
                    <span>
                        <em>@<%#:Review.Place %></em>
                    </span>
                </h6>
                <p><%#: Review.Beer.BeerType.Type %></p>
                <hr/>
                <p><%#: Review.Description %></p>
                <p>
                    <small>
                        <em>Bottom line:</em>
                    </small>
                </p>
                <ul class="list-inline">
                    <li>
                        Overall:
                        <span class="badge"><%#: Review.Overall %></span>
                    </li>
                    <li>|</li>
                    <li>
                        Taste:
                        <span class="badge"><%#: Review.Taste %></span>
                    </li>
                    <li>|</li>
                    <li>
                        Look:
                        <span class="badge"><%#: Review.Look %></span>
                    </li>
                    <li>|</li>
                    <li>
                        Aroma:
                        <span class="badge"><%#: Review.Smell %></span>
                    </li>
                </ul>
            </div>
        </div>
        <asp:PlaceHolder runat="server" ID="EditPlaceholder" Visible="False">
            <div class="panel-footer">
                <asp:Button runat="server" CssClass="btn btn-warning" ID="EditButton" CommandName="Edit" Text="Edit"/>

                <a type="button" class="btn btn-danger" data-toggle="modal" data-target='<%# "#review" + Review.Id %>'>Delete</a>
                <div id='<%# "review" + Review.Id %>' class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Delete review</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure you want to delete this review?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" CssClass="btn btn-danger pull-left" ID="DeleteButton" CommandName="Delete" Text="Delete"/>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</div>