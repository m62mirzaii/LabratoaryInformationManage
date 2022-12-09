$(document).ready(function () {
    $('#tblUser').DataTable( 
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
                window.open('/User/Delete/' + id, '_parent');
            }
        });
}

$(function () {

    $("body").on('click', '#btnAdd', function () {
        $("#MyPopup").modal("hide");
        $.ajax({
            url: 'User/Show_InsertPartial',
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
            url: 'User/Show_UpdatePartial',
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
    var firstName = $('#txt_FirstName').val();
    if (firstName == null || firstName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام را وارد نمایید");
        return false;
    }

    var lastName = $('#txt_LastName').val();
    if (lastName == null || lastName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام خانوتدگی را وارد نمایید");
        return false;
    }

    var phone = $('#txt_Phone').val();
    if (phone == null || phone == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا تلفن را وارد نمایید");
        return false;
    }

    var address = $('#txt_Address').val();
    if (address == null || address == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا آدرس را وارد نمایید");
        return false;
    }

    var userName = $('#txt_UserName').val();
    if (userName == null || userName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام کاربری را وارد نمایید");
        return false;
    }

    var password = $('#txt_Password').val();
    if (password == null || password == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا رمز عبور را وارد نمایید");
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

