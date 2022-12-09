$(document).ready(function () {

    $("#alertBox").hide();

    $('#tblPiece').DataTable({
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
                window.open('/Piece/Delete/' + id, '_parent');
            }
        });
}

$(function () {

    $("body").on('click', '#btnAdd', function () {
        $("#MyPopup").modal("hide");
        $.ajax({
            url: 'Piece/Show_InsertPartial',
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
            url: 'Piece/Show_UpdatePartial',
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
    var code = $('#txt_Code').val();
    if (code == null || code == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کد قطعه را وارد نمایید");
        return false;
    }

    var pieceName = $('#txt_PieceName').val();
    if (pieceName == null || pieceName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام قطعه را وارد نمایید");
        return false;
    }

    var pieceUsageId = $('#pieceUsageId').val();
    if (pieceUsageId == null || pieceUsageId == "" || pieceUsageId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا کاربرد قطعه را وارد نمایید");
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

