<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserNotifications.ascx.cs" Inherits="RememBeer.WebClient.UserControls.UserNotifications" %>

<asp:PlaceHolder runat="server" ID="SuccessMessagePlaceholder" Visible="false">
    <div class="alert alert-dismissible alert-success">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <asp:Label ID="SuccessMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="WarningMessagePlaceholder" Visible="false">
    <div class="alert alert-dismissible alert-warning">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <asp:Label ID="WarningMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="ErrorMessagePlaceholder" Visible="false">
    <div class="alert alert-dismissible alert-danger">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <asp:Label ID="ErrorMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:PlaceHolder>