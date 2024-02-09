$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_RequestUnit();
});

function LoadTable_RequestUnit() {
    $('#tblRequestUnit').DataTable({
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
            "url": "RequestUnit/GetRequestUnits",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "name" }, 
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_Update(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteRequestUnit(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
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
    $("#insertRequestUnitModal").modal("show");
}
function InsertRequestUnit() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }

    var name = $('#txt_Name').val(); 

    $.ajax({
        type: 'POST',
        url: 'RequestUnit/AddRequestUnit',
        data: {
            Name: name, 
        },
        dataType: 'html',
        success: function (response) {
            $("#insertRequestUnitModal").modal("hide");
            $('#tblRequestUnit').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertRequestUnitModal").modal("hide");
}
function validate_Insert() {

    var name = $('#txt_Name').val();
    if (name == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updateRequestUnitModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblRequestUnit tbody').on('click', 'tr', function () {

        var row = $('#tblRequestUnit').DataTable().row(this).data();

        $('#txt_Name_update').val(row.name); 
    });

    $("#updateRequestUnitModal").modal("show");
    $('#requestUnitId_Selected').val(id);
}

function UpdateRequestUnit() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#requestUnitId_Selected').val();
    var name = $('#txt_Name_update').val();
    
    $.ajax({
        type: 'POST',
        url: 'RequestUnit/UpdateRequestUnit',
        data: {
            Id: id,
            Name: name, 
        },
        dataType: 'html',
        success: function (response) {
            $("#updateRequestUnitModal").modal("hide");
            $('#tblRequestUnit').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateRequestUnitModal").modal("hide");
}
function validate_Update() {

    var name = $('#txt_Name_update').val();
    if (name == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام را وارد نمایید");
        return false;
    }
    return true;
}


//================================ DELETE ====================================================
function deleteRequestUnit(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/RequestUnit/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Name').val('');
    $('#txt_Name_update').val('');
    $('#requestUnitId_Selected').val('');
    $(".alertBox").hide();
}