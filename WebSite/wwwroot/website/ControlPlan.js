var controlPlanId_Selected;
var controlPlanProcessId_Selected;
var persianToday = "'" + $("#PersianToday").data("value") + "'";

$(document).ready(function () {
    $(".alertBox").hide();
    Load_ComboBox();

    LoadTable_ControlPlan();
    LoadTable_ControlPlanPiece();
    LoadTable_ControlPlanProcess();
    LoadTable_ControlPlanProcessTest();

    insert_eidt_ControlPlan();
    insert_ControlPlanPiece();
    insert_ControlPlanProcess();
    insert_ControlPlanProcessTest();

    LoadTable_PopUp();
});


//===============================================================================================
function Load_ComboBox() {
    $("#companyList").select2({
        dropdownParent: $("#insertControlPlan .modal-body"),
        allowClear: true,
        placeholder: 'نام کارخانه ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Company/GetCompanyForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.name,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#companyId").val(getID[0]['id']);
    });

    $("#companyList_update").select2({
        dropdownParent: $("#updateControlPlan .modal-body"),
        allowClear: true,
        placeholder: 'نام کارخانه ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Company/GetCompanyForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.name,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#companyId_update").val(getID[0]['id']);
    }); 
}

//===============================================================================================
function LoadTable_ControlPlan() {

    $('#tblControlPlan').DataTable({
        pageLength: 17,
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
            "url": "ControlPlan/GetControlPlans",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "companyName" },
            { "data": "planNumber" },
            { "data": "createDate" },
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnShow_EditControlPlan" title="ویرایش" data-id="' + row.id + '"> <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="delete_ControlPlan(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });

    $('#tblControlPlan tbody').on('click', 'tr', function () {
        var data = $("#tblControlPlan").DataTable().row(this).data();
        $("#controlPlanId_Selected").val(data.id);

        $("#tblControlPlanPiece").DataTable().ajax.reload();
        $("#tblControlPlanProcess").DataTable().ajax.reload();

        $("#tblControlPlan tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");


        $("#companyList_update").select2("trigger", "select", { data: { id: data.companyId, text: data.companyName } });
        $('#txt_PlanNumber_update').val(data.planNumber);
        $('#txt_CreateDate_update').val(data.createDate);
    });
}

function LoadTable_ControlPlanPiece() {
    $("#tblControlPlanPiece").DataTable({
        paging: false,
        info: false,
        scrollY: "300px",
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        stateSave: true,
        "lengthChange": false,
        "order": [],
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "ControlPlan/GetControlPlanPieces",
            'data': function (reqData) {
                reqData.ControlPlanId = $("#controlPlanId_Selected").val()
            },
            "type": "POST",
            "datatype": "json",
            "contentType": "application/x-www-form-urlencoded",
        },
        "columns": [

            { "data": "code", },
            { "data": "pieceName", },
            { "data": "usageName", },
            {
                "render": function (data, type, row) {
                    return '<a id="btnDeletePiece" title="حذف"  onmouseover="" style="cursor: pointer; color:red" onclick="delete_ControlPlanPiece(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>'
                },
                orderable: false,
            },
        ]
    });
}

function LoadTable_ControlPlanProcess() {

    $('#tblControlPlanProcess').DataTable({
        "bPaginate": false,
        scrollY: "300px",
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
            "url": "ControlPlan/GetControlPlanProcess",
            'data': function (reqData) {
                reqData.ControlPlanId = $("#controlPlanId_Selected").val()
            },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "processName" },
            { "data": "processTypeName" },
            {
                "render": function (data, type, row) {
                    return ' <a  id="btnDelete" title="حذف"  onmouseover="" style="cursor: pointer; color:red" onclick="delete_ControlPlanProcess(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                },
                orderable: false,
            },
        ]
    });

    $('#tblControlPlanProcess tbody').on('click', 'tr', function () {
 
        var data = $("#tblControlPlanProcess").DataTable().row(this).data();
        $("#controlPlanProcessId_Selected").val(data.id);

        $("#div_ControlPlanProcessTest").show();
        $('#tblControlPlanProcessTest').DataTable().ajax.reload();

        $("#tblControlPlanProcess tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");

    });
}

function LoadTable_ControlPlanProcessTest() {

    $('#tblControlPlanProcessTest').DataTable({
        "bPaginate": false,
        scrollY: "300px",
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
            "url": "ControlPlan/GetControlPlanProcessTest",
            'data': function (reqData) {
                reqData.ControlPlanProcessId = $("#controlPlanProcessId_Selected").val()
            },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "testName" },
            { "data": "testConditionName" },
            { "data": "amount" }, 
            { "data": "minimum" },
            { "data": "maximum" },
            { "data": "measureName" },
            { "data": "standardName" },
            { "data": "testDescriptionName" },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف"  onmouseover="" style="cursor: pointer"  onclick="delete_ControlPlanProcessTest(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                },
                orderable: false,
            },
        ]
    });
}

