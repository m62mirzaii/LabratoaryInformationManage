var persianToday = "'" + $("#PersianToday").data("value") + "'";
$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_Test();
    Load_ComboBox();
});

function LoadTable_Test() {
    $('#tblTest').DataTable({
        pageLength: 25,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "order": [],
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
        fixedColumns: true,
        "columns": [

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
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_Update(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteTest(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}
function Load_ComboBox() {

    $("#testCondition").select2({
        dropdownParent: $("#insertTestModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestCondition/GetTestConditionForSelect2",
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
        $("#testConditionId").val(getID[0]['id']);
    });

    $("#testCondition_update").select2({
        dropdownParent: $("#updateTestModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestCondition/GetTestConditionForSelect2",
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
        $("#testConditionId_update").val(getID[0]['id']);
    });

    $("#testImportance").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestImportance/GetTestImportancForSelect2",
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
        $("#testImportanceId").val(getID[0]['id']);
    });

    $("#testImportance_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestImportance/GetTestImportancForSelect2",
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
        $("#testImportanceId_update").val(getID[0]['id']);
    });

    $("#labratoaryTool").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "LabratoaryTool/GetLabratoaryToolForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.toolName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#labratoaryToolId").val(getID[0]['id']);
    });

    $("#labratoaryTool_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "LabratoaryTool/GetLabratoaryToolForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.toolName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#labratoaryToolId_update").val(getID[0]['id']);
    });

    $("#testImportance").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestImportance/GetTestImportancForSelect2",
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
        $("#testImportanceId").val(getID[0]['id']);
    });

    $("#testImportance_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestImportance/GetTestImportancForSelect2",
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
        $("#testImportanceId_update").val(getID[0]['id']);
    });
    $("#measure").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Measure/GetMeasureForSelect2",
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
        $("#measureId").val(getID[0]['id']);
    });

    $("#measure_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Measure/GetMeasureForSelect2",
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
        $("#measureId_update").val(getID[0]['id']);
    });

    $("#standard").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Standard/GetStandardForSelect2",
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
        $("#standardId").val(getID[0]['id']);
    });

    $("#standard_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Standard/GetStandardForSelect2",
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
        $("#standardId_update").val(getID[0]['id']);
    });

    $("#testDescription").select2({
        dropdownParent: $("#insertTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestDescription/GetTestDescriptionForSelect2",
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
        $("#testDescriptionId").val(getID[0]['id']);
    });

    $("#testDescription_update").select2({
        dropdownParent: $("#updateTestModal .modal-body"),
        allowClear: true,
        placeholder: '  ',
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "TestDescription/GetTestDescriptionForSelect2",
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
        $("#testDescriptionId_update").val(getID[0]['id']);
    });
}


