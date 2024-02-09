$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_TestLabratoaryTool();
    Load_ComboBox();
});

function LoadTable_TestLabratoaryTool() {
 
    $('#tblTestLabratoaryTool').DataTable({
        pageLength: 18,
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
            "url": "TestLabratoaryTool/GetTestLabratoaryTooles",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "testName" },
            { "data": "labratoaryToolName" }, 
            {
                "render": function (data, type, row) {
                    return ' <a href="#" id="btnEdit" title="ویرایش"  onclick="ShowPopUp_Update(' + row.id + ')" > <i style="color:black;font-size:small" class="fa fa-edit"></i></a>'
                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteTestLabratoaryTool(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}
function Load_ComboBox() { 

    $("#test").select2({
        dropdownParent: $("#insertModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Test/GetTestForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.testName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#testId").val(getID[0]['id']);
    });


    $("#labratoaryTool").select2({
        dropdownParent: $("#insertModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
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

    $("#test_update").select2({
        dropdownParent: $("#updateModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "Test/GetTestForSelect2",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.testName,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#testId_update").val(getID[0]['id']);
    });

    $("#labratoaryTool_update").select2({
        dropdownParent: $("#updateModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
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
}


//================================ Insert ====================================================
function ShowPopUp_Insert() {
    $("#insertModal").modal("show");
}
function InsertTestLabratoaryTool() {

    if (!validate_Insert()) {
        return false;
    }

    var labratoaryToolId = $('#labratoaryToolId').val();
    var testId = $('#testId').val(); 

    $.ajax({
        type: 'POST',
        url: 'TestLabratoaryTool/InsertTestLabratoaryTool',
        data: {

            LabratoaryToolId: labratoaryToolId,
            TestId: testId, 
        },
        dataType: 'html',
        success: function (response) {
            $("#insertModal").modal("hide");
            $('#tblTestLabratoaryTool').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertModal").modal("hide");
}
function validate_Insert() { 

    var testId = $('#testId').val();
    if (testId == null || testId == "" || testId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام آزمایش را وارد نمایید");
        $(".alertBox").show();
        return false;
    }

    var labratoaryToolId = $('#labratoaryToolId').val();
    if (labratoaryToolId == null || labratoaryToolId == "" || labratoaryToolId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام ابزار را وارد نمایید");
        $(".alertBox").show();
        return false;
    }

    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updateModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblTestLabratoaryTool tbody').on('click', 'tr', function () {
        var row = $('#tblTestLabratoaryTool').DataTable().row(this).data();         
        $("#test_update").select2("trigger", "select", { data: { id: row.testId, text: row.testName } });
        $("#labratoaryTool_update").select2("trigger", "select", { data: { id: row.labratoaryToolId, text: row.labratoaryToolName } });
    });

    $("#updateModal").modal("show");
    $('#testLabratoaryToolId_Selected').val(id);
}

function UpdateTestLabratoaryTool() {

    if (!validate_Update ()) {
        return false;
    }

    var id = $('#testLabratoaryToolId_Selected').val();
    var labratoaryToolId = $('#labratoaryToolId_update').val();
    var testId = $('#testId_update').val();  

    $.ajax({
        type: 'POST',
        url: 'TestLabratoaryTool/UpdateTestLabratoaryTool',
        data: {
            Id: id,
            LabratoaryToolId: labratoaryToolId,
            TestId: testId, 
        },
        dataType: 'html',
        success: function (response) {
            $("#updateModal").modal("hide");
            $('#tblTestLabratoaryTool').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateModal").modal("hide");
}
function validate_Update() {  
 
    var testId = $('#testId_update').val();
    if (testId == null || testId == "" || testId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام آزمایش را وارد نمایید");
        return false;
    }

    var labratoaryToolId = $('#labratoaryToolId_update').val();
    if (labratoaryToolId == null || labratoaryToolId == "" || labratoaryToolId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام ابزار را وارد نمایید");
        $(".alertBox").show();
        return false;

    }
    return true;
} 

//================================ DELETE ====================================================
function deleteTestLabratoaryTool(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/TestLabratoaryTool/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#testLabratoaryToolId_Selected').val();
    $('#labratoaryToolId').val(''); 
    $('#testId').val('');

    $('#labratoaryToolId_update').val('');
    $('#testId_update').val('');
}