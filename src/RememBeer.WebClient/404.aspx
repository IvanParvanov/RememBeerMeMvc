<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="RememBeer.WebClient._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="error-template">
                    <h1>
                        Oops!
                    </h1>
                    <h2>
                        404 Not Found
                    </h2>
                    <div class="error-details">
                        Requested page not found!
                    </div>
                    <div class="error-actions">
                        <a href="/" class="btn btn-primary btn-lg">
                            <span class="glyphicon glyphicon-home"></span>
                            Take Me Home
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>