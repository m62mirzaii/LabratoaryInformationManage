var TestAcceptId;
var isConfirm_Titr;
var persianToday = "'" + $("#PersianToday").data("value") + "'";

$(document).ready(function () {

    LoadTable_TestAccept();
    LoadTable_TestAcceptDetail();

    LoadTable_TestAcceptDetail_Insert();

    Load_ComboBox();

});

function LoadTable_TestAccept() {

    $('#tblTestAccept').DataTable({
        pageLength: 5,
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
            "url": "TestAccept/GetTestAccepts",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "planNumber" },
            { "data": "requestNumber" },
            { "data": "receptionNumber" },
            { "data": "createDate" },
            { "data": "confirmCodeName" },
            {
                "render": function (data, type, row) {
                    if (row.confirmCode == 0 || row.confirmCode == 2) {
                        return ' <a class="btn btn-sm btn-primary" style="width:85px;font-size:7.5pt" onclick="SendToKartabl(' + row.id + ')"> ارسال به کارتابل</a>'
                    }
                    else {
                        return "";
                    }
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {

                    if (row.confirmCode == 0 || row.confirmCode == 2) {
                        return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_UpdateTestAccept(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                    } else { return "" }

                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    if (row.confirmCode == 0 || row.confirmCode == 2) {
                        return '<a  id="btnDeleteTestAccept" title="حذف"  onmouseover="" style="cursor: pointer"  onclick="delete_TestAccept(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                    } else { return "" }
                },
                orderable: false,
            },
        ],

    });

    $('#tblTestAccept tbody').on('click', 'tr', function () {
        var data = $("#tblTestAccept").DataTable().row(this).data();
        $("#TestAcceptId_Selected").val(data.id);
        isConfirm_Titr = data.isConfirm;
        $("#controlPlanId_Selected").val(data.controlPlanId);
        $("#testRequestId_Selected").val(data.testRequestId);

        if (data.confirmCode == 0 || data.confirmCode == 2) {
            $("#btnShowTestAcceptDetail").show();
        }
        else {
            $("#btnShowTestAcceptDetail").hide();
        }

        $("#tblTestAcceptDetail").DataTable().ajax.reload();

        $("#tblTestAccept tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");

        $("#controlPlan_forUpdate").select2("trigger", "select", { data: { id: data.controlPlanId, text: data.planNumber } });
        $("#testRequest_update").select2("trigger", "select", { data: { id: data.id, text: data.requestNumber } });

        $("#testRequestId_update").val(data.testRequestId)
        $("#txt_ReceptionNumber_Update").val(data.receptionNumber);
        $("#txt_CreateDate_forUpdate").val(data.createDate);
    });
}

function LoadTable_TestAcceptDetail() {

    $('#tblTestAcceptDetail').DataTable({
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
            "url": "TestAccept/GetTestAcceptDetails",
            'data': function (reqData) {
                reqData.TestAcceptId = $("#TestAcceptId_Selected").val()
            },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "processName" },
            { "data": "processTypeName" },
            { "data": "pieceName" },
            { "data": "testName" },
            { "data": "testConditionName" },
            { "data": "amount" },
            { "data": "testImportanceName" },
            { "data": "minimum" },
            { "data": "maximum" },
            { "data": "measureName" },
            { "data": "standardName" },
            { "data": "testDescriptionName" },
            { "data": "avarage" },
            { "data": "labratoaryToolName" },
            { "data": "fromDate" },
            { "data": "endDate" },
            { "data": "humidity" },
            { "data": "temperature" },
            {
                "render": function (data, type, row) {
                    if (row.isConfirm) {
                        return '  <i class="fa fa-check" style="color:green"></i>'
                    }
                    else {
                        return ""
                    }

                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {

                    if (row.confirmCode_TestAccept == 0 || row.confirmCode_TestAccept == 2) {
                        return '<a  id="btnDelete_detail" title="حذف"  onmouseover="" style="cursor: pointer"  onclick="delete_TestAcceptDetail(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></a>';
                    } else { return "" }
                },
                orderable: false,
            },
        ]
    });

    $('#tblTestAcceptDetail tbody').on('click', 'tr', function () {
        var data = $("#tblTestAcceptDetail").DataTable().row(this).data();
        $("#TestAcceptDetailId_Selected").val(data.id);

        $("#tblTestDetailRequest tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");
    });
}

