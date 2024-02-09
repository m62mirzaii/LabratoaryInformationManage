$(document).ready(function () {

    $("#alertBox").hide();

    LoadTable_Process();
    Load_ComboBox();
});

function LoadTable_Process() {
    $('#tblProcess').DataTable({
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
            "url": "Process/GetProcesses",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [

            { "data": "processName" },
            { "data": "processTypeName" },
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteProcess(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}
function Load_ComboBox() { 

    $("#processType").select2({
        dropdownParent: $("#insertProcessModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "ProcessType/ProcessTypeForSelect2",
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
        $("#ProcessTypeId").val(getID[0]['id']);
    });
     
    $("#processType_update").select2({
        dropdownParent: $("#updateProcessModal"),
        allowClear: true,
        minimumInputLength: 0,
        placeholder: '  ',
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "ProcessType/ProcessTypeForSelect2",
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
        $("#ProcessTypeId_update").val(getID[0]['id']);
    }); 
}


//================================ Insert ====================================================
function ShowPopUp_Insert() {
    RestFormObjects();
    $("#insertProcessModal").modal("show");
}
function InsertProcess() {

    if (!validate_Insert()) {
        return false;
    }
     
    var ProcessName = $('#txt_ProcessName').val();
    var ProcessTypeId = $('#ProcessTypeId').val();
    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'Process/InsertProcess',
        data: {
             
            ProcessName: ProcessName,
            ProcessTypeId: ProcessTypeId,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertProcessModal").modal("hide");
            $('#tblProcess').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertProcessModal").modal("hide");
}
function validate_Insert() {

    var ProcessName = $('#txt_ProcessName').val();
    if (ProcessName == null || ProcessName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام فرآیند را وارد نمایید");
        $("#alertBox").show();
        return false;
    }

    var ProcessTypeId = $('#ProcessTypeId').val();
    if (ProcessTypeId == null || ProcessTypeId == "" || ProcessTypeId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد فرآیند را وارد نمایید");
        $("#alertBox").show();
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updateProcessModal").modal("show");

}

function ShowPopUp_Update(id) {
    $('#tblProcess tbody').on('click', 'tr', function () {

        var row = $('#tblProcess').DataTable().row(this).data();
      
        $('#txt_ProcessName_update').val(row.processName);
        $('#ProcessTypeId_update').val(row.processTypeId);
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
         

        $("#processType_update").select2("trigger", "select", { data: { id: row.processTypeId, text: row.processTypeName } });
    });
     
    $("#updateProcessModal").modal("show");
    $('#processId_Selected').val(id);

}

function UpdateProcess() {

    if (!validate_Update()) {
        return false;
    }
      
    var id = $('#processId_Selected').val();
    var ProcessName = $('#txt_ProcessName_update').val();
    var ProcessTypeId = $('#ProcessTypeId_update').val();
    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'Process/UpdateProcess',
        data: {
            Id: id, 
            ProcessName: ProcessName,
            ProcessTypeId: ProcessTypeId,
            IsActive: isActive,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateProcessModal").modal("hide");
            $('#tblProcess').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateProcessModal").modal("hide");
}
function validate_Update() {

    var ProcessName = $('#txt_ProcessName_update').val();
    if (ProcessName == null || ProcessName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام فرآیند را وارد نمایید");
        $("#alertBox").show();
        return false;
    }

    var ProcessTypeId = $('#ProcessTypeId_update').val();
    if (ProcessTypeId == null || ProcessTypeId == "" || ProcessTypeId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد فرآیند را وارد نمایید");
        $("#alertBox").show();
        return false;
    }
    return true;
}


//================================ DELETE ====================================================
function deleteProcess(id) {
    swal({
        title: "تأیید حذف",
        text: "آیا از حذف این مقدار اطمینان دارید؟",
        icon: "warning",
        buttons: ['لغو', 'بلی'],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                window.open('/Process/Delete/' + id, '_parent');
            }
        });
}
function RestFormObjects() {

    $('#txt_ProcessName').val('');
    $('#ProcessTypeId').val('');

    $('#processId_Selected').val('');
    $('#txt_ProcessName_update').val('');
    $('#ProcessTypeId_update').val('');
}