function LoadTable_PopUp() {

    $("#tblPiece").DataTable({
        paging: false,
        info: false,
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "Piece/GetPieces",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "render": function (data, type, row) {
                    return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox " name="check" value="' + row.id + '">';
                },
                orderable: false
            },
            { "data": "code" },
            { "data": "pieceName" },
            { "data": "pieceUsageName" },
        ]
    });

    $("#tblProcess").DataTable({
        paging: false,
        info: false,
        scrollCollapse: true,
        scroller: true,
        deferRender: true,
        "searching": true,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "Process/GetProcesses",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "render": function (data, type, row) { return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox" name="check" value="' + row.id + '">'; },
                orderable: false,
            },
            { "data": "processName" },
            { "data": "processTypeName" },
        ]
    });

    $("#tblTest").DataTable({
        paging: false,
        info: false,
        scrollY: "500px",
        scrollX: "100%",
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "lengthChange": false,
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "Test/GetTests",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "render": function (data, type, row) { return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox" name="check" value="' + row.id + '">'; },
                orderable: false,
            },
            { "data": "testName" },
            { "data": "testConditionName" },
            { "data": "testImportanceName" },
            { "data": "amount" },
            { "data": "minimum" },
            { "data": "maximum" },
            { "data": "standardName" },
            { "data": "measureName" },
            { "data": "testDescriptionName" },

        ],

    });

}
//===============================================================================================


function delete_ControlPlan(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/ControlPlan/DeleteControlPlan/' + id, '_parent');
                $('#tblControlPlan').DataTable().ajax.reload();

            }
        });
}

function delete_ControlPlanPiece(id) {
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
                    url: 'ControlPlan/DeleteControlPlanPiece',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblControlPlanPiece').DataTable().ajax.reload();
                    }
                });
            }
        });
}

function delete_ControlPlanProcess(id) {
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
                    url: 'ControlPlan/DeleteControlPlanProcess',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblControlPlanProcess').DataTable().ajax.reload();
                    }
                });
            }
        });
}

function delete_ControlPlanProcessTest(id) {
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
                    url: 'ControlPlan/DeleteControlplanProcessTest',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblControlPlanProcessTest').DataTable().ajax.reload();
                    }
                });
            }
        });
}

//===============================================================================================

function insert_eidt_ControlPlan() {

    $("body").on('click', '#btnShow_AddControlPlan', function () {
        $("#insertControlPlan").modal("hide");
        $.ajax({
            url: 'ControlPlan/Show_InsertPartial_ControlPlan',
            data: {},
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#dvPartial").html(response);
                $("#insertControlPlan").modal("show"); 
            }
        });
    });

    $("body").on('click', '#btnAddControlPlan', function () {

        if (validate_Insert_ControlPlan() == false) {
            return false
        }

        companyId = $("#companyId").val();
        planNumber = $("#txt_PlanNumber").val();
        createDate = $("#txt_CreateDate").val();



        $.ajax({
            type: 'POST',
            url: 'ControlPlan/AddControlPlan',
            data: {
                companyId: companyId,
                planNumber: planNumber,
                createDate: createDate
            },
            dataType: 'html',
            success: function (response) {
                $("#insertControlPlan").modal("hide");
                $('#tblControlPlan').DataTable().ajax.reload();
            }
        });
    });

    $("body").on('click', '#btnShow_EditControlPlan', function () {
        var obj = {};
        obj.Id = $(this).attr('data-id');

        $.ajax({
            url: 'ControlPlan/Show_UpdatePartial_ControlPlan',
            data: JSON.stringify(obj),
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#dvPartial").html(response);
                $("#updateControlPlan").modal("show");
            }
        });
    });

    $("body").on('click', '#btnUpdateControlPlan', function () {

        if (validate_Update_ControlPlan() == false) {
            return false
        }


        var id = $('#controlPlanId_Selected').val();
        companyId = $("#companyId_update").val();
        planNumber = $("#txt_PlanNumber_update").val();
        createDate = $("#txt_CreateDate_update").val();

        $.ajax({
            type: 'POST',
            url: 'ControlPlan/UpdateControlPlan',
            data: {
                id: id,
                companyId: companyId,
                planNumber: planNumber,
                createDate: createDate
            },
            dataType: 'html',
            success: function (response) {
                $("#updateControlPlan").modal("hide");
                $('#tblControlPlan').DataTable().ajax.reload();
            }
        });
    });

    $("body").on("click", "#closeControlPlanModal_insert", function (e) {
        $("#insertControlPlan").modal("hide");
    });

    $("body").on("click", "#closeControlPlanModal_update", function (e) {
        $("#updateControlPlan").modal("hide");
    });

    function validate_Insert_ControlPlan() {

        companyId = $("#companyId").val();
        if (companyId == "" || companyId == 0) {
            $(".alertBox").show();
            $(".alertBox span").addClass('alert-danger')
            $(".alertBox span").text("لطفا کمپانی را وارد نمایید");
            return false;
        }

        planNumber = $("#txt_PlanNumber").val();
        if (planNumber == "" || planNumber == 0) {
            $(".alertBox").show();
            $(".alertBox span").addClass('alert-danger')
            $(".alertBox span").text("لطفا شماره طرح کنترل را وارد نمایید");
            return false;
        }

        return true;
    }

    function validate_Update_ControlPlan() {

        companyId = $("#companyId_update").val();
        if (companyId == "" || companyId == 0) {
            $(".alertBox").show();
            $(".alertBox span").addClass('alert-danger')
            $(".alertBox span").text("لطفا کمپانی را وارد نمایید");
            return false;
        }

        planNumber = $("#txt_PlanNumber_update").val();
        if (planNumber == "" || planNumber == 0) {
            $(".alertBox").show();
            $(".alertBox span").addClass('alert-danger')
            $(".alertBox span").text("لطفا شماره طرح کنترل را وارد نمایید");
            return false;
        }

        return true;
    }
}

