PreviewImage = function (uri) {
    imageDialog = $("#imagePopUp");
    imageTag = $('#image');
    uriParts = uri.split("/");
    imageTag.attr('src', uri);
    $('#imageLink').attr('href', uri);
    imageTag.load(function () {
        $('#imagePopUp').dialog({
            modal: true,
            resizable: true,
            draggable: true,
            width: 'auto',
            height: 'auto',
            title: uriParts[uriParts.length - 1]
        });
    });
}

$(function () {
    $.extend($.fn.disableTextSelect = function () {
        return this.each(function () {
            if ($.browser.mozilla) {//Firefox
                $(this).css('MozUserSelect', 'none');
            } else if ($.browser.msie) {//IE
                $(this).bind('selectstart', function () { return false; });
            } else {//Opera, etc.
                $(this).mousedown(function () { return false; });
            }
        });
    });

});

function onRowSelected(e) {
    var grid = $(this).data('tGrid');
    var filePath = e.row.cells[grid.columns.length - 1].innerHTML;
    var isFolder = e.row.cells[grid.columns.length - 2].innerHTML == "true";
    if (isFolder) {
        grid.rebind({ filePath: filePath });
        loadActionLinks(filePath);
    } else {
        var path = filePath;
        var pos = path.indexOf("OOTSFiles") - 1;
        var filename = path.substring(pos);
        PreviewImage(filename);
    }
}