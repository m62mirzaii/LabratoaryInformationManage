var testRequestId;
var persianToday = "'" + $("#PersianToday").data("value") + "'";
$(document).ready(function () {

    LoadTable_TestRequest();
    LoadTable_TestRequestDetail();
    LoadTable_TestRequestDetail_Insert();

    Load_ComboBox();
});

function LoadTable_TestRequest() {

    $('#tblTestRequest').DataTable({
        pageLength: 6,
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
            "url": "TestRequest/GetTestRequests",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "requestNumber" },
            { "data": "requestUnitName" },
            { "data": "requestUserName" },
            { "data": "pieceName" },
            { "data": "companyName" },
            { "data": "requestDate" },
            //{
            //    "render": function (data, type, row) {
            //        return ' <a href="#" id="btnShowUpdateTestRequest" title="ویرایش" data-id="' + row.id + '"> <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
            //    },
            //    orderable: false,
            //},
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_Update(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDeleteTestRequest" title="حذف"  onmouseover="" style="cursor: pointer"  onclick="delete_TestRequest(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                },
                orderable: false,
            },
        ]
    });

    $('#tblTestRequest tbody').on('click', 'tr', function () {
        var data = $("#tblTestRequest").DataTable().row(this).data();
        $("#testRequestId_Selected").val(data.id);
        $("#requestUnit_update").select2("trigger", "select", { data: { id: data.requestUnitId, text: data.requestUnitName } });
        $("#requestUser_update").select2("trigger", "select", { data: { id: data.requestUserId, text: data.requestUserName } });
        $("#piece_update").select2("trigger", "select", { data: { id: data.pieceId, text: data.pieceName } });
        $("#company_update").select2("trigger", "select", { data: { id: data.companyId, text: data.companyName } });

        $("#txt_RequestNumber_update").val(data.requestNumber);
        $("#txt_RequestDate_update").val(data.requestDate);
        $("#txt_PieceCreatorName_update").val(data.pieceCreatorName);

        $("#tblTestRequestDetail").DataTable().ajax.reload();

        $("#tblTestRequest tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");
    });
}

function LoadTable_TestRequestDetail() {

    $('#tblTestRequestDetail').DataTable({
        pageLength: 10,
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
            "url": "TestRequest/GetTestRequestDetails",
            'data': function (reqData) {
                reqData.testRequestId = $("#testRequestId_Selected").val()
            },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "testName" },
            { "data": "testConditionName" },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete_detail" title="حذف"  onmouseover="" style="cursor: pointer"  onclick="delete_TestRequestDetail(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                },
                orderable: false,
            },
        ]
    });

    $('#tblTestRequestDetail tbody').on('click', 'tr', function () {
    
        var data = $("#tblTestRequestDetail").DataTable().row(this).data();
        $("#testRequestDetailId_Selected").val(data.id);

        $("#tblTestDetailRequest tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");
    });
}

function LoadTable_TestRequestDetail_Insert() {
    $("#tblTestRequestDetail_Insert").DataTable({
        paging: false,
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
            { "data": "labratoaryToolName" },
            { "data": "amount" },
            { "data": "minimum" },
            { "data": "maximum" },
            { "data": "standardName" },
            { "data": "measureName" },
            { "data": "testDescriptionName" },
            { "data": "fromDate" },
            { "data": "endDate" },
        ],

    });
}