function LoadTable_TestAcceptDetail_Insert() {

    $('#tblTestAcceptDetail_Insert').DataTable({
        "bPaginate": false,
        filter: true,
        deferRender: true,
        "searching": false,
        "bInfo": false,
        "lengthChange": false,
        "ajax": {
            "url": "TestAccept/GetTestAcceptDetailsForInsertPopup",
            'data': function (reqData) {
                reqData.controlPlanId = $("#controlPlanId_Selected").val(),
                    reqData.testRequestId = $("#testRequestId_Selected").val()
            },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "processName" },
            { "data": "processTypeName" },
            { "data": "pieceName" },
            { "data": "testName" },
            { "data": "testConditionName" },
            { "data": "amount" },
            { "data": "testImportanceName" },
            { "data": "labratoaryToolName" },
            { "data": "minimum" },
            { "data": "maximum" },
            { "data": "measureName" },
            { "data": "standardName" },
            { "data": "testDescriptionName" },
            {
                "render": function (data, type, row) {
     
                    if (row.isConflict == false) {
                        return '  <input id="txt_avarage_' + row.controlPlanProcessTestId + "_" + row.controlPlanPieceId + '" type="text" style="width:50px" class="form-control form-control-sm" />'
                    }
                    else {
                        return '';
                    }
                }
            },
            {
                "render": function (data, type, row) {
                    if (row.isConflict == false) {
                        return '<input class="form-control" style="width:85px;font-size: 11px;" id="txt_fromDate_' + row.controlPlanProcessTestId + "_" + row.controlPlanPieceId + '"  onclick="PersianDatePicker.Show(this,' + persianToday + ')" />'
                    } else {
                        return '';
                    }
                }
            },
            {
                "render": function (data, type, row) {
                    if (row.isConflict == false) {
                        return '<input class="form-control" style="width:85px;font-size: 11px;" id="txt_endDate_' + row.controlPlanProcessTestId + "_" + row.controlPlanPieceId + '"  onclick="PersianDatePicker.Show(this,' + persianToday + ')" />'
                    } else {
                        return '';
                    }
                }
            },
            {
                "render": function (data, type, row) {
                    if (row.isConflict == false) {
                        return '  <input id="txt_humidity_' + row.controlPlanProcessTestId + "_" + row.controlPlanPieceId + '" type="text" maxlength="2" onkeypress="return AllowOnlyNumbers(event);" style="width:50px;" class="form-control form-control-sm" />'
                    } else {
                        return '';
                    }
                }
            },
            {
                "render": function (data, type, row) {
                    if (row.isConflict == false) {
                        return '  <input id="txt_temperature_' + row.controlPlanProcessTestId + "_" + row.controlPlanPieceId + '" type="text" maxlength="2" onkeypress="return AllowOnlyNumbers(event);" style="width:50px;" class="form-control form-control-sm" />'
                    } else {
                        return '';
                    }
                }
            },
        ],
        order: [[0, 'desc']],
        rowCallback: function (row, data) {
            if (data.isConflict == true) {
                $(row).css("background-color", "#f08080");
            }
        }
    }); 
}

