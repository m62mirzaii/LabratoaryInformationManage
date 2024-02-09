$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_TestDescription();
});

function LoadTable_TestDescription() {
    $('#tblTestDescription').DataTable({
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
            "url": "TestDescription/GetTestDescriptions",
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteTestDescription(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
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
    $("#insertTestDescriptionModal").modal("show");
}
function InsertTestDescription() {

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
        url: 'TestDescription/AddTestDescription',
        data: {
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestDescriptionModal").modal("hide");
            $('#tblTestDescription').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertTestDescriptionModal").modal("hide");
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
    $("#updateTestDescriptionModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblTestDescription tbody').on('click', 'tr', function () {

        var row = $('#tblTestDescription').DataTable().row(this).data();

        $('#txt_Name_update').val(row.name);
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
    });

    $("#updateTestDescriptionModal").modal("show");
    $('#testDescriptionId_Selected').val(id);
}

function UpdateTestDescription() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#testDescriptionId_Selected').val();
    var name = $('#txt_Name_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'TestDescription/UpdateTestDescription',
        data: {
            Id: id,
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestDescriptionModal").modal("hide");
            $('#tblTestDescription').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateTestDescriptionModal").modal("hide");
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
function deleteTestDescription(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/TestDescription/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Name').val('');
    $('#txt_Name_update').val('');
    $('#testDescriptionId_Selected').val('');
    $(".alertBox").hide();
}