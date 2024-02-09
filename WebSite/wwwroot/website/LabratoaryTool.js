$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_LabratoaryTool();
});

function LoadTable_LabratoaryTool() {
    $('#tblLabratoaryTool').DataTable({
        pageLength: 18,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "bInfo": false,
        "lengthChange": false,
        "order": [],
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "LabratoaryTool/GetActiveTools",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [
            { "data": "toolName" }, 
            {
                "render": function (data, type, row) {
                    if (row.isActive) {
                        return '   <input type="checkbox" class="form-check-input" disabled checked="' + row.isActive + '">'
                    }
                    else {
                        return '   <input type="checkbox" class="form-check-input" disabled>'
                    }

                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_Update(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteLabratoaryTool(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}

//================================ Insert ====================================================
function ShowPopUp_Insert() {
    $(".alertBox").hide();
    RestFormObjects();
    $("#insertLabratoaryToolModal").modal("show");
}
function InsertLabratoaryTool() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }

    var toolName = $('#txt_ToolName').val();
    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'LabratoaryTool/AddLabratoaryTool',
        data: {
            ToolName: toolName,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertLabratoaryToolModal").modal("hide");
            $('#tblLabratoaryTool').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertLabratoaryToolModal").modal("hide");
}
function validate_Insert() {

    var toolName = $('#txt_ToolName').val();
    if (toolName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام ابزار را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() { 
    $("#updateLabratoaryToolModal").modal("show"); 
}
function ShowPopUp_Update(id) {
    $('#tblLabratoaryTool tbody').on('click', 'tr', function () {

        var row = $('#tblLabratoaryTool').DataTable().row(this).data();

        $('#txt_ToolName_update').val(row.toolName);
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
    });

    $("#updateLabratoaryToolModal").modal("show");
    $('#labratoaryToolId_Selected').val(id);
}
function UpdateLabratoaryTool() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#labratoaryToolId_Selected').val();
    var toolName = $('#txt_ToolName_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'LabratoaryTool/UpdateLabratoaryTool',
        data: {
            Id: id,
            ToolName: toolName,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateLabratoaryToolModal").modal("hide");
            $('#tblLabratoaryTool').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateLabratoaryToolModal").modal("hide");
}
function validate_Update() {

    var toolName = $('#txt_ToolName_update').val();
    if (toolName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام ابزار را وارد نمایید");
        return false;
    }
    return true;
} 

//================================ DELETE ====================================================
function deleteLabratoaryTool(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/LabratoaryTool/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {
 
    $('#txt_ToolName').val('');
    $('#txt_ToolName_update').val('');
    $('#labratoaryToolId_Selected').val('');
    $(".alertBox").hide();
}