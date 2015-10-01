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
        <div id="TotalDocumentsDownloadedReportReportPopUp" style="display: none;">
            <div>
                <%   
                    
                   

                    Html.Telerik().Grid<OOTS.Models.FilesDownloadAuditTrailViewModel>()
        .Name("TotalReportGrid")
        .DataKeys(key => key.Add(x => x.FileID))
        .Columns(columns =>
        {
            columns.Bound(x => x.NiceNameOrAreaName).Title("Nice Name").ReadOnly(true);
            columns.Bound(x => x.DateTimeDownloaded).Format("{0:g}").Title("DateTimeDownloaded").ReadOnly(true);

        })
        .DataBinding(dataBinding => dataBinding.Ajax()
                     .Select("GetTotalDocumentsDownloadedReport", "Reports", new { isLoad = "false" })
                    )
        .Pageable(pager => pager.PageSize(Int32.MaxValue).Style(GridPagerStyles.Status))
        .Sortable(sorting => sorting.OrderBy(sortorder => sortorder.Add(x => x.DateTimeDownloaded).Descending()))
        
        .HtmlAttributes(new { style = "text-align:left; border:none;" })
        .Render();
                  
                %>
            </div>
        </div>
   
   <div id="DocumentsDownloadedReportBySpecificUserPOPUP" style="display: none;">
   <div>
   <span><b>User Name: </b></span>
   <span id="txtUsername"></span>
   </div>
  <br />
            <div>
                <%   
                    
                   

                    Html.Telerik().Grid<OOTS.Models.FilesDownloadAuditTrailViewModel>()
        .Name("DocumentsDownloadedReportBySpecificUserGrid")
        .DataKeys(key => key.Add(x => x.FileID))
        .Columns(columns =>
        {
            columns.Bound(x => x.NiceNameOrAreaName).Title("Nice Name").ReadOnly(true);
            columns.Bound(x => x.DateTimeDownloaded).Format("{0:g}").Title("DateTimeDownloaded").ReadOnly(true);

        })
        .DataBinding(dataBinding => dataBinding.Ajax()
                     .Select("GetDocumentsDownloadedReportBySpecificUser", "Reports", new { userName = "" })
                    )
        .Pageable(pager => pager.PageSize(Int32.MaxValue).Style(GridPagerStyles.Status))
        .Sortable(sorting => sorting.OrderBy(sortorder => sortorder.Add(x => x.DateTimeDownloaded).Descending()))
        
        .HtmlAttributes(new { style = "text-align:left; border:none;" })
        .Render();
                  
                %>
            </div>
        </div>
   
   
   
    <div id="UserActivityReportBySpecificUserPopUP" style="display: none;">
   <div>
   <span><b>User Name: </b></span>
   <span id="txtUserNameactivity"></span>
   </div>
  <br />
            <div>
                <%   
                    
                   

                    Html.Telerik().Grid<OOTS.Models.UserLoginAuditTrailViewModel>()
        .Name("UserActivityReportBySpecificUserGrid")
        .DataKeys(key => key.Add(x => x.DateTimeLogged))
        .Columns(columns =>
        {
            columns.Bound(x => x.UserName).Title("UserName").ReadOnly(true);
            columns.Bound(x => x.DateTimeLogged).Title("DateTimeLogged").ReadOnly(true);

        })
        .DataBinding(dataBinding => dataBinding.Ajax()
                     .Select("GetUserActivityReportForASpecificUser", "Reports", new { userName = "" })
                    )
        .Pageable(pager => pager.PageSize(Int32.MaxValue).Style(GridPagerStyles.Status))
        .Sortable(sorting => sorting.OrderBy(sortorder => sortorder.Add(x => x.DateTimeLogged).Descending()))
         
        .HtmlAttributes(new { style = "text-align:left; border:none;" })
        .Render();
                  
                %>
            </div>
        </div>
   
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
    <script src="/Scripts/jquery.cascadingDropDown.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GenerateTotalDocumentsDownloadedReport() {

            

            var grid = $("#TotalReportGrid").data('tGrid');
            grid.rebind({ isLoad: "True"});

            $('#TotalDocumentsDownloadedReportReportPopUp').dialog({
                modal: true,
                resizable: true,
                draggable: true,
                minHeight: 500,  
                minWidth: 800,  
                //width: '800',
                //height: 'auto',
                title: "Report Name: Total Documents Downloaded with Dates and Times"
            });



        }

        function GenerateDocumentsDownloadedReportBySpecificUser() {
            userName = $("#UserListForDocumentsDownloadedReport option:selected").text();
            $('#txtUsername').html(userName);
            var grid = $("#DocumentsDownloadedReportBySpecificUserGrid").data('tGrid');
            grid.rebind({ userName: userName });

            $('#DocumentsDownloadedReportBySpecificUserPOPUP').dialog({
                modal: true,
                resizable: true,
                draggable: true,
                minHeight: 500,
                minWidth: 800,              
                title: "Report Name: Documents Downloaded with Dates and Times"
            });

        }

        function GenerateUserActivityReportBySpecificUser() {
            userName = $("#UserListForUserActivityReport option:selected").text();
            $('#txtUserNameactivity').html(userName);
            var grid = $("#UserActivityReportBySpecificUserGrid").data('tGrid');
            grid.rebind({ userName: userName });

            $('#UserActivityReportBySpecificUserPopUP').dialog({
                modal: true,
                resizable: true,
                draggable: true,
                minHeight: 500,
                minWidth: 800,              
                title: "Report Name: User Activity with Dates and Times"
            });
         
        }

    </script>
</asp:Content>
