$(document).ready(function () {

    $(".alertBox").hide();

    LoadTable_User();
});

function LoadTable_User() {
    $('#tblUser').DataTable({
        pageLength: 18,
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
            "url": "User/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        fixedColumns: true,
        "columns": [
            { "data": "fullName" },
            { "data": "bankAccountNo" },
            { "data": "phone" },
            { "data": "address" },
            { "data": "userName" },
            {
                "render": function (data, type, row) {
                    if (row.isAdmin) {
                        return '   <input type="checkbox" class="form-check-input" disabled checked="checked">'
                    }
                    else {
                        return '   <input type="checkbox" class="form-check-input" disabled>'
                    }

                },
                orderable: false,
            },
            {
                "render": function (data, type, row) {
                    if (row.isActive) {
                        return '   <input type="checkbox" class="form-check-input" disabled checked="checked">'
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
                    return '<a  id="btnDelete" title="حذف" onmouseover="" style="cursor: pointer; color:red" onclick="deleteUser(' + row.id + ')"><i style="color:black;font-size:small" class="fa fa-trash"></i></a>'
                },
                orderable: false,
            },
        ]
    });
}

//================================ Insert ====================================================
function ShowPopUp_Insert() {
    RestFormObjects();
    $(".alertBox").hide();
    $("#insertUserModal").modal("show");
}
function InsertUser() {

    if (!validate_Insert()) {
        $(".alertBox").show();
        return false;
    }
     
    var firstName =  $('#txt_FirstName').val( );
    var lastName = $('#txt_LastName').val();
    var bankAccountNo = $('#txt_BankAccountNo').val();
    var phone = $('#txt_Phone').val();
    var address = $('#txt_Address').val();
    var userName = $('#txt_UserName').val();
    var password = $('#txt_Password').val();

    var isAdmin = false;
    if ($('#chk_IsAdmin').prop('checked')) {
        isAdmin = true;
    }

    var isActive = false;
    if ($('#chk_IsActive').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'User/AddUser',
        data: {
            FirstName: firstName,
            LastName: lastName,
            BankAccountNo: bankAccountNo,
            Phone: phone,
            Address: address,
            UserName: userName,
            Password: password, 
            IsActive: isActive,
            IsAdmin: isAdmin,
        },
        dataType: 'html',
        success: function (response) {
            $("#insertUserModal").modal("hide");
            $('#tblUser').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}
function ClosePopUp_Insert() {
    RestFormObjects();
    $("#insertUserModal").modal("hide");
}
function validate_Insert() {

    var firstName = $('#txt_FirstName').val();
    if (firstName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام را وارد نمایید");
        return false;
    }

    var lastName = $('#txt_LastName').val();
    if (lastName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام خانوادگی را وارد نمایید");
        return false;
    }

    var userName = $('#txt_UserName').val();
    if (userName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام کاربری را وارد نمایید");
        return false;
    }

    var password = $('#txt_Password').val();
    if (password == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا رمز عبور را وارد نمایید");
        return false;
    }
    return true;
}

//================================ Update ====================================================
function ShowPopUp_Update() {
    $("#updateUserModal").modal("show");
}

function ShowPopUp_Update(id) {
    $('#tblUser tbody').on('click', 'tr', function () {

        var row = $('#tblUser').DataTable().row(this).data();

        $('#txt_FirstName_update').val(row.firstName);
        $('#txt_LastName_update').val(row.lastName);
        $('#txt_BankAccountNo_update').val(row.bankAccountNo);
        $('#txt_Phone_update').val(row.phone);
        $('#txt_Address_update').val(row.address);
        $('#txt_UserName_update').val(row.userName);
        $('#txt_Password_update').val(row.password); 

        $('#chk_IsAdmin_update').prop("checked", false);
        if (row.isAdmin) {
            $('#chk_IsAdmin_update').prop("checked", true)
        }
        $('#chk_IsActive_update').prop("checked", false);
        if (row.isActive) {
            $('#chk_IsActive_update').prop("checked", true)
        }
    });

    $("#updateUserModal").modal("show");
    $('#userId_Selected').val(id);
}
 
function UpdateUser() {

    if (!validate_Update()) {
        $(".alertBox").show();
        return false;
    }

    var id = $('#userId_Selected').val();
    var firstName = $('#txt_FirstName_update').val();
    var lastName = $('#txt_LastName_update').val();
    var bankAccountNo = $('#txt_BankAccountNo_update').val();
    var phone = $('#txt_Phone_update').val();
    var address = $('#txt_Address_update').val();
    var userName = $('#txt_UserName_update').val();
    var password = $('#txt_Password_update').val();

    var isAdmin = false;
    if ($('#chk_IsAdmin_update').prop('checked')) {
        isAdmin = true;
    }

    var isActive = false;
    if ($('#chk_IsActive_update').prop('checked')) {
        isActive = true;
    }

    $.ajax({
        type: 'POST',
        url: 'User/UpdateUser',
        data: {
            Id: id,
            FirstName: firstName,
            LastName: lastName,
            BankAccountNo: bankAccountNo,
            Phone: phone,
            Address: address,
            UserName: userName,
            Password: password,
            IsActive: isActive,
            IsAdmin: isAdmin,
        },
        dataType: 'html',
        success: function (response) {
            $("#updateUserModal").modal("hide");
            $('#tblUser').DataTable().ajax.reload();
            RestFormObjects();
        }
    });
}

function ClosePopUp_Update() {
    RestFormObjects();
    $("#updateUserModal").modal("hide");
}
function validate_Update() {


    var firstName = $('#txt_FirstName_update').val();
    if (firstName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام را وارد نمایید");
        return false;
    }

    var lastName = $('#txt_LastName_update').val();
    if (lastName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام خانوادگی را وارد نمایید");
        return false;
    }

    var userName = $('#txt_UserName_update').val();
    if (userName == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا نام کاربری را وارد نمایید");
        return false;
    }

    var password = $('#txt_Password_update').val();
    if (password == "") {
        $(".alertBox span").addClass('alert-danger')
        $(".alertBox span").text("لطفا رمز عبور را وارد نمایید");
        return false;
    }
    return true;
}


//================================ DELETE ====================================================
function deleteUser(id) {
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
function RestFormObjects() {

    $('#userId_Selected').val('');
    $('#txt_FirstName').val('');
    $('#txt_LastName').val('');
    $('#txt_BankAccountNo').val('');
    $('#txt_Phone').val('');
    $('#txt_Address').val('');
    $('#txt_UserName').val('');
    $('#txt_Password').val('');

    $('#txt_FirstName_update').val('');
    $('#txt_LastName_update').val('');
    $('#txt_BankAccountNo_update').val('');
    $('#txt_Phone_update').val('');
    $('#txt_Address_update').val('');
    $('#txt_UserName_update').val('');
    $('#txt_Password_update').val('');
}