<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UsersAndRoles
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content" align="center">
        <h2>
            User Expiry Management</h2>
        <p>
            <p>
                <div>
                    <b>Select a User:</b>
                    <% 
                        List<SelectListItem> list = ViewData["UsersData"] as List<SelectListItem>;
                    %>
                    <%=  Html.DropDownList("UserList", list,"Select")%>
                </div>
                <div>
                    <p>
                        <label for="ExpiryDate">
                            Expiry Date:</label>
                        <%= Html.TextBox("ExpiryDate")%>
                    </p>
                </div>
            </p>
        </p>
        <p>
            <div>
                <input type="button" id="Button1" onclick="SetUserExpiry();" value="Set User Expiry Date for the Selected User" />
            </div>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
    <<script src="/Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            // Handler for .ready() called.           
            $('#ExpiryDate').datepicker({
                dateFormat: 'dd-mm-yy'
                 
            });

        });

        function SetUserExpiry() {
            userName1 = $("#UserList option:selected").text(); ;
            expiryDate1 = $("#ExpiryDate").val(); //   $("#ExpiryDate").datepicker("getDate");
            jQuery.ajax({ 
                type: "POST",
                url: "/UserAdminRepository/SetUserExpiry",
                data: { userName: userName1, expiryDate: expiryDate1 },
                cache: false,
                success: function (data) {
                    alert("Done");                    
                }
            })
        }

   

    </script>
</asp:Content>
