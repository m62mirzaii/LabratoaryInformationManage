var TestAcceptId;

var persianToday = "'" + $("#PersianToday").data("value") + "'";
$(document).ready(function () {

    LoadTable_TestAccept();
    LoadTable_TestAcceptDetail();

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
        "language": {
            "sSearch": " جستجو:  "
        },
        "ajax": {
            "url": "TestAccept_Kartabl/GetTestAcceptForKartabl",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "planNumber" },
            { "data": "requestNumber" },
            { "data": "receptionNumber" },
            { "data": "createDate" },
            {
                "render": function (data, type, row) {
                    if (row.confirmCode == 1) {
                        return '<a id="btnConfirm" class="btn btn-sm btn-success" title="تایید"   onclick="Confirm_TestAccept(' + row.id + ')">تایید </a>';
                    }
                    else {
                        return "";
                    } 
                },
            },
            {
                "render": function (data, type, row) {
                    if (row.confirmCode == 1) {
                        return '<a id="btnConfirm" class="btn btn-sm btn-danger" title="عودت"  onclick="reurn_TestAccept(' + row.id + ')">عودت </a>';
                    }
                    else {
                        return "";
                    }
                },
            },
            {
                "render": function (data, type, row) {
                    if (row.confirmCode == 3) {
                        return '<a id="btnConfirm" class="btn btn-sm btn-primary" title="اصلاح اطلاعات"  onclick="Update_TestAccept_ConfirmCode(' + row.id + ')">اصلاح اطلاعات </a>';
                    }
                    else {
                        return "";
                    }
                },
            },
        ]
    });

    $('#tblTestAccept tbody').on('click', 'tr', function () {

        var data = $("#tblTestAccept").DataTable().row(this).data();
        $("#TestAcceptId_Selected").val(data.id);
        $("#controlPlanId_Selected").val(data.controlPlanId);

        $("#tblTestAccept tr").removeClass("rowSelected");
        var selected = $(this).hasClass("rowSelected");
        if (!selected)
            $(this).addClass("rowSelected");


        $("#tblTestAcceptDetail").DataTable().ajax.reload();
    });
}

function LoadTable_TestAcceptDetail() {

    $('#tblTestAcceptDetail').DataTable({
        "bPaginate": false,
        filter: true,
        deferRender: true,
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
            { "data": "fromDate" },
            { "data": "endDate" },
            { "data": "humidity" },
            { "data": "temperature" },
            {
                "render": function (data, type, row) {

                    return '<input type="checkbox" style="width:18px;height: 18px;" class="select-checkbox " name="check" value="' + row.id + '">';
                }
            },
            {
                "render": function (data, type, row) {
                    return '<input type="textbox" style="width:250px" class="form-control"  id="txtAnswer_' + row.id + '"  >';
                },
                orderable: false
            },
            {
                "render": function (data, type, row) {
                    return '<input type="textbox" style="width:250px" class="form-control"   id="txtResult_' + row.id + '"  >';
                },
                orderable: false
            },
        ]
    });
}

function Confirm_TestAccept(id) {

    const details = [];
    var table = $("#tblTestAcceptDetail").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {




        var detailId = $(this).val();


        var answer = $("#txtAnswer_" + detailId).val();
        var result = $("#txtResult_" + detailId).val();
        var isConfirm = false;

        debugger

        if ($(this).is(":checked"))
            isConfirm = true;

        details.push({
            id: detailId,
            isConfirm: isConfirm,
            answerText: answer,
            testResult: result
        });
        /*      }*/
    });

    swal({
        title: "تأیید",
        text: "آیا از تایید این مقدار اطمینان دارید؟",
        icon: "success",
        buttons: ['لغو', 'بلی'],
    })
        .then((confirm) => {
            if (confirm) {

                $.ajax({
                    type: 'POST',
                    url: 'TestAccept_Kartabl/ConfirmTestAccept',
                    data: {
                        id: id,
                        TestAcceptDetails: details
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAccept').DataTable().ajax.reload();
                        $('#tblTestAcceptDetail').DataTable().clear().draw();
                    }
                });
            }
        });
}


function Update_TestAccept_ConfirmCode(id) {

    const detailIds = [];
    var table = $("#tblTestAcceptDetail").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {
        if ($(this).is(":checked")) {
            detailIds.push($(this).val());
        }
    });

    swal({
        title: "اصلاح وضعیت",
        text: "آیا از ویرایش این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((confirm) => {
            if (confirm) {

                $.ajax({
                    type: 'POST',
                    url: 'TestAccept_Kartabl/Update_TestAccept_ConfirmCode',
                    data: {
                        id: id
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAccept').DataTable().ajax.reload();
                        $('#tblTestAcceptDetail').DataTable().clear().draw();
                    }
                });
            }
        });
}


function reurn_TestAccept(id) {

    const detailIds = [];
    var table = $("#tblTestAcceptDetail").DataTable();
    $("input:checkbox", table.rows().nodes()).each(function () {
        if ($(this).is(":checked")) {
            detailIds.push($(this).val());
        }
    });

    swal({
        title: "عودت",
        text: "آیا از عودت این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((confirm) => {
            if (confirm) {

                $.ajax({
                    type: 'POST',
                    url: 'TestAccept_Kartabl/Return_TestAccept',
                    data: {
                        id: id
                    },
                    dataType: 'html',
                    success: function (response) {
                        $('#tblTestAccept').DataTable().ajax.reload();
                        $('#tblTestAcceptDetail').DataTable().clear().draw();
                    }
                });
            }
        });
}