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
<div id="dialog" title="Upload files">
    <% using (Html.BeginForm("Upload", "Repository", new { path = Model.FullPath }, FormMethod.Post, new { enctype = "multipart/form-data", id = "uploadfrm" }))
       {%>
    <p>
        <input type="file" id="fileUpload" name="fileUpload" size="23" />
    </p>   
    <p>
        Nice Name</p>
    <input type="text" id="txtNiceName" name="txtNiceName" style="width: 100%;"/>
    <p>
        Description</p>
    <input type="text" id="txtDescription" name="txtDescription" style="width: 100%;"/>
     <p>
        <input type="submit" value="Upload file" /></p>
    <% } %>
</div>
<div style="position: relative; top: 0; padding-top: 12px; padding-left: 10px;">
    <a id="A1" href='javascript:CreateNewFolder("<%= Model.FullPath %>");' style="padding-left: .4em;">
        <span>Create New Folder</span></a> | <a id="A3" <% if (jList != "" && list.Count == 1)
            { %> href='javascript:Rename("<%= Model.FullPath %>");' <% 
        } %> style="padding-left: .4em;"><span>Rename</span></a> | <a id="A2" <% if (jList != "")
            { %> href='javascript:deletefile("<%= Model.FullPath %>");' <% 
        } %> style="padding-left: .4em;"><span>Delete</span></a> | <a href="#" onclick='javascript:OpenUploadDialog("<%= Model.FullPath %>");'>
            Upload File</a> | <a id="download" <% if (jList != "")
            { %> href='javascript:download();' <% 
        } %> style="padding-left: .4em;"><span>Download Files</span></a>
</div>
<%} %>
<script type="text/javascript">
    $(function () {
        $("#dialog").dialog({
            bgiframe: true,
            height: 300,
            modal: true,
            autoOpen: false,
            resizable: false
        })
    });

    function OpenUploadDialog(path) {

        jQuery('#dialog').dialog('open');
        jQuery('#uploadfrm').attr('action', '/Repository/Upload?path=' + path);
        var options = {
            success: function () { jQuery('#dialog').dialog('close'); selectFolder(path); }
        };
        $('#uploadfrm').ajaxForm(options);
        return false
    }

    function CreateNewFolder(path) {
        getFoldername(path);
    }

    function CreateNewFolderCallback(path, foldername) {
        $.ajax({
            type: "POST",
            url: getApplicationPath() + "Repository/CreateNewFolder",
            data: { path: path, foldername: foldername },
            cache: false,
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    selectFolder(data);
                }
            }
        })
    }

    function getFoldername(path) {
        var win = $('<div><p>Enter your new folder name</p></div>');
        var userInput = $('<input type="text" style="width:100%"></input>');
        userInput.appendTo(win);
        //        $('<p>Area Name</p>').appendTo(win);
        //        var userInput1 = $('<input type="text" style="width:100%"></input>');
        //        userInput1.appendTo(win);
        //        $('<p>Description</p>').appendTo(win);
        //        var userInput2 = $('<input type="text" style="width:100%"></input>');
        //        userInput2.appendTo(win);
        var userValue = '';
        $(win).dialog({
            'modal': true,
            'buttons': {
                'Ok': function () {
                    folderName = $(userInput).val();
                    //                    areaName = $(userInput1).val();
                    //                    description = $(userInput1).val();
                    CreateNewFolderCallback(path, folderName);
                    $(this).dialog('close');

                },
                'Cancel': function () {
                    $(this).dialog('close');
                }
            }
        });

        return userValue;
    }

    function download() {
        var list = '<%= jList %>';
        var path = getApplicationPath() + "Repository/DownloadFiles/?jlist=" + list;
        window.location.href = path;
    }
    function deletefile(path1) {
        var list = '<%= jList %>';
        $.ajax({
            type: "POST",
            url: getApplicationPath() + "Repository/DeleteFiles/?jlist=" + list,
            cache: false,
            success: function (data) {
                selectFolder(path1);
            }
        })


    }

    function Rename(path1) {

        GetNewName(path1);
    }

    function RenameCallback(path1, newname) {
        var list = '<%= jList %>';
        $.ajax({
            type: "POST",
            url: getApplicationPath() + "Repository/Rename",
            data: { jlist: list, path: path1, newname: newname },
            cache: false,
            success: function (data) {
                selectFolder(path1);
            }
        })




    }

    function GetNewName(path) {
        var win = $('<div><p>Enter the new name</p></div>');
        var userInput = $('<input type="text" style="width:100%"></input>');
        userInput.appendTo(win);
        var userValue = '';
        $(win).dialog({
            'modal': true,
            'buttons': {
                'Ok': function () {
                    userValue = $(userInput).val();
                    RenameCallback(path, userValue);
                    $(this).dialog('close');
                },
                'Cancel': function () {
                    $(this).dialog('close');
                }
            }
        });

        return userValue;
    }

</script>
