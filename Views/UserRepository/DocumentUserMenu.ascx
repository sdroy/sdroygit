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
     <a id="download" <% if (jList != "")
            { %> href='javascript:download();' <% 
        } %> style="padding-left: .4em;"><span>Download Files</span></a>
</div>
<%} %>
<script type="text/javascript">
     

   

  

   
    

    function download() {
        var list = '<%= jList %>';
        var path =   "/UserRepository/DownloadFiles/?jlist=" + list;
        window.location.href = path;
    }
    

     

</script>
