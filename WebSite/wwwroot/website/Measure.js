$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_Measure();
});

function LoadTable_Measure() {
    $('#tblMeasure').DataTable({
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
            "url": "Measure/GetMeasures",
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteMeasure(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
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
    $("#insertMeasureModal").modal("show");
}
function InsertMeasure() {

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
        url: 'Measure/AddMeasure',
        data: {
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertMeasureModal").modal("hide");
            $('#tblMeasure').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {  
    RestFormObjects();
    $("#insertMeasureModal").modal("hide");
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
    $("#updateMeasureModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblMeasure tbody').on('click', 'tr', function () {

        var row = $('#tblMeasure').DataTable().row(this).data();

        $('#txt_Name_update').val(row.name);
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
    });

    $("#updateMeasureModal").modal("show");
    $('#measureId_Selected').val(id);
}

function UpdateMeasure() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#measureId_Selected').val();
    var name = $('#txt_Name_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'Measure/UpdateMeasure',
        data: {
            Id: id,
            Name: name,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateMeasureModal").modal("hide");
            $('#tblMeasure').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateMeasureModal").modal("hide");
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
function deleteMeasure(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/Measure/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_Name').val('');
    $('#txt_Name_update').val('');
    $('#measureId_Selected').val('');
    $(".alertBox").hide();
}