//================================ Insert ====================================================
function ShowPopUp_Insert() {
    $(".alertBox").hide();
    $("#insertTestModal").modal("show");
}
function InsertTest() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }

    var testName = $('#txt_TestName').val();
    var testConditionId = $('#testConditionId').val();
    var testImportanceId = $('#testImportanceId').val();
    var labratoaryToolId = $('#labratoaryToolId').val();
    var amount = $('#txt_Amount').val();
    var fromDate = $('#txt_FromDate').val();
    var endDate = $('#txt_EndDate').val();
     
    measureId = $("#measureId").val();
    standardId = $("#standardId").val();
    testDescriptionId = $("#testDescriptionId").val();

    min = $("#txt_Min").val();
    max = $("#txt_Max").val(); 

    $.ajax({
        type: 'POST',
        url: 'Test/AddTest',
        data: {
            TestName: testName,
            TestConditionId: testConditionId,
            TestImportanceId: testImportanceId,
            LabratoaryToolId: labratoaryToolId,
            Amount: amount,
            Minimum: min,
            Maximum: max, 
            MeasureId: measureId,
            StandardId: standardId,
            TestDescriptionId: testDescriptionId,
            FromDate: fromDate,
            EndDate: endDate,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertTestModal").modal("hide");
            $('#tblTest').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertTestModal").modal("hide");
}
function validate_Insert() {


    var testName = $('#txt_TestName').val();
    if (testName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام تست را وارد نمایید");
        return false;
    }

    var testConditionId = $('#testConditionId').val();
    if (testConditionId == null || testConditionId == "" || testConditionId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد  را وارد نمایید");
        return false;
    }

    var labratoaryToolId = $('#labratoaryToolId').val();
    if (labratoaryToolId == "" || labratoaryToolId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کد ابزار  را وارد نمایید");
        return false;
    }

    var testImportanceId = $('#testImportanceId').val();
    if (testImportanceId == "" || testImportanceId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا اهمیت  را وارد نمایید");
        return false;
    }  

    var amount = $('#txt_Amount').val();
    if (amount == "" || amount == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا هزینه را وارد نمایید");
        return false;
    }

    var standardId = $('#standardId').val();
    if (standardId == "" || standardId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا استاندارد را وارد نمایید");
        return false;
    }

    var txt_Min = $('#txt_Min').val();
    if (txt_Min == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا حداقل را وارد نمایید");
        return false;
    }

    var txt_Max = $('#txt_Max').val();
    if (txt_Max == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا حداکثر را وارد نمایید");
        return false;
    }


    var measureId = $('#measureId').val();
    if (measureId == "" || measureId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا واحد را وارد نمایید");
        return false;
    }


    var testDescriptionId = $('#testDescriptionId').val();
    if (testDescriptionId == "" || testDescriptionId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا شرح را وارد نمایید");
        return false;
    }



    var fromDate = $('#txt_FromDate').val();
    if (fromDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا از تاریخ را وارد نمایید");
        return false;
    }

    var endDate = $('#txt_EndDate').val();
    if (endDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تا تاریخ را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $(".alertBox").hide();
    $("#updateTestModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblTest tbody').on('click', 'tr', function () {

        var row = $('#tblTest').DataTable().row(this).data();

        $('#txt_TestName_update').val(row.testName);
         
        $("#txt_Min_update").val(row.minimum);
        $("#txt_Max_update").val(row.maximum);
        $('#txt_Amount_update').val(row.amount);
        $('#txt_FromDate_update').val(row.fromDate);
        $('#txt_EndDate_update').val(row.endDate);
         
        $("#testCondition_update").select2("trigger", "select", { data: { id: row.testConditionId, text: row.testConditionName } });
        $("#testImportance_update").select2("trigger", "select", { data: { id: row.testImportanceId, text: row.testImportanceName } });
        $("#labratoaryTool_update").select2("trigger", "select", { data: { id: row.labratoaryToolId, text: row.labratoaryToolName } });

        $("#standard_update").select2("trigger", "select", { data: { id: row.standardId, text: row.standardName } });
        $("#measure_update").select2("trigger", "select", { data: { id: row.measureId, text: row.measureName } });
        $("#testDescription_update").select2("trigger", "select", { data: { id: row.testDescriptionId, text: row.testDescriptionName } });
    });

    $("#updateTestModal").modal("show");
    $('#testId_Selected').val(id);

}

function UpdateTest() {

    if (!validate_Update()) {
        return false;
    }

    var id = $('#testId_Selected').val();
    var testName = $('#txt_TestName_update').val();
    var testConditionId = $('#testConditionId_update').val();
    var testImportanceId = $('#testImportanceId_update').val();
    var labratoaryToolId = $('#labratoaryToolId_update').val(); 

    measureId = $("#measureId_update").val();
    standardId = $("#standardId_update").val();
    testDescriptionId = $("#testDescriptionId_update").val();

    var amount = $('#txt_Amount_update').val();
    var fromDate = $('#txt_FromDate_update').val();
    var endDate = $('#txt_EndDate_update').val();
     
    min = $("#txt_Min_update").val();
    max = $("#txt_Max_update").val();

    $.ajax({
        type: 'POST',
        url: 'Test/UpdateTest',
        data: {
            Id: id,
            TestName: testName,
            TestConditionId: testConditionId,
            TestImportanceId: testImportanceId,
            LabratoaryToolId: labratoaryToolId,
            Minimum: min,
            Maximum: max, 
            MeasureId: measureId,
            StandardId: standardId,
            TestDescriptionId: testDescriptionId,
            Amount: amount,
            FromDate: fromDate,
            EndDate: endDate,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateTestModal").modal("hide");
            $('#tblTest').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateTestModal").modal("hide");
}
function validate_Update() {

    var testName = $('#txt_TestName_update').val();
    if (testName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام تست را وارد نمایید");
        return false;
    }

    var testConditionId = $('#testConditionId_update').val();
    if (testConditionId == null || testConditionId == "" || testConditionId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد   را وارد نمایید");
        return false;
    }


    var amount = $('#txt_Amount_update').val();
    if (amount == "" || amount == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا هزینه را وارد نمایید");
        return false;
    }

    var fromDate = $('#txt_FromDate_update').val();
    if (fromDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا از تاریخ را وارد نمایید");
        return false;
    }

    var endDate = $('#txt_EndDate_update').val();
    if (endDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تا تاریخ را وارد نمایید");
        return false;
    }

    return true;
}


//================================ DELETE ====================================================
function deleteTest(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/Test/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_TestName').val('');
    $('#TestTypeId').val('');

    $("#testCondition :selected").remove();
    $('#testConditionId').val('');
 
    $("#testImportance :selected").remove();
    $('#testImportanceId').val('');

    $('#labratoaryTool :selected').remove();
    $('#labratoaryToolId').val('');

    $('#txt_Amount').val('');

    $("#standard :selected").remove();
    $('#standardId').val('');

    $('#txt_Min').val('');
    $('#txt_Max').val('');

    $("#measure :selected").remove();
    $('#measureId').val('');

    $("#testDescription :selected").remove();
    $('#testDescriptionId').val('');

    $('#txt_FromDate').val('');
    $('#txt_EndDate').val(''); 

    $('#TestId_Selected').val('');
    $('#txt_TestName_update').val('');
    $('#TestTypeId_update').val('');

    $(".alertBox").hide();
}