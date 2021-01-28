$(document).ready(function () {
    debugger;
    LoadData();
    getFormDetails();
    $('#DOB').datetimepicker();
});

$("#btnAddUpdate").click(function () {
    debugger;
    var formdata = {
        Patient_Id: $('#Patient_Id').val(),
        Name: $('#Name').val(),
        DOB: $('#DOB').val(),
        Mobile_No: $('#Mobile_No').val(),
        Email: $('#Email').val(),
    };
    //var formdata = new FormData($("#frmPatient")[0]);
    $('#myModal').modal('hide');
    $("#wait").show();
    $.ajax({
        type: "POST",
        url: '/Patient/AddUpdatePatient',
        data:JSON.stringify({formdata: formdata}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success(result, "Patient Record");
            $('#Patient_Id').val("");
            $("#wait").hide();
            $('#frmPatient')[0].reset();
            LoadData();
        }
});
    
});
function getBaselocation(url) {
    return "/" + url;
}

function LoadData() {
    $("#wait").show();
    $.ajax({
        type: 'GET',
        url: "/Patient/Index",
        success: function (data) {
            $("#wait").hide();
            $("#getPatients").html(data);
        },
        error: function (ex) {
            $("#wait").hide();
        }
    });
}

$('#btnReset').click(function () {
    $('#frmPatient')[0].reset();
});

$('#btnClose').click(function () {
    $('#frmPatient')[0].reset();
});

$('#btnUpperClose').click(function () {
    $('#frmPatient')[0].reset();
});

function DeletePatient(id) {
    debugger;
    if (confirm("Are you sure do you want to delete?")) {
        $("#wait").show();
        $.ajax({
            type: "POST",
            url: "/Patient/RemovePatient",
            data: { id: id },
            success: function (result) {
                if (result == "DeleteSuccess") {
                    toastr.error("Patient deleted successfully", "Patient Record");
                    $("#wait").hide();
                    LoadData();
                }
            },
            error: function () {
                alert("Error while deleting Patient");
                $("#wait").hide();
            }
        });
    }
}

function GetPatient(id) {

    $.ajax({
        type: "GET",
        url: "/Patient/GetPatient",
        data: { id: id },
        success: function (result) {
            debugger;
            $('#Name').val(result.Name);
            $('#Mobile_No').val(result.Mobile_No);
            $('#DOB').val(GetDateInFormat(result.DOB));
            $('#Email').val(result.Email);
            $('#Patient_Id').val(result.Patient_Id);
            $('#myModal').modal('show');
            $('#btnAdd').hide();
        },
        error: function () {
            alert("Data not found");
        }
    });
}

function ExportToExcel() {


    $.ajax({
        type: "POST",
        url: "/Patient/ExportRecordsToExcel",
        success: function (result) {
            debugger;
            toastr.success("Records Exported Successfully");

        },
        error: function () {
            alert("Data not found");
        }
    });
}

$(document).ready(function () {
    $('#DOB').datepicker();
});

function GetDateInFormat(dt) {
    debugger;
    dt = new Date(parseInt(dt.substr(6)));
    var res = "";
    if ((dt.getMonth() + 1).toString().length < 2)
        res += "0" + (dt.getMonth() + 1).toString();
    else
        res += (dt.getMonth() + 1).toString();
    res += "/";
    if (dt.getDate().toString().length < 2)
        res += "0" + dt.getDate().toString();
    else
        res += dt.getDate().toString();
    res += "/" + dt.getFullYear().toString();
    return res;
}

//$("#btnSearch").click(function () {
//    debugger;
//    $("#wait").show();
//    var value = $("#Search").val();
//    $.ajax({
//        type: "POST",
//        url: "/Patient/Index",
//        data: value,
//        success: function (data) {
//            $("#wait").hide();
//            $("#getPatients").html(data);
//        },
//        error: function () {
//        }
//    });
//});

function PatientSearch() {
    $("#wait").show();
    var value = $("#Search").val();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Patient/ExportRecordsToExcel",
        success: function (result) {
            debugger;
            toastr.success("Records Exported Successfully");

        },
        error: function () {
            alert("Data not found");
        }
    });
}

function saveCustomFormDetails()
{
    var formdata = {
        Patient_Id: $('#Patient_Id').val(),
        AveragePain: $('#AveragePain').val(),
        EnjoyementOfLife: $('#EnjoyementOfLife').val(),
        GeneralActivity: $('#GeneralActivity').val(),
        FinalScore: $('#FinalScore').val(),
    };
    $("#wait").show();
    $.ajax({
        type: "POST",
        url: '/Patient/AddUpdatePatient',
        data: JSON.stringify({ formdata: formdata }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            toastr.success(result);
            $('#Patient_Id').val("");
            $("#wait").hide();
            $('#PEG')[0].reset();
            LoadData();
        }
    });
}

function getCustomFormDetails() {
    $("#wait").show();
    $.ajax({
        type: 'GET',
        url: "/Patient/Index",
        success: function (data) {
            $("#wait").hide();
            $("#getPatients").html(data);
        },
        error: function (ex) {
            $("#wait").hide();
        }
    });
}

function getFormDetails()
{
    debugger;
    $("#wait").show();
    $.ajax({
        type: 'GET',
        url: "/Patient/CommonGetCustomFormDetails",
        success: function (data) {
            debugger;
            $("#wait").hide();
            $('#AveragePain').val(data[0].AveragePain);
            $('#EnjoyementOfLife').val(data[0].EnjoyementOfLife);
            $('#GeneralActivity').val(data[0].GeneralActivity);
            $('#FinalScore').val(data[0].FinalScore);
            //$("#getPatients").html(data);
        },
        error: function (ex) {
            $("#wait").hide();
        }
    });
}



