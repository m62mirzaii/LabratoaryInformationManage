$(document).ready(function () {
    $('#tblProcess').DataTable(

        {
            pageLength: 12,
            filter: true,
            deferRender: true,
            scrollCollapse: true,
            scroller: true,
            "searching": true,
            "bInfo": false,
            "lengthChange": false
        }


    );
});

function myDelete(id) {
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

$(function () {

    $("body").on('click', '#btnAdd', function () {
        $("#MyPopup").modal("hide");
        $.ajax({
            url: 'Process/Show_InsertPartial',
            data: {},
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#dvPartial").html(response);
                $("#MyPopup").modal("show");
            }
        });
    });

    $("body").on('click', '#btnEdit', function () {
        $("#MyPopup").modal("hide");
        var obj = {};
        obj.Id = $(this).attr('data-id');

        $.ajax({
            url: 'Process/Show_UpdatePartial',
            data: JSON.stringify(obj),
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#dvPartial").html(response);
                $("#MyPopup").modal("show");
            }
        });
    });
});

function validateform() {
    var processName = $('#txt_ProcessName').val();
    if (processName == null || processName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام فرآیندها را وارد نمایید");
        return false;
    }

    var definitionId = $('#DefinitionId').val();
    if (definitionId == null || definitionId == "" || definitionId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام Definition  را وارد نمایید");
        return false;
    }
}

$(function () {
    $(document).on("click", "#submit", function (e) {
        if (validateform() == false) {
            e.preventDefault();
        }
    });
});