function insert_ControlPlanPiece() {

    $("body").on('click', '#btnNewPiece', function () {

        $("#insertPieceModal").modal("hide");
        $("#controlPlanId").val(controlPlanId);
        $.ajax({
            type: 'POST',
            url: 'ControlPlan/Show_InsertPartial_ControlPlanPiece',
            data: {},
            dataType: 'html',
            success: function (response) {
                $("#insertPieceModal").modal("show");

                setTimeout(function () {
                    $('#tblPiece').DataTable().columns.adjust().draw();
                }, 200);
                $('#tblPiece').DataTable().ajax.reload();
            }
        });
    });

    $("body").on('click', '#btnAddPeicessss', function () {
        const pieceids = [];
        var table = $("#tblPiece").DataTable();
        $("input:checkbox", table.rows().nodes()).each(function () {
            if ($(this).is(":checked")) {
                pieceids.push($(this).val());
            }
        });

        $.ajax({
            type: 'POST',
            url: 'ControlPlan/AddControlPlanPiece',
            data: {
                ControlPlanId: $("#controlPlanId_Selected").val(),
                Pieceids: pieceids
            },
            dataType: 'html',
            success: function (response) {
                $("#insertPieceModal").modal("hide");
                $('#tblControlPlanPiece').DataTable().ajax.reload();
            }
        });
    });

    $("body").on("click", "#closePieceModal", function (e) {
        $("#insertPieceModal").modal("hide");
    });
}

function insert_ControlPlanProcess() {

    $("body").on('click', '#btnNewProcess', function () {

        $("#insertProcessModal").modal("hide");
        $.ajax({
            type: 'POST',
            url: 'ControlPlan/Show_InsertPartial_ControlPlanProcess',
            data: {},
            dataType: 'html',
            success: function (response) {
                $("#insertProcessModal").modal("show");

                setTimeout(function () {
                    $('#tblProcess').DataTable().columns.adjust().draw();
                }, 200);

                $('#tblProcess').DataTable().ajax.reload();
            }
        });
    });

    $("body").on('click', '#AddControlPlanProcess', function () {
        const processIds = [];
        var table = $("#tblProcess").DataTable();
        $("#insertProcessModal_body").addClass("modal-body");
        $("input:checkbox", table.rows().nodes()).each(function () {
            if ($(this).is(":checked")) {
                processIds.push($(this).val());
            }
        });

        $.ajax({
            type: 'POST',
            url: 'ControlPlan/AddControlPlanProcess',
            data: {
                ControlPlanId: $("#controlPlanId_Selected").val(),
                ProcessIds: processIds
            },
            dataType: 'html',
            success: function (response) {
                $("#insertProcessModal").modal("hide");
                $('#tblControlPlanProcess').DataTable().ajax.reload();
            }
        });
    });

    $("body").on("click", "#closeProcessModal", function (e) {
        $("#insertProcessModal").modal("hide");
    });
}

