<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    OOTS Files
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="floatLeft">
        <div>
            <h3>
                Manage User Permission</h3>
            <p>
                <b>Select a User:</b>
                <% 
                    List<SelectListItem> list = ViewData["UsersData"] as List<SelectListItem>;
                %>
                <%=  Html.DropDownList("UserList", list)  %>
            </p>
        </div>
        <div>
         <h3>
                Manage Role Permission</h3>
            <p>
                <b>Select a Role:</b>
                <% 
                    List<SelectListItem> roleList = ViewData["RolesData"] as List<SelectListItem>;
                %>
                <%=  Html.DropDownList("RoleList", roleList)%>
            </p>
        </div>
    </div>
    <div id="listpanel">
        <div id="leftBorder">
            <% Html.RenderPartial("DocumentsUserAdminGrid", Model);%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="server">
    <div id="header">
        <div>
        </div>
        <div id="commands">
            <% Html.RenderPartial("DocumentUserAdminMenu", Model);%>
        </div>
    </div>
</asp:Content>