function Load_ComboBox() {

    $("#requestUnit").select2({
        dropdownParent: $("#insertTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "RequestUnit/GetRequestUnitForSelect2",
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
        $("#requestUnitId").val(getID[0]['id']);
    });

    $("#requestUnit_update").select2({
        dropdownParent: $("#updateTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "RequestUnit/GetRequestUnitForSelect2",
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
        $("#requestUnitId_update").val(getID[0]['id']);
    });


    $("#requestUser").select2({
        dropdownParent: $("#insertTestRequestModal .modal-body"),
        allowClear: true,
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "RequestUser/GetRequestUserForSelect2",
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
        $("#requestUserId").val(getID[0]['id']);
    });

    $("#requestUser_update").select2({
        dropdownParent: $("#updateTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "RequestUser/GetRequestUserForSelect2",
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
        $("#requestUserId_update").val(getID[0]['id']);
    });


    $("#piece").select2({
        dropdownParent: $("#insertTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Piece/GetPieceForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.pieceName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#pieceId").val(getID[0]['id']);
    });

    $("#piece_update").select2({
        dropdownParent: $("#updateTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Piece/GetPieceForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.pieceName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#pieceId_update").val(getID[0]['id']);
    });


    $("#company").select2({
        dropdownParent: $("#insertTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
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

    $("#company_update").select2({
        dropdownParent: $("#updateTestRequestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
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

function ShowPopUp_Insert() {
    $("#insertTestRequestModal").modal("show");
    RestFormObjects();
}
function insert_TestRequest() {
    var requestNumber = $("#txt_RequestNumber").val();
    var requestDate = $("#txt_RequestDate").val();
    var pieceId = $("#pieceId").val();
    var requestUnitId = $("#requestUnitId").val();
    var requestUserId = $("#requestUserId").val();
    var companyId = $("#companyId").val();


    if (validateform_Insert_TestRequest() == false) {
        return false;
    }

    $.ajax({
        type: 'POST',
        url: 'TestRequest/AddTestRequest',
        data: {
            requestNumber: requestNumber,
            requestDate: requestDate,
            pieceId: pieceId,
            requestUnitId: requestUnitId,
            requestUserId: requestUserId,
            companyId: companyId
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestRequestModal").modal("hide");
            $('#tblTestRequest').DataTable().ajax.reload();
        }
    });
}

function ClosePopUp_Insert() {
    $("#insertTestRequestModal").modal("hide");
    RestFormObjects();
}

function validateform_Insert_TestRequest() {

    //var controlPlanId = $('#controlPlanId').val();
    //if (controlPlanId == "" || controlPlanId == "0") {
    //    $(".alertBox span").addClass('alert-danger')
    //    $(".alertBox span").text("لطفا شماره طرح کنترل را انتخاب نمایید");
    //    return false;
    //}

    //var createDate = $('#txt_CreateDate').val();
    //if (createDate == "" || createDate == null) {
    //    $(".alertBox span").addClass('alert-danger')
    //    $(".alertBox span").text("لطفا تاریخ  را وارد نمایید");
    //    return false;
    //}
}
function ShowPopUp_Update(id) {

    $('#tblTestRequest tbody').on('click', 'tr', function () {

        var row = $('#tblTestRequest').DataTable().row(this).data();

        $('#txt_RequestNumber_update').val(row.requestNumber);
        $('#txt_RequestDate_update').val(row.requestDate);

        $("#requestUnit_update").select2("trigger", "select", { data: { id: row.requestUserId, text: row.requestUserName } });
        $("#requestUser_update").select2("trigger", "select", { data: { id: row.requestUserId, text: row.requestUserName } });
        $("#piece_update").select2("trigger", "select", { data: { id: row.pieceId, text: row.pieceName } });
        $("#company_update").select2("trigger", "select", { data: { id: row.companyId, text: row.companyName } });

    });

    $("#updateTestRequestModal").modal("show");
    $('#testRequestId_Selected').val(id);
}

function update_TestRequest() {

    var testRequestId = $("#testRequestId_Selected").val();
    var requestNumber = $("#txt_RequestNumber_update").val();
    var requestDate = $("#txt_RequestDate_update").val();
    var pieceId = $("#pieceId_update").val();
    var requestUnitId = $("#requestUnitId_update").val();
    var requestUserId = $("#requestUserId_update").val();
    var companyId = $("#companyId_update").val();

    if (validateform_Update_TestRequest() == false) {
        return false;
    }

    $.ajax({
        type: 'POST',
        url: 'TestRequest/UpdateTestRequest',
        data: {
            id: testRequestId,
            requestNumber: requestNumber,
            requestDate: requestDate,
            pieceId: pieceId,
            requestUnitId: requestUnitId,
            requestUserId: requestUserId,
            companyId: companyId
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestRequestModal").modal("hide");
            $('#tblTestRequest').DataTable().ajax.reload();
        }
    });
}

function ClosePopUp_Update() {
    $("#updateTestRequestModal").modal("hide");
}

function validateform_Update_TestRequest() {

    //var controlPlanId = $('#controlPlanId_forUpdate').val();
    //if (controlPlanId == "" || controlPlanId == "0") {
    //    $(".alertBox span").addClass('alert-danger')
    //    $(".alertBox span").text("لطفا شماره طرح کنترل را انتخاب نمایید");
    //    return false;
    //}

    //var createDate = $('#txt_CreateDate_forUpdate').val();
    //if (createDate == "" || createDate == null) {
    //    $(".alertBox span").addClass('alert-danger')
    //    $(".alertBox span").text("لطفا تاریخ  را وارد نمایید");
    //    return false;
    //}
}
function ShowPopUp_InsertDetail() {
   /* $("#tblTestRequestDetail_Insert").DataTable().ajax.reload();*/
    $("#insertTestRequestDetailModal").modal("show");



    setTimeout(function () {
        $('#tblTestRequestDetail_Insert').DataTable().columns.adjust().draw();
    }, 200);
    $('#tblTestRequestDetail_Insert').DataTable().ajax.reload();
}

function Insert_TestRequestDetail() {
    const testIds = [];
    var table = $("#tblTestRequestDetail_Insert").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {
        if ($(this).is(":checked")) {
            testIds.push($(this).val());
        }
    });

    $.ajax({
        type: 'POST',
        url: 'TestRequest/AddTestRequestDetail',
        data: {
            testRequestId: $("#testRequestId_Selected").val(),
            testIds: testIds,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestRequestDetailModal").modal("hide");
            $('#tblTestRequest').DataTable().ajax.reload();
            $('#tblTestRequestDetail').DataTable().ajax.reload();
        }
    });
}

function closePopup_InsertDetail() {
    $("#insertTestRequestDetailModal").modal("hide");
}
function delete_TestRequest(id) {
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
                    url: 'TestRequest/DeleteTestRequest',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestRequest').DataTable().ajax.reload();
                        $('#tblTestRequestDetail').DataTable().ajax.reload();
                    }
                });
            }
        });
}

function delete_TestRequestDetail(id) {
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
                    url: 'TestRequest/DeleteTestRequestDetail',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestRequestDetail').DataTable().ajax.reload();
                    }
                });
            }
        });
} 

function AllowOnlyNumbers(e) {

    e = (e) ? e : window.event;
    var clipboardData = e.clipboardData ? e.clipboardData : window.clipboardData;
    var key = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
    var str = (e.type && e.type == "paste") ? clipboardData.getData('Text') : String.fromCharCode(key);

    return (/^\d+$/.test(str));
}
  
function RestFormObjects() {

    $('#txt_RequestNumber').val('');  

    $("#requestUnit :selected").remove();
    $('#requestUnitId').val('');

    $('#requestUser :selected').remove();
    $('#requestUserId').val('');

    $("#piece :selected").remove();
    $('#pieceId').val('');    

    $("#company :selected").remove();
    $('#companyId').val(''); 

    $('#txt_RequestDate').val('');
    $(".alertBox").hide();
}