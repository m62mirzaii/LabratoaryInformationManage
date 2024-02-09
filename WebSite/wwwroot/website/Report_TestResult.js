
$(document).ready(function () {

    /*$("#div_viewer").hide();*/
    Load_ComboBox();
});

function Load_ComboBox() {


    $("#assessmentType").select2({
        allowClear: true,
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "/api/TestResultReport/GetAssessmentResultForSelect2",
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
        $("#assessmentTypeCode").val(getID[0]['id']);
    });

    $("#receptionNumber").select2({
        allowClear: true,
        minimumInputLength: 0,
        ajax: {
            cache: false,
            dataType: "json",
            type: "Post",
            url: "/api/TestResultReport/ReceptionNumberForSelect2",
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.receptionNumber,
                            id: item.id
                        }
                    })
                };
            }
        },
    }).on('change', function (e) {
        var getID = $(this).select2('data');
        $("#testRequestId").val(getID[0]['id']);


        debugger
        getID.each(function () {
            var num = $(this).val();

            alert(num);
        });


    });

}

function ShowReport() {


    //var testRequestIds = [];
    //$('#receptionNumber :selected').each(function (i, sel) {
    //    testRequestIds.push($(sel).val());
    //});

    var testRequestIds = $("#testRequestId").val();
    var Result_Separation = $("#assessmentTypeCode").val();

    $("#viewer").boldReportViewer({
        parameters: [
            { name: 'Result_Separation', values: [Result_Separation] },
            { name: 'TestRequestIDs', values: [testRequestIds] }
        ]
    });
}

