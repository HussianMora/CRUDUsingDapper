$(document).ready(function () {
    debugger;
    $('#DataTable').DataTable({
        "searching": true,
        "ordering": true,
        "pagingType": "full_numbers"
    });
    $('[data-toggle="popover"]').popover({
        title: setPopoverData,
        html: true,
        placement: 'right'
    });
    function setPopoverData(id) {
        var set_data = '';
        var element = $(this);
        var id = element.attr("id");
        $.ajax({
            url: "/Patient/PatientPopoverDetails",
            method: "post",
            async: false,
            data: { id: id },
            success: function (data) {
                set_data = '<div class="container">';
                set_data += ' <ul class="list-group">';
                set_data += '<li class="list-group-item">' + data.Patient_Id + '</li>';
                set_data += '<li class="list-group-item">' + data.Name + '</li>';
                set_data += '<li class="list-group-item">' + GetDateInFormat(data.DOB) + '</li>';
                set_data += '<li class="list-group-item">' + data.Mobile_No + '</li>';
                set_data += '<li class="list-group-item">' + data.Email + '</li>';
                set_data += '</ul>';
                set_data += '</div>';
            }
        });
        return set_data;
    }
});

$("#btnAddUpdate").click(function () {
    var formdata = {
        Patient_Id: $('#Patient_Id').val(),
        Name: $('#Name').val(),
        DOB: $('#DOB').val(),
        Mobile_No: $('#Mobile_No').val(),
        Email: $('#Email').val(),
    };
    $('#myModal').modal('hide');
    $("#wait").show();
    $.ajax({
        type: "POST",
        url: '/Patient/AddUpdatePatient',
        data: JSON.stringify({ formdata: formdata }),
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
    blockPage();
    $(".loader").show();
    $.ajax({
        type: 'GET',
        url: "/Patient/Index",
        success: function (data) {
            unblockPage();
            $(".loader").hide();
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
    showConfirmation("", "Are you sure do you want to delete?", function () {
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
    }, function () {
        return false;
    });
}

function GetPatient(id) {

    $.ajax({
        type: "GET",
        url: "/Patient/GetPatient",
        data: { id: id },
        success: function (result) {
            var item1 = result.Item1[0];
            var item2 = result.Item2[0];
            $('#Name').val(item1.Name);
            $('#Mobile_No').val(item1.Mobile_No);
            $('#DOB').val(GetDateInFormat(item2.DOB));
            $('#Email').val(item2.Email);
            $('#Patient_Id').val(item1.Patient_Id);
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

function PatientSearch() {
    $("#wait").show();
    var value = $("#Search").val();
    $.ajax({
        type: "POST",
        url: "/Patient/ExportRecordsToExcel",
        success: function (result) {
            toastr.success("Records Exported Successfully");

        },
        error: function () {
            alert("Data not found");
        }
    });
}

function saveCustomFormDetails() {
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

function getFormDetails() {
    $("#wait").show();
    $.ajax({
        type: 'GET',
        url: "/Patient/CommonGetCustomFormDetails",
        success: function (data) {
            $("#wait").hide();
            $('#AveragePain').val(data[0].AveragePain);
            $('#EnjoyementOfLife').val(data[0].EnjoyementOfLife);
            $('#GeneralActivity').val(data[0].GeneralActivity);
            $('#FinalScore').val(data[0].FinalScore);
        },
        error: function (ex) {
            $("#wait").hide();
        }
    });
}

function registerPatient() {
    if (valRegister()) {
        var formData = new FormData();
        formData.append("Name", $("#Name").val());
        formData.append("Email", $("#Email").val());
        formData.append("DOB", $("#DOB").val());
        formData.append("Password", $("#Password").val());
        $.ajax({
            type: "POST",
            url: "/Account/Register",
            processData: false,
            contentType: false,
            data: formData,
            success: function (result) {
                setTimeout(function () {
                    window.location.href = result.redirectTo;
                }, 500);
                toastr.success("Patient Registered Successfully");
            }
        });
    };
}

function valRegister() {
    if ($('#Name').val() == '') {
        toastr.warning("Name cannot be blank");
        return false;
    }
    valEmail = validateEmail();
    if (valEmail == 0) {
        toastr.warning("Please Enter Email Address");
        return false;
    }
    if (valEmail == 2) {
        toastr.warning("Please Enter Correct Email Address");
        return false;
    }
    if ($('#DOB').val() == '') {
        toastr.warning("Date cannot be blank");
        return false;
    }
    valPass = valPassword();
    if (valPass == 0) {
        toastr.warning("Password cannot be blank");
        return false;
    }
    if (valPass == 1) {
        toastr.warning("Password lenght cannot be less than 8");
        return false;
    }
    if (valPass == 2) {
        toastr.warning("Password must contain atleast one upper case ,lower case and number");
        return false;
    }
    if (valPass == 3) {
        toastr.warning("Password must contain one special character");
        return false;
    }
    confrmPass = confirmPassword();
    if (confrmPass == 0) {
        toastr.warning("Please Confirm Password");
        return false;
    }
    if (confrmPass == 1) {
        toastr.warning("Password doesn't match");
        return false;
    }
    else {
        return true;
    }
}
function validateEmail() {
    if ($('#Email').val() == '') {
        return 0;
    }
    var filter = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (filter.test($('#Email').val())) {
        return 1;
    }
    else {
        return 2;
    }
}

function valPassword() {
    if ($('#Password').val() == '') {
        return 0;
    }
    a = $('#Password').val();
    if (a.length < 8) {
        return 1;
    }
    if (!$('#Password').val().match(/([a-z])/) || !$('#Password').val().match(/([0-9])/) || !$('#Password').val().match(/([A-Z])/)) {
        return 2;
    }
    if (!$('#Password').val().match(/([!,%,&,@,#,$,^,*,?,_,~])/)) {
        return 3;
    }
}

function confirmPassword() {
    if ($('#ConfirmPassword').val() == '') {
        return 0;
    }
    if ($('#Password').val() != $('#ConfirmPassword').val()) {
        return 1;
    }
}

function myFunction() {
    var popup = document.getElementById("myPopup");
    popup.classList.toggle("show");
}

function openQuickAddPatientPopup() {
    var modalNarrativesPHQ9 = $("#modalNarratives");
    $("#modalBodyNarratives").load('/Patient/QuickAddPatient', function () {
        modalNarrativesPHQ9.modal('show');
    });
}

function quickAddPatient() {
    var formdata = {
        Name: $('#QA_Name').val(),
        DOB: $('#QA_DOB').val(),
    };
    $('#modalNarratives').modal('hide');
    $("#wait").show();
    $.ajax({
        type: "POST",
        url: '/Patient/AddUpdatePatient',
        data: JSON.stringify({ formdata: formdata }),
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
}

