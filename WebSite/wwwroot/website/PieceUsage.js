$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_PieceUsage();
});

function LoadTable_PieceUsage() {
    $('#tblPieceUsage').DataTable({
        pageLength: 18,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "bInfo": false,
        "order": [],
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "PieceUsage/GetPieceUsages",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "usageName" }, 
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deletePieceUsage(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}

//================================ Insert ====================================================
function ShowPopUp_Insert() {
    RestFormObjects();
    $(".alertBox").hide();
    $("#insertPieceUsageModal").modal("show");
}
function InsertPieceUsage() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }

    var usageName = $('#txt_UsageName').val();
    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'PieceUsage/AddPieceUsage',
        data: {
            UsageName: usageName,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertPieceUsageModal").modal("hide");
            $('#tblPieceUsage').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertPieceUsageModal").modal("hide");
}
function validate_Insert() {

    var usageName = $('#txt_UsageName').val();
    if (usageName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updatePieceUsageModal").modal("show"); 
}

function ShowPopUp_Update(id) {
    $('#tblPieceUsage tbody').on('click', 'tr', function () {

        var row = $('#tblPieceUsage').DataTable().row(this).data();

        $('#txt_UsageName_update').val(row.usageName); 
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        } 
    });

    $("#updatePieceUsageModal").modal("show");
    $('#pieceUsageId_Selected').val(id);
}

function UpdatePieceUsage() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#pieceUsageId_Selected').val();
    var usageName = $('#txt_UsageName_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'PieceUsage/UpdatePieceUsage',
        data: {
            Id: id,
            UsageName: usageName,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updatePieceUsageModal").modal("hide");
            $('#tblPieceUsage').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updatePieceUsageModal").modal("hide");
}
function validate_Update() {
     
    var usageName = $('#txt_UsageName_update').val();
    if (usageName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد را وارد نمایید");
        return false;
    }
    return true;
}


//================================ DELETE ====================================================
function deletePieceUsage(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/PieceUsage/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {
   
    $('#txt_UsageName').val('');
    $('#txt_UsageName_update').val('');
    $('#PieceUsageId_Selected').val(''); 
    $(".alertBox").hide();
}