$(document).ready(function () { 

    $('#tblLabratoaryTool').DataTable(
        {
            pageLength: 12,
            filter: true,
            deferRender: true,
            scrollCollapse: true,
            scroller: true,
            "searching": true,
            "bInfo": false,
            "lengthChange": false
        }); 

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
                window.open('/LabratoaryTool/Delete/' + id, '_parent');
            }
        });
}


$(function () {

    $("body").on('click', '#btnAdd', function () {
        $("#MyPopup").modal("hide");
        $.ajax({
            url: 'LabratoaryTool/Show_InsertPartial',
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
            url: 'LabratoaryTool/Show_UpdatePartial',
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
    var toolName = $('#txt_ToolName').val();
    if (toolName == null || toolName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام ابزار را وارد نمایید");
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