function Load_ComboBox() {

    $("#controlPlan").select2({
        dropdownParent: $("#insertTestAcceptModal"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "ControlPlan/GetControlPlanForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.planNumber,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#controlPlanId").val(getID[0]['id']);
    });

    $("#controlPlan_forUpdate").select2({
        dropdownParent: $("#updateTestAcceptModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "ControlPlan/GetControlPlanForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.planNumber,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#controlPlanId_forUpdate").val(getID[0]['id']);
    });

    $("#testRequest").select2({
        dropdownParent: $("#insertTestAcceptModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestRequest/GetTestRequestForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.requestNumber,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#testRequestId").val(getID[0]['id']);
    });

    $("#testRequest_update").select2({
        dropdownParent: $("#updateTestAcceptModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestRequest/GetTestRequestForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.requestNumber,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#testRequestId_update").val(getID[0]['id']);
    });



}

//=========================================================================

function ShowPopUp_InsertTestAccept() {
    $("#insertTestAcceptModal").modal("show");
    ResetFormObjects();
};
function InsertTestAccept() {
    var controlPlanId = $("#controlPlanId").val();
    var createDate = $("#txt_CreateDate").val();
    var testRequestId = $("#testRequestId").val();
    var receptionNumber = $("#txt_ReceptionNumber").val();

    if (validateform_Insert_TestAccept() == false) {
        $(".alertBox").show();
        return false;
    }


    $.ajax({
        type: 'POST',
        url: 'TestAccept/CheckReceptionNumber',
        data: {
            ReceptionNumber: receptionNumber,
        },
        dataType: 'html',
        success: function (response) {

            if (response == 'true') {
                $.ajax({
                    type: 'POST',
                    url: 'TestAccept/AddTestAccept',
                    data: {
                        controlPlanId: controlPlanId,
                        createDate: createDate,
                        testRequestId: testRequestId,
                        receptionNumber: receptionNumber
                    },
                    dataType: 'html',
                    success: function (response) {
                        $("#insertTestAcceptModal").modal("hide");
                        $('#tblTestAccept').DataTable().ajax.reload();
                        ResetFormObjects();
                    }
                });
            }
            else {
                $(".alertBox span").show();
                $(".alertBox span").addClass('alert-danger')
                $(".alertBox span").text("شماره پذیرش نمیتواند تکراری باشد");
            }
        }
    });



};
function validateform_Insert_TestAccept() {

    var controlPlanId = $('#controlPlanId').val();
    if (controlPlanId == "" || controlPlanId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره طرح کنترل را انتخاب نمایید");
        return false;
    }

    var testRequestId = $('#testRequestId').val();
    if (testRequestId == "" || testRequestId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره درخواست را انتخاب نمایید");
        return false;
    }

    var receptionNumber = $("#txt_ReceptionNumber").val();
    if (receptionNumber == "" || receptionNumber == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره پذیرش را وارد نمایید");
        return false;
    }

    var createDate = $('#txt_CreateDate').val();
    if (createDate == "" || createDate == null) {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تاریخ  را وارد نمایید");
        return false;
    }

}

function ShowPopUp_UpdateTestAccept() {
    $("#updateTestAcceptModal").modal("show");
};
function UpdateTestAccept() {
    var testAcceptId = $("#TestAcceptId_Selected").val();
    var controlPlanId = $("#controlPlanId_forUpdate").val();
    var createDate = $("#txt_CreateDate_forUpdate").val();
    var testRequestId = $("#testRequestId_update").val();
    var receptionNumber = $("#txt_ReceptionNumber_Update").val();

    if (validateform_Update_TestAccept() == false) {
        $(".alertBox").show();
        return false;
    }

    $.ajax({
        type: 'POST',
        url: 'TestAccept/UpdateTestAccept',
        data: {
            id: testAcceptId,
            controlPlanId: controlPlanId,
            createDate: createDate,
            testRequestId: testRequestId,
            receptionNumber: receptionNumber
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestAcceptModal").modal("hide");
            $('#tblTestAccept').DataTable().ajax.reload();
        }
    });
};
function validateform_Update_TestAccept() {

    var controlPlanId = $('#controlPlanId_forUpdate').val();
    if (controlPlanId == "" || controlPlanId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره طرح کنترل را انتخاب نمایید");
        return false;
    }

    var testRequestId = $('#testRequestId_update').val();
    if (testRequestId == "" || testRequestId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره درخواست را انتخاب نمایید");
        return false;
    }

    var receptionNumber = $("#txt_ReceptionNumber_Update").val();
    if (receptionNumber == "" || receptionNumber == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شماره پذیرش را وارد نمایید");
        return false;
    }

    var createDate = $('#txt_CreateDate_forUpdate').val();
    if (createDate == "" || createDate == null) {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تاریخ  را وارد نمایید");
        return false;
    }


}

function delete_TestAccept(id) {
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
                    url: 'TestAccept/DeleteTestAccept',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAccept').DataTable().ajax.reload();
                        $('#tblTestAcceptDetail').DataTable().ajax.reload();
                    }
                });
            }
        });
}

function CloseTestAccept() {
    $("#insertTestAcceptModal").modal("hide");
    $("#updateTestAcceptModal").modal("hide");
};


