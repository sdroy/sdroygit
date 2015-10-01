

var currentOOTSPath = '';

function getApplicationPath() {
    var path = location.pathname;
    var index = path.indexOf("/Repository");
    if (index > -1)
        return path.substr(0, index + 1);
    if (path.charAt(path.length - 1) != '/')
        path += '/';
    return path;
}

function selectFolder(folder) {
    var grid = $("#DocumentsGrid").data('tGrid');
    grid.rebind({ filePath: folder });
    loadActionLinks(folder);
}


function loadActionLinks(file) {
    var path = getApplicationPath();
    $.ajax({
        type: "POST",
        url: path + "Repository/GetDocumentAdminMenu",
        data: { path: file },
        cache: false,
        dataType: "html",
        success: function (data) {
            $('#commands').html(data);
        },
        error: function (req, status, error) {

            alert("failed.");
        }
    });
}
