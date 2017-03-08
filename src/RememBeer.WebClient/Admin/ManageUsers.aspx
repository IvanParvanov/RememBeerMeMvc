<%@ Page Title="Admin - Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="RememBeer.WebClient.Admin.ManageUsers" %>
<%@ Register TagPrefix="uc" TagName="Notifier" Src="~/UserControls/UserNotifications.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="GridViewUpdate">
        <ContentTemplate>
            <uc:Notifier runat="server" ID="Notification" ViewStateMode="Disabled"></uc:Notifier>
            <div class="container">
                <div class="form-group form-inline">
                    <asp:TextBox CssClass="form-control" runat="server" ID="SearchTb" placeholder="Username/Email"></asp:TextBox>
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Search" OnClick="Search_OnClick"/>
                </div>
                <asp:GridView ID="UserGridView" runat="server"
                              ItemType="RememBeer.Models.Contracts.IApplicationUser"
                              CssClass="table table-bordered table-striped table-hover table-responsive"
                              AllowPaging="True"
                              AutoGenerateColumns="False"
                              AutoGenerateEditButton="True"
                              AllowCustomPaging="True"
                              OnPageIndexChanging="UserGridView_OnPageIndexChanging"
                              OnRowEditing="UserGridView_OnRowEditing"
                              OnRowUpdating="UserGridView_OnRowUpdating"
                              OnRowCancelingEdit="UserGridView_OnRowCancelingEdit"
                              PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" AccessibleHeaderText="Id">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#: Eval("Id") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="IdTb" CssClass="form-control" runat="server" Text='<%# Bind("Id") %>' ReadOnly="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email" AccessibleHeaderText="Email">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#: Eval("Email") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="EmailTb" CssClass="form-control" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirmed" AccessibleHeaderText="Confirmed">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#: Eval("EmailConfirmed") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="ConfirmCheckbox" CssClass="form-control" runat="server" Checked='<%# Bind("EmailConfirmed") %>'></asp:CheckBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Admin" AccessibleHeaderText="Admin">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#: Item.Roles.Any() ? "True" : "False" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Locked Until" AccessibleHeaderText="Locked Until">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%#: Eval("LockoutEndDateUtc") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="LockoutTb" CssClass="form-control" runat="server" Text='<%#: Eval("LockoutEndDateUtc") %>' ReadOnly="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disable/Enable" AccessibleHeaderText="Disable/Enable">
                            <ItemTemplate>
                                <asp:Button runat="server" CssClass="btn btn-primary btn-sm" CommandName="EnableUser" Text="Enable" CommandArgument='<%#: Eval("Id") %>' OnCommand="OnUserCommand"/>
                                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" CommandName="DisableUser" Text="Disable" CommandArgument='<%#: Eval("Id") %>' OnCommand="OnUserCommand"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Admin" AccessibleHeaderText="Make Admin">
                            <ItemTemplate>
                                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" CommandName="MakeAdmin" Text="Make Admin" CommandArgument='<%# Eval("Id") %>' OnCommand="OnUserCommand"/>
                                <asp:Button runat="server" CssClass="btn btn-warning btn-sm" CommandName="RemoveAdmin" Text="Remove Admin" CommandArgument='<%# Eval("Id") %>' OnCommand="OnUserCommand"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reviews" AccessibleHeaderText="Reviews">
                            <ItemTemplate>
                                <a class="btn btn-primary" href='<%#: "UserDetails?id=" + Eval("Id") %>'><i class="fa fa-arrow-right"></i></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>