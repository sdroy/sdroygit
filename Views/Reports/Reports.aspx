<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Reports
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content" align="center">
        <h2>
            Reports</h2>
        <h3>
            Total Documents Downloaded Report</h3>
        <p>
            <br />
            <input type="button" id="Button2" onclick="GenerateTotalDocumentsDownloadedReport();"
                value="Generate Total Documents Downloaded Report" />
        </p>
        <p>
            <h3>
                Documents Downloaded Report for a Specific User</h3>
            <p>
                <b>Select a User:</b>
                <% 
                    List<SelectListItem> list = ViewData["UsersData"] as List<SelectListItem>;
                %>
                <%=  Html.DropDownList("UserListForDocumentsDownloadedReport", list, "Select")%>
                <br />
                <input type="button" id="Button3" onclick="GenerateDocumentsDownloadedReportBySpecificUser();"
                    value="Generate Documents Downloaded Report for the Selected User" />
            </p>
        </p>
        <p>
            <h3>
                User Activity Report for a Specific User</h3>
            <p>
                <b>Select a User:</b>
                <%=  Html.DropDownList("UserListForUserActivityReport", list, "Select")%>
                <br />
                <input type="button" id="Button1" onclick="GenerateUserActivityReportBySpecificUser();"
                    value="Generate User Activity Report for the Selected User" />
            </p>
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
    <script src="/Scripts/jquery.cascadingDropDown.js" type="text/javascript"></script>
    <script type="text/javascript">

        function GenerateTotalDocumentsDownloadedReport() {
            var path = "/Reports/GenerateTotalDocumentsDownloadedReport";
            window.location.href = path;

        }

        function GenerateDocumentsDownloadedReportBySpecificUser() {
            userName = $("#UserListForDocumentsDownloadedReport option:selected").text();
            var path = "/Reports/GenerateDocumentsDownloadedReportForASpecificUser?userName=" + userName;
            window.location.href = path;
 
        }

        function GenerateUserActivityReportBySpecificUser() {
            userName = $("#UserListForUserActivityReport option:selected").text();
            var path = "/Reports/GenerateUserActivityReportForASpecificUser?userName=" + userName;
            window.location.href = path;            
        }

      

    </script>
</asp:Content>
