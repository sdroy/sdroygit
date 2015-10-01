<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OOTS.Web.Models.FileModel>" %>
<script src="<%= Url.Content("~/Scripts/jQuery-1.4.4.js") %>" type="text/javascript"></script>
<% Html.Telerik().Grid<FileModel>()
        .Name("DocumentsGrid")
        .DataKeys(key => key.Add(x => x.FullPath))
        .Columns(columns =>
        {
            columns.Bound(x => x.FullPath).Format("<input type='checkbox'  value='{0}'>").Encoded(false).Width(22).Title("");
            columns.Bound(x => x.Name).ClientTemplate("<img width='16' height='16' alt='<#= CategoryText #>' src='"
                                                        + Url.Content("~/Content/Images/")
                                                        + "<#= CategoryText #>.png'  style= 'vertical-align:middle;'/>"
                                                        + "<span id='<#= FullPath #>_span' style='padding-left: 2px;'> <#= Name #></span>")
                                       .Title("Name");
            columns.Bound(x => x.NiceNameOrAreaName).Title("Nice Name").ReadOnly(true);
            columns.Bound(x => x.Description).Title("Description").ReadOnly(true);
            columns.Bound(x => x.Created).Format("{0:g}").Title("Date created").ReadOnly(true);
            columns.Bound(x => x.Accessed).Format("{0:g}").Title("Date modified").ReadOnly(true);
            columns.Bound(x => x.IsFolder).Hidden(true);
            columns.Bound(x => x.FullPath).Hidden(true);

        })
        .DataBinding(dataBinding => dataBinding.Ajax()
                    .Select("SelectFiles", "UserAdminRepository", new { filePath = Model.FullPath })
                    )
        .Pageable(pager => pager.PageSize(Int32.MaxValue).Style(GridPagerStyles.Status))
        .Sortable(sorting => sorting.OrderBy(sortorder => sortorder.Add(x => x.Accessed).Descending()))
        .Selectable()
        .ClientEvents(events => events.OnRowSelect("onRowSelected_userAdminGrid").OnDataBound("onDocumentsGridDataBound"))
        .HtmlAttributes(new { style = "text-align:left; border:none;" })
        .Render();
%>
<div id="imagePopUp" title="An Image!" style="display: none;">
<div>
  <img id="image"   width="500"   src=""/>
  </div>
  <div></div>
  <a id="imageLink" href="#"  > It is just a preview. Click here to view the orginal image. </a>
  </div>
</div>
<script type="text/javascript">
    function onDocumentsGridDataBound(e) {
        $(':checkbox').click(function () {
            menuLoad();
        });
        $('.noselect').disableTextSelect(); //No text selection on elements with a class of 'noSelect'
    }

    function menuLoad() {
        var list = new Object();
        var i = 0;
        var filename = currentOOTSPath;
        var path = getApplicationPath();
        $("input:checkbox:checked").each(function () {
            list[i++] = $(this).val();
        });
        $.ajax({
            type: "POST",
            url: "/UserAdminRepository/GetDocumentUserAdminMenu",
            data: { path: filename, list: list },
            cache: false,
            dataType: "html",
            success: function (data) {
                $('#commands').html(data);
            }
        });
    }

    function onRowSelected_userAdminGrid(e) {
        var grid = $(this).data('tGrid');
        var filePath = e.row.cells[grid.columns.length - 1].innerHTML;
        var isFolder = e.row.cells[grid.columns.length - 2].innerHTML == "true";
        if (isFolder) {
            grid.rebind({ filePath: filePath });
            menuLoad();
        } else {
            var path = filePath;
            var pos = path.indexOf("OOTSFiles") - 1;
            var filename = path.substring(pos);
            PreviewImage(filename);
        }
    }
</script>
