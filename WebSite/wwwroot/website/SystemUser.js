$(document).ready(function () {

    LoadTable_Users();
    LoadTable_Systems();
    LoadTable_UserSystemForInsert();
    LoadTable_UserSystemForUpdate();

    Load_ComboBox();

    insert();
    update();
});

function LoadTable_Users() {
    $('#tblUsers').DataTable({
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
            "url": "SystemUser/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "fullNameUser" },
            {
                "render": function (data, type, row) { 
                    return ' <a href="#" id="btnShowUpdatePartial" title="ویرایش" data-id="' + row.id + '"> <i style="color:black;font-size:small" class="fa fa-edit"></i></a>' 
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a title="حذف " style="cursor: pointer" onclick="deleteByuserId(' + row.userId + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });


    $('#tblUsers tbody').on('click', 'tr', function () {
        var data = $("#tblUsers").DataTable().row(this).data();
        $("#userId_Selected").val(data.userId);
        $("#tblSystems").DataTable().ajax.reload();

        $("#tblUsers tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");
    });
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
            "url": "SystemUser/GetByUserId",
            "type": "POST",
            "datatype": "json",
            'data': function (reqData) {
                reqData.userId = $("#userId_Selected").val()
            },
        },
        fixedColumns: true,
        "columns": [

            { "data": "systemNameFa" },  
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnShowUpdatePartial" title="ویرایش" data-id="' + row.id + '"> <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a title="حذف " style="cursor: pointer" onclick="deleteById(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}

function LoadTable_UserSystemForInsert() {
    $('#tblSystemForInsert').DataTable({
        pageLength: 15,
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
            "url": "System/GetSystems",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [
            {
                "render": function (data, type, row) {
                    return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox " name="check" value="' + row.id + '">';
                }
            },
            { "data": "nameFa" },
        ]
    });
}

function LoadTable_UserSystemForUpdate() {
    $('#tblSystemForUpdate').DataTable({
        pageLength: 15,
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
            "url": "System/GetSystems",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [
            {
                "render": function (data, type, row) {
                    return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox " name="check" value="' + row.id + '">';
                }
            },
            { "data": "nameFa" },
        ]
    });
}


function Load_ComboBox() {

    $("#userLists").select2({ 
        allowClear: true,
        placeholder: '  ', 
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "User/GetActiveUsers",
            data: '{}',
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
        $("#tblSystems").DataTable().ajax.reload();
    }); 
}

function insert() {

    $("body").on('click', '#btnShowInsertPartial', function () {
        $("#insertModal").modal("hide");
        $.ajax({
            type: 'POST',
            url: 'SystemUser/Show_InsertPartial',
            data: {},
            dataType: 'html',
            success: function (response) {
                $("#insertModal").modal("show");

                setTimeout(function () {
                    $('#tblSystemForInsert').DataTable().columns.adjust().draw();
                }, 200);
                $('#tblSystemForInsert').DataTable().ajax.reload();

            }
        });

    });

    $("body").on('click', '#btnAddSystemUser', function () {

        const systemIds = [];
        var table = $("#tblSystemForInsert").DataTable();
        $("input:checkbox", table.rows().nodes()).each(function () {
            if ($(this).is(":checked")) {
                systemIds.push($(this).val());
            }
        });

        $.ajax({
            type: 'POST',
            url: 'SystemUser/AddSystemUser',
            data: {
                userId: $("#userId_Selected").val(),
                systemIds: systemIds,
            },
            dataType: 'html',
            success: function (response) {
                $("#insertModal").modal("hide");
                $('#tblUsers').DataTable().ajax.reload();
                $('#tblSystems').DataTable().ajax.reload();
            }
        }); 
    });

    $("body").on("click", "#btncloseModal", function (e) {
        $("#insertModal").modal("hide");
    });
}

function update() {

    $("body").on('click', '#btnShowUpdatePartial', function () {
        $("#updateModal").modal("hide");
        $.ajax({
            type: 'POST',
            url: 'SystemUser/Show_UpdatePartial',
            data: {},
            dataType: 'html',
            success: function (response) {
                $("#updateModal").modal("show");

                setTimeout(function () {
                    $('#tblSystemForUpdate').DataTable().columns.adjust().draw();
                }, 200);
                $('#tblSystemForUpdate').DataTable().ajax.reload();

            }
        });

    });

    $("body").on('click', '#btnUpdateSystemUser', function () {

        const systemIds = [];
        var table = $("#tblSystemForUpdate").DataTable();
        $("input:checkbox", table.rows().nodes()).each(function () {
            if ($(this).is(":checked")) {
                systemIds.push($(this).val());
            }
        });

        $.ajax({
            type: 'POST',
            url: 'SystemUser/UpdateSystemUser',
            data: {
                userId: $("#userId_Selected").val(),
                systemIds: systemIds,
            },
            dataType: 'html',
            success: function (response) {
                $("#updateModal").modal("hide");
                $('#tblUsers').DataTable().ajax.reload();
                $('#tblSystems').DataTable().ajax.reload();
            }
        });

    });

    $("body").on("click", "#btncloseUpdateModal", function (e) {
        $("#updateModal").modal("hide");
    });
}

//function deleteByuserId(userId) {
//    swal({
//        title: "تأیید حذف",
//        text: "آیا از حذف این مقدار اطمینان دارید؟",
//        icon: "warning",
//        buttons: ['لغو', 'بلی'],
//        dangerMode: true,
//    })
//        .then((willDelete) => {
//            if (willDelete) {

//                $.ajax({
//                    type: 'POST',
//                    url: 'SystemUser/DeleteByUserId',
//                    data: {
//                        userId: userId,
//                    },
//                    dataType: 'html',
//                    success: function (response) {
//                        $('#tblUsers').DataTable().ajax.reload();
//                        $('#tblSystems').DataTable().clear().draw();
//                    }
//                });
//            }
//        });
//}

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
                    url: 'SystemUser/DeleteById',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblUsers').DataTable().ajax.reload();
                        $('#tblSystems').DataTable().ajax.reload();
                    }
                });
            }
        });
}