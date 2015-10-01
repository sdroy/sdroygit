<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UsersAndRoles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content" align="center">
        <h2>
            User Role Management</h2>
        <h3>
            Manage Roles By User</h3>
        <p>
            <p>
                <b>Select a User:</b>
                <% 
                    List<SelectListItem> list = ViewData["UsersData"] as List<SelectListItem>;
                %>
                 
                <%=  Html.DropDownList("UserListForDeletion", list,"Select")%>
                
            </p>
        </p>
        <p>
            <b>Roles For the Selected User:</b>
           <% 
                    List<SelectListItem> emptylist =  new List<SelectListItem>();
                %>
            <%=  Html.DropDownList("SelectedRoleList", emptylist)%>
            <br />
            <input type="button" id="Button1" onclick="RemoveUserFromRole();" value="Remove User from the Selected Role" />
        </p>
        <h3>
            Manage Users By Role</h3>
        <p>
            <p>
                <b>Select a Role:</b>
                <% 
                    List<SelectListItem> roleList = ViewData["RolesData"] as List<SelectListItem>;                    
                %>
                <%=  Html.DropDownList("RoleList", roleList)%>
            </p>
        </p>
        <p>
            <p>
                <b>Select a User:</b>
                <%=  Html.DropDownList("UserList", list)  %>
            </p>
            <br />
            <input type="button" id="btnAddUserToRoleButton" onclick="AddUserToRole()" value="Add User to Role" />
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
<script src="/Scripts/jquery.cascadingDropDown.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            // Handler for .ready() called.

            $("#SelectedRoleList").CascadingDropDown("#UserListForDeletion", '/UserAdminRepository/GetRolesForTheSelectedUser');
        });

        function AddUserToRole() {
            userName = $("#UserList option:selected").text(); ;
            roleName = $("#RoleList option:selected").text();
            $.ajax({
                type: "POST",
                url: "/UserAdminRepository/AddUserToRole",
                data: { selectedUserName: userName, roleName: roleName },
                cache: false,
                success: function (data) {
                    alert("Done");
                    window.location.href = "/UserAdminRepository/UsersAndRoles";
                }
            })
        }

        function RemoveUserFromRole() {
            userName = $("#UserListForDeletion option:selected").text(); ;
            roleName = $("#SelectedRoleList option:selected").text();
            $.ajax({
                type: "POST",
                url: "/UserAdminRepository/RemoveUserFromRole",
                data: { selectedUserName: userName, roleName: roleName },
                cache: false,
                success: function (data) {
                    alert("Done");
                    window.location.href = "/UserAdminRepository/UsersAndRoles";
                }
            })
        }

    </script>
</asp:Content>