function insert_ControlPlanProcessTest() {

    $("body").on('click', '#btnNewProcessTest', function () {
        $("#insertProcessTestModal").modal("hide");
        $.ajax({
            type: 'POST',
            url: 'ControlPlan/Show_InsertPartial_ControlPlanProcessTest',
            data: {},
            dataType: 'html',
            success: function (response) {
                $("#insertProcessTestModal").modal("show");

                setTimeout(function () {
                    $('#tblTest').DataTable().columns.adjust().draw();
                }, 200);
                $('#tblTest').DataTable().ajax.reload();
                RestFormObjects();
            }
        });
    });

    $("body").on('click', '#AddControlPlanProcessTest', function () {
        const testIds = [];
        var table = $("#tblTest").DataTable();
        $("input:checkbox", table.rows().nodes()).each(function () {
            if ($(this).is(":checked")) {
                testIds.push($(this).val());
            }
        });
 
        $.ajax({
            type: 'POST',
            url: 'ControlPlan/AddControlPlanProcessTest',
            data: {
                ControlPlanProcessId: $("#controlPlanProcessId_Selected").val(),
                TestIds: testIds,               
            },
            dataType: 'html',
            success: function (response) {

                //debugger
                //if (validateform_ControlPlanProcessTest() != false) {
                    $("#insertProcessTestModal").modal("hide");
                    $('#tblControlPlanProcessTest').DataTable().ajax.reload();
                /*}*/
            }
        });

    });

    $("body").on("click", "#closeProcessTestModal", function (e) {
        $("#insertProcessTestModal").modal("hide");
        RestFormObjects();
    });
}
//===============================================================================================
$(function () {

    $(document).on("click", "#AddControlPlan", function (e) {
        if (validateform_ControlPlan_Insert() == false) {
            e.preventDefault();
        }
    });

    $(document).on("click", "#UpdateControlPlan", function (e) {
        if (validateform_ControlPlan_Update() == false) {
            e.preventDefault();
        }
    });

    //$(document).on("click", "#AddControlPlanProcessTest", function (e) {
    //    if (validateform_ControlPlanProcessTest() == false) {
    //        e.preventDefault();
    //    }
    //});

});

function validateform_ControlPlan_Insert() {

    var companyId = $('#companyId').val();
    if (companyId == null || companyId == "" || companyId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام کارخانه را انتخاب نمایید");
        return false;
    }

    var planNumber = $('#txt_PlanNumber').val();
    if (planNumber == null || planNumber == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره طرح کنترل  را وارد نمایید");
        return false;
    }

    var endDate = $('#txt_CreateDate').val();
    if (endDate == null || endDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تاریخ را وارد نمایید");
        return false;
    }
}

function validateform_ControlPlan_Update() {

    var companyId = $('#companyId_update').val();
    if (companyId == null || companyId == "" || companyId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام کارخانه را انتخاب نمایید");
        return false;
    }

    var planNumber = $('#txt_PlanNumber_update').val();
    if (planNumber == null || planNumber == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره طرح کنترل  را وارد نمایید");
        return false;
    }

    var endDate = $('#txt_CreateDate_update').val();
    if (endDate == null || endDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تاریخ را وارد نمایید");
        return false;
    }
}

//function validateform_ControlPlanProcessTest() {

//    debugger
//    var txt_Min = $('#txt_Min').val();
//    var txt_Max = $('#txt_Max').val();
//    var testDescriptionId = $('#testDescriptionId').val();

//    if ((txt_Min == null || txt_Min == "") && (txt_Max == null || txt_Max == "") && (testDescriptionId == "0" || testDescriptionId == "" || testDescriptionId == null)) {
//        $(".alertBox span").addClass('alert-danger')
//        $(".alertBox span").text("لطفا حداقل یا حداکثر یا شرح را وارد نمایید");
//        return false;
//    }

//    var standardId = $('#standardId').val();
//    if (standardId == "0" || standardId == "" || standardId == null) {
//        $(".alertBox span").addClass('alert-danger')
//        $(".alertBox span").text("لطفا استاندارد را انتخاب نمایید");
//        return false;
//    }

//    var testImportanceId = $('#testImportanceId').val();
//    if (testImportanceId == "0" || testImportanceId == "" || testImportanceId == null) {
//        $(".alertBox span").addClass('alert-danger')
//        $(".alertBox span").text("لطفا اهمیت را انتخاب نمایید");
//        return false;
//    }

//    //var measureId = $('#measureId').val();
//    //if (measureId == "0" || measureId == "" || measureId == null) {
//    //    $(".alertBox span").addClass('alert-danger')
//    //    $(".alertBox span").text("لطفا واحد را انتخاب نمایید");
//    //    return false;
//    //}

//    return true;
//}

//===============================================================================================

function RestFormObjects() {

    $('#txt_Min').val('');
    $('#txt_Max').val('');

    $("#testDescription :selected").remove();
    $('#testDescriptionId').val('');

    $("#standard :selected").remove();
    $('#standardId').val('');

    $("#testImportance :selected").remove();
    $('#testImportanceId').val('');

    $("#measure :selected").remove();
    $('#measureId').val('');

}
