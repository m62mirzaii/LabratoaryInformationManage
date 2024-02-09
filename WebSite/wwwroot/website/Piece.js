$(document).ready(function () {

    $("#alertBox").hide();

    LoadTable_Piece();
    Load_ComboBox(); 
});

function LoadTable_Piece() {
    $('#tblPiece').DataTable({
        pageLength: 18,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "order": [],
        "searching": true,
        "bInfo": false,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "Piece/GetPieces",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "code" },
            { "data": "pieceName" },
            { "data": "pieceUsageName" },

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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deletePiece(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
} 
function Load_ComboBox() {

    $("#pieceUsage").select2({
        dropdownParent: $("#insertPieceModal"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "PieceUsage/PieceUsageForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.usageName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#pieceUsageId").val(getID[0]['id']);
    });

    $("#pieceUsage_update").select2({
        dropdownParent: $("#updatePieceModal"),
        allowClear: true,
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "PieceUsage/PieceUsageForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.usageName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#pieceUsageId_update").val(getID[0]['id']);
    });
}


//================================ Insert ====================================================
function ShowPopUp_Insert() {
    $("#insertPieceModal").modal("show");
}
function InsertPiece() {

    //if (!validate_Insert()) {
    //    return false;
    //}

    var code = $('#txt_Code').val();
    var pieceName = $('#txt_PieceName').val();
    var pieceUsageId = $('#pieceUsageId').val();
    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'Piece/InsertPiece',
        data: {
            Code: code,
            PieceName: pieceName,
            PieceUsageId: pieceUsageId,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertPieceModal").modal("hide");
            $('#tblPiece').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertPieceModal").modal("hide");
}
function validate_Insert() {

    var code = $('#txt_Code').val();
    if (code == null || code == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کد قطعه را وارد نمایید");
        return false;
    }

    var pieceName = $('#txt_PieceName').val();
    if (pieceName == null || pieceName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام قطعه را وارد نمایید");
        return false;
    }

    var pieceUsageId = $('#pieceUsageId').val();
    if (pieceUsageId == null || pieceUsageId == "" || pieceUsageId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد قطعه را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updatePieceModal").modal("show");
   
} 
function ShowPopUp_Update(id) {
    $('#tblPiece tbody').on('click', 'tr', function () {

        var row = $('#tblPiece').DataTable().row(this).data();
         
        $('#txt_Code_update').val(row.code);
        $('#txt_PieceName_update').val(row.pieceName);
        $('#pieceUsageId_update').val(row.pieceUsageId);  
        $('#chk_IsActive_update').prop("checked", false); 
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
         
        $("#pieceUsage_update").select2("trigger", "select", { data: { id: row.pieceUsageId, text: row.pieceUsageName } });
    });

    $("#updatePieceModal").modal("show");
    $('#pieceId_Selected').val(id);
} 
function UpdatePiece() {
   
    //if (!validate_Insert()) {
    //    return false;
    //}

    var id = $('#pieceId_Selected').val();
    var code = $('#txt_Code_update').val();
    var pieceName = $('#txt_PieceName_update').val();
    var pieceUsageId = $('#pieceUsageId_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'Piece/UpdatePiece',
        data: {
            Id: id,
            Code: code,
            PieceName: pieceName,
            PieceUsageId: pieceUsageId,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updatePieceModal").modal("hide");
            $('#tblPiece').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
} 
function ClosePopUp_Update() {
    RestFormObjects();
    $("#updatePieceModal").modal("hide");
}
function validate_Update() {

    var code = $('#txt_Code_update').val();
    if (code == null || code == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کد قطعه را وارد نمایید");
        return false;
    }

    var pieceName = $('#txt_PieceName_update').val();
    if (pieceName == null || pieceName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام قطعه را وارد نمایید");
        return false;
    }

    var pieceUsageId = $('#pieceUsageId_update').val();
    if (pieceUsageId == null || pieceUsageId == "" || pieceUsageId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد قطعه را وارد نمایید");
        return false;
    }
    return true;
}
 

//================================ DELETE ====================================================
function deletePiece(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/Piece/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Code').val('');
    $('#txt_PieceName').val('');
    $('#pieceUsageId').val(''); 

    $('#pieceId_Selected').val('');
    $('#txt_Code_update').val('');
    $('#txt_PieceName_update').val('');
    $('#pieceUsageId_update').val('');
}