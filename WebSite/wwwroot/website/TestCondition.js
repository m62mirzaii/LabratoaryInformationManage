$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_TestCondition();
});

function LoadTable_TestCondition() {
    $('#tblTestCondition').DataTable({
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
            "url": "TestCondition/GetTestConditions",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "name" },
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteTestCondition(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
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
    $("#insertTestConditionModal").modal("show");
}
function InsertTestCondition() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }

    var name = $('#txt_Name').val();
    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'TestCondition/AddTestCondition',
        data: {
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestConditionModal").modal("hide");
            $('#tblTestCondition').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertTestConditionModal").modal("hide");
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
    $("#updateTestConditionModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblTestCondition tbody').on('click', 'tr', function () {

        var row = $('#tblTestCondition').DataTable().row(this).data();

        $('#txt_Name_update').val(row.name);
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
    });

    $("#updateTestConditionModal").modal("show");
    $('#testConditionId_Selected').val(id);
}

function UpdateTestCondition() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#testConditionId_Selected').val();
    var name = $('#txt_Name_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'TestCondition/UpdateTestCondition',
        data: {
            Id: id,
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestConditionModal").modal("hide");
            $('#tblTestCondition').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateTestConditionModal").modal("hide");
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
function deleteTestCondition(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/TestCondition/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Name').val('');
    $('#txt_Name_update').val('');
    $('#testConditionId_Selected').val('');
    $(".alertBox").hide();
}