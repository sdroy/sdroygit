<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FileModel>" %>
<%
    List<string> list = ViewData["CheckedList"] as List<string>;
    string jList = "";
    if (list != null && list.Count > 0)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        jList = serializer.Serialize(list);
    }

    { %>
<div style="position: relative; top: 0; padding-top: 12px; padding-left: 10px;">
       <a id="A1" <% if (jList != "")
            { %> href='javascript:AssignTheSelectedFilesToTheSelectedUser();' <% 
        } %> style="padding-left: .4em;"><span>Assign the selected files or Folders to the selected user</span></a> 
        |<a id="A2" <% if (jList != "")
            { %> href='javascript:AssignTheSelectedFilesToTheSelectedRole();' <% 
        } %> style="padding-left: .4em;"><span>Assign the selected files or Folders to the selected role</span></a> 
        |<a id="download"
            <% if (jList != "")
            { %> href='javascript:download();' <% 
        } %> style="padding-left: .4em;"><span>Download Files</span></a>
</div>
<%} %>
<script type="text/javascript">
    function AssignTheSelectedFilesToTheSelectedUser() {
        var list = '<%= jList %>';
        userID = $("#UserList").attr("value");      
        $.ajax({
            type: "POST",
            url:  "/UserAdminRepository/AssignTheSelectedFilesToTheSelectedUser",
            data: { jlist: list, userID: userID },
            cache: false,
            success: function (data) {
                alert("Done");
            }
        })
    }

    function AssignTheSelectedFilesToTheSelectedRole() {
        var list = '<%= jList %>';
        roleName = $("#RoleList").attr("value");    
        $.ajax({
            type: "POST",
            url:  "/UserAdminRepository/AssignTheSelectedFilesToTheSelectedRole",
            data: { jlist: list, roleName: roleName},
            cache: false,
            success: function (data) {
                alert("Done");
            }
        })
    }

    function download() {
        var list = '<%= jList %>';
        var path = "/UserAdminRepository/DownloadFiles/?jlist=" + list;
        window.location.href = path;
    }
</script>
