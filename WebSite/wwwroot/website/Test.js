$(document).ready(function () {

    $('#tblTest').DataTable({
        pageLength: 12,
        filter: true,
        deferRender: true,
        scrollCollapse: true,
        scroller: true,
        "searching": true,
        "bInfo": false,
        "lengthChange": false
    });

    document.querySelectorAll('#tblTest tr > td:nth-child(3)').forEach(e => {
        e.textContent = numberSeparator(e.textContent.trim());
    })
});

function numberSeparator(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

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
            window.open('/Test/Delete/' + id, '_parent');
        }
    });
}

$(function () {

    $("body").on('click', '#btnAdd', function () {
        $("#MyPopup").modal("hide");
        $.ajax({
            url: 'Test/Show_InsertPartial',
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
            url: 'Test/Show_UpdatePartial',
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
    var testName = $('#txt_TestName').val();
    if (testName == null || testName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا آزمایش را وارد نمایید");
        return false;
    }

    var processId = $('#ProcessId').val();
    if (processId == null || processId == "" || processId == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا فرآیند را وارد نمایید");
        return false;
    }

    var amount = $('#txt_Amount').val();
    if (amount == null || amount == "" || amount == "0") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا هزینه را وارد نمایید");
        return false;
    }

    var fromDate = $('#txt_FromDate').val();
    if (fromDate == null || fromDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا از تاریخ را وارد نمایید");
        return false;
    }

    var endDate = $('#txt_EndDate').val();
    if (endDate == null || endDate == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تا تاریخ را وارد نمایید");
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

