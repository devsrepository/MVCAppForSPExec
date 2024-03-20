


//var saveRegistration = function () {
//    debugger;
//    var id = $("#hdnId").val();
//    var name = $("#txtName").val();
//    var mobileNo = $("#txtMobileNo").val();
//    var cityName = $("#txtCityName").val();
//    var stateName = $("#txttxtstateName").val

//    model = { Id: id, Name: name, MobileNo: mobileNo, CityName: cityName, StateName: stateName };

//    $.ajax({
//        url: "/Appointment/saveAppointment",
//        method: "POST",
//        contentType: "application/json;charset=utf-8",
//        data: JSON.stringify(model),
//        datatype: "json",
//        success: function (response) {
//            alert(response.model);
//            GetAppointment();
//        },
//        error: function () {
//            alert("error occured");
//        }
//}

var saveAppointment = function () {
    debugger;
    var id = $("#hdnId").val() //this is added after edit the row
    var name = $("#txtName").val();
    var email = $("#txtEmail").val();
    var city = $("#ddlCity").val();
    var mobileno = $("#txtMobileNo").val();
    var appointmentDate = $("#txtAppointmentDate").val();
    var gender = $("#txtGender").val();
    var message = $("#txtMessage").val();
    var createdate = $("#dtCreateDate").val();
    var updatedate = $("#dtUpdateDate").val();
    var createdby = $("#txtCreatedby").val();
    var updatedby = $("#txtUpdatedby").val();
    var state = $("#ddlStates").val(); //added for onchange

    var model = {
        Id: id, //this is added after edit the row
        Name: name,
        Email: email,
        City: city, MobileNo: mobileno, AppointmentDate: appointmentDate,
        Gender: gender, Message: message, CreateDate: createdate, UpdateDate: updatedate,
        CreatedBy: createdby, UpdatedBy: updatedby, State: state
    };
    if (name == "") {
        debugger;
        /* alert("please enter Mobile");*/
        $("#spnName").text("please enter your valid mobile number");
        $("#txtName").focus();
        return false;
    }
    else {
        $.ajax({
            url: "/Appointment/saveAppointment",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(model),
            datatype: "json",
            success: function (response) {
                alert(response.model);
                GetAppointment();
            },
            error: function () {
                alert("error occured");
            }
        });
    }

}
var GetAppointment = function () {
    debugger;
    $.ajax({
        url: "/Appointment/GetAppointmentList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {

            var html = "";
            $("#tblAppointment tbody").empty();

            $.each(response.model, function (index, elementValue) {

                html += "<tr><td>" + elementValue.Id + "</td><td>"
                    + elementValue.Name + "</td><td>" + elementValue.MobileNo 
                    "</td><td>" + 
                    elementValue.CityName + "</td><td>"
                    + elementValue.StateName +
                    "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/></td></tr>";


            });

            $("#tblAppointment tbody").append(html);
        }
    });
}
