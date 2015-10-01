<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    OOTS Files
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="listpanel">
        <div id="leftBorder">
            <% Html.RenderPartial("DocumentsGrid", Model);%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="server">
    <div id="header">
        <div>
        </div>
        <div id="commands">
            <% Html.RenderPartial("DocumentAdminMenu", Model);%>
        </div>
    </div>
    <% Html.RenderPartial("RepositoryTreeView");%>
</asp:Content>
