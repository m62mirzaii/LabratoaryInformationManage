$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_RequestUser();
});

function LoadTable_RequestUser() {
    $('#tblRequestUser').DataTable({
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
            "url": "RequestUser/GetRequestUsers",
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteRequestUser(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
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
    $("#insertRequestUserModal").modal("show");
}
function InsertRequestUser() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }
    var name = $('#txt_Name').val(); 

    $.ajax({
        type: 'POST',
        url: 'RequestUser/AddRequestUser',
        data: {
            Name: name, 
        },
        dataType: 'html',
        success: function (response) {
            $("#insertRequestUserModal").modal("hide");
            $('#tblRequestUser').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertRequestUserModal").modal("hide");
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
    $("#updateRequestUserModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblRequestUser tbody').on('click', 'tr', function () {

        var row = $('#tblRequestUser').DataTable().row(this).data();

        $('#txt_Name_update').val(row.name); 
    });

    $("#updateRequestUserModal").modal("show");
    $('#requestUserId_Selected').val(id);
}

function UpdateRequestUser() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#requestUserId_Selected').val();
    var name = $('#txt_Name_update').val(); 

    $.ajax({
        type: 'POST',
        url: 'RequestUser/UpdateRequestUser',
        data: {
            Id: id,
            Name: name, 
        },
        dataType: 'html',
        success: function (response) {
            $("#updateRequestUserModal").modal("hide");
            $('#tblRequestUser').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateRequestUserModal").modal("hide");
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
function deleteRequestUser(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/RequestUser/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Name').val('');
    $('#txt_Name_update').val('');
    $('#requestUserId_Selected').val('');
    $(".alertBox").hide();
}