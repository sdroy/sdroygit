<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ManageRoles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <h2>
            Manage Roles</h2>
        <p>
            <b>Create a New Role: </b>
            <input type="text" id="txtRoleName" name="txtRoleName" style = "width: 250px;" />
            <br />
            <input type="button" id="btnCreateRoleButton" onclick="CreateRole()" value="Create Role" />
        </p>
        <p>
        </p>
        
        <p>
            <b>Delete a Existing Role:</b>
            <% 
                List<SelectListItem> roleList = ViewData["RolesData"] as List<SelectListItem>;
            %>
            <%=  Html.DropDownList("RoleList", roleList, new { style = "width: 250px;" })%>
            <br />
            <input type="button" id="btnDeleteRole" onclick="DeleteRole()" value="Delete Role" />
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
<script type="text/javascript">
    function CreateRole() {
        roleName = $("#txtRoleName").attr("value");        
        $.ajax({
            type: "POST",
            url: "/UserAdminRepository/CreateRole",
            data: { newRoleName: roleName },
            cache: false,
            success: function (data) {
                alert("Done");
                window.location.href = "/UserAdminRepository/ManageRoles";
            }
        })
    }
    function DeleteRole() {
        roleName = $("#RoleList").attr("value");        
        $.ajax({
            type: "POST",
            url: "/UserAdminRepository/DeleteRole",
            data: { newRoleName: roleName },
            cache: false,
            success: function (data) {
                alert("Done");
                window.location.href = "/UserAdminRepository/ManageRoles";
            }
        })
    }

</script>
</asp:Content>