//=========================================================================
function ShowPopUp_InsertTestAcceptDetail() {
    $("#insertTestAcceptDetailModal").modal("show");

    setTimeout(function () {
        $("#tblTestAcceptDetail_Insert").DataTable().columns.adjust().draw();
    }, 200);
    $('#tblTestAcceptDetail_Insert').DataTable().ajax.reload();


};
function InsertTestAcceptDetail() {
    const TestAcceptDetails = [];
    $('#tblTestAcceptDetail_Insert tbody tr').each(function () {

        var data = $("#tblTestAcceptDetail_Insert").DataTable().row(this).data();
        var TestAcceptId = $("#TestAcceptId_Selected").val();
        var txt_avarage = $("#txt_avarage_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();
        var txt_toolCode = $("#txt_toolCode_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();
        var txt_fromDate = $("#txt_fromDate_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();
        var txt_endDate = $("#txt_endDate_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();
        var txt_humidity = $("#txt_humidity_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();
        var txt_temperature = $("#txt_temperature_" + data.controlPlanProcessTestId + "_" + data.controlPlanPieceId).val();

        if (txt_avarage != "" || txt_humidity != "" || txt_temperature != "") {
            TestAcceptDetails.push({
                TestAcceptId: TestAcceptId,
                controlPlanProcessTestId: data.controlPlanProcessTestId,
                controlPlanPieceId: data.controlPlanPieceId,
                avarage: txt_avarage,
                toolCode: txt_toolCode,
                fromDate: txt_fromDate,
                endDate: txt_endDate,
                humidity: txt_humidity,
                temperature: txt_temperature,
                testRequestDetailId: data.testRequestDetailId
            });
        }
    });
    $.ajax({
        type: 'POST',
        url: 'TestAccept/AddTestAcceptDetail',
        data: {
            TestAcceptDetails: TestAcceptDetails,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestAcceptDetailModal").modal("hide");
            $('#tblTestAcceptDetail').DataTable().ajax.reload();
        }
    });

};

function ShowPopUp_UpdateTestAcceptDetsil() {
    $("#updateTestAcceptDetailModal").modal("show");
}
function UpdateTestAcceptDetsil() {
    var TestAcceptDetailId = $("#TestAcceptDetailId_Selected").val();
    var txt_avarage = $("#txt_Avarage").val();
    var txt_toolCode = $("#txt_ToolCode").val();
    var txt_performDate = $("#txt_PerformDate").val();
    var txt_humidity = $("#txt_Humidity").val();
    var txt_temperature = $("#txt_Temperature").val();

    $.ajax({
        type: 'POST',
        url: 'TestAccept/UpdateTestAcceptDetail',
        data: {
            id: TestAcceptDetailId,
            avarage: txt_avarage,
            toolCode: txt_toolCode,
            performDate: txt_performDate,
            humidity: txt_humidity,
            temperature: txt_temperature
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestAcceptDetailModal").modal("hide");
            $('#tblTestAcceptDetail').DataTable().ajax.reload();
        }
    });
}

function delete_TestAcceptDetail(id) {
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
                    url: 'TestAccept/DeleteTestAcceptDetail',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAcceptDetail').DataTable().ajax.reload();
                    }
                });
            }
        });
}

function CloseTestAcceptDetail() {
    $("#insertTestAcceptDetailModal").modal("hide");
    $("#updateTestAcceptDetailModal").modal("hide");
};

function SendToKartabl(id) {
    swal({
        title: "تأیید ",
        text: "آیا از ارسال این مقدار اطمینان دارید؟",
        buttons: ['لغو', 'بلی'],
    })
        .then((willSend) => {
            if (willSend) {
                $.ajax({
                    type: 'POST',
                    url: 'TestAccept/SendToKartabl',
                    data: {
                        id: id,
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAccept').DataTable().ajax.reload();
                        $('#tblTestAcceptDetail').DataTable().ajax.reload();
                    }
                });
            }
        });
}


function ResetFormObjects() {

    $("#controlPlan :selected").remove();
    $('#controlPlanId').val('');

    $('#testRequest :selected').remove();
    $('#testRequestId').val('');

    $('#txt_ReceptionNumber').val('');
    $('#txt_CreateDate').val('');

    $(".alertBox").hide();
}
