
$(document).ready(function () {

    Load_ComboBox();
    LoadTable_Systems();
    LoadTable_UserSystemForInsert();

});

function Load_ComboBox() {

    $("#drpUser").select2({
        allowClear: true,
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "User/GetUserForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.fullName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#userId_Selected").val(getID[0]['id']);
    });
}

function ShowUserAccess() {

    var userId = $("#userId_Selected").val();
    if (userId == "") {
        alert('لطفا کاربر را انتخاب نمایید');
        return false;
    }

    $('#tblSystems').DataTable().ajax.reload();

}

function LoadTable_Systems() {

    $('#tblSystems').DataTable({
        pageLength: 18,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "bInfo": false,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "UserAccess/GetByUserId",
            "type": "POST",
            "datatype": "json",
            'data': function (param) {
                param.userId = $("#userId_Selected").val()
            },
        },
        fixedColumns: true,
        "columns": [
            { "data": "nameFa" },
            {
                "render": function (data, type, row) {
                    return '<a title="حذف"  style="cursor: pointer;" onclick="deleteById(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            }
        ]
    });
}

function deleteById(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax({
                    type: 'POST',
                    url: 'UserAccess/DeleteById',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblSystems').DataTable().ajax.reload();
                    }
                });
            }
        });
}


//============ Insert ======================

function ShowPopUp_Insert() {

    RestFormObjects();


    var userId = $("#userId_Selected").val();
    if (userId == "") {
        alert('لطفا کاربر را انتخاب نمایید');
        return false;
    }

    setTimeout(function () {
        $('#tblSystemForInsert').DataTable().columns.adjust().draw();
    }, 200);


    $("#insertModal").modal("show");
}
function LoadTable_UserSystemForInsert() {
    $('#tblSystemForInsert').DataTable({
        paging: false,
        info: false,
        scrollY: "500px",
        scrollX: "400px",
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "bInfo": false,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "UserAccess/GetSystems_ForInsert",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [
            {
                "render": function (data, type, row) {
                    return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox " name="check"  value="' + row.systemId + '"> ';
                }
            },
            { "data": "nameFa" }, 
        ]
    });
}

function InsertSystemUser() {

    const systemIds = [];
    var table = $("#tblSystemForInsert").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {
        if ($(this).is(":checked")) {  
            systemIds.push($(this).val()); 
        }
    });

    if (systemIds.length == 0) {
        alert('لطفا سیستم های مورد نظر را انتخاب نمایید');
        return false;
    }

    $.ajax({
        type: 'POST',
        url: 'UserAccess/AddUserAccess',
        data: {
            userId: $("#userId_Selected").val(),
            systemIds: systemIds 
        },
        dataType: 'html',
        success: function (response) {
            $("#insertModal").modal("hide");
            $('#tblSystems').DataTable().ajax.reload();
        }
    });


}

function ClosePopUp() {
    RestFormObjects();
    $("#insertModal").modal("hide");
} 

function RestFormObjects() {

    var table = $("#tblSystemForInsert").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {
        if ($(this).is(":checked")) {
            $(this).prop('checked', false);
        }
    });

}   