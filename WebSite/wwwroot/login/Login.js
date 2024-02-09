 

$(function () {

    $("body").on('click', '#btnLogin', function () { 
        $.ajax({
            url: 'Home/LoginValidation',
            data: {},
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            success: function (response) { 

            }
        });
    }); 
});
