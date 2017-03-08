<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RememBeer.WebClient._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="jumbotron text-center">
  <h1>Remem<i class="fa fa-beer fa-lg"></i>Me</h1>
       <p>So, you are in <em>&lt;insert foreign country with great beers&gt;</em> having an ice-cold pint of <br/> <em>&lt;insert beer with a complicated name&gt;</em>. You really like it. 
           You would like to try it again on your next visit.
       </p>
       <p>The sad part is you would never remember it for so long...</p>
        <h5>Until now.</h5>
  <p><a class="btn btn-success btn-hg" href="Reviews/My">Get Started</a></p>
</div>
</asp:Content>