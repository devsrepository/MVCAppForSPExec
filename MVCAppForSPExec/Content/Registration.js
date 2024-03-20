$(document).ready(function () {
    GetRegistrationList();
    GetRegistrationListById();
    GetRegListWithCityState();
})
/*this is for save the record without photo upload*/
//var saveRegistration = function () {
//    debugger;
//    var id = $("#hdnId").val();
//    var firstName = $("#txtFirstName").val();
//    var lastName = $("#txtLastName").val();
//    var email = $("#Email").val();
//    var password = $("#txtpassword").val();

//    model = {
//        Id:id,
//        FirstName: firstName,
//        LastName: lastName,
//        Email: email,
//        Password: password
//    }

//    $.ajax({
//        url: "/Registration/SaveRegistration",
//        method: "POST",
//        data: JSON.stringify(model),
//        dataType: "json",
//        contentType: "application/json;charset=utf-8",
//        async: false,
//        success: function (response) {
//            GetRegistrationList();
//            window.location("~/Registration/List");
//        }

//    });
//}
var saveRegistration = function () {
    debugger;
    var $formData = new FormData();
    var image = document.getElementById('flPhoto');

    if (image.files.length > 0) {
        for (var i = 0; i < image.files.length; i++) {
            $formData.append('image-' + i, image.files[i]);
        }
    }
    var regid = $("#hdnId").val();
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var email = $("#Email").val();
    var password = $("#txtpassword").val();

    $formData.append('RegId', regid);
    $formData.append('FirstName', firstName);
    $formData.append('LastName', lastName);
    $formData.append('Email', email);
    $formData.append('Password', password);

    $.ajax({
        url: "/Registration/SaveRegistration",
        method: "POST",
        data: $formData,
        dataType: false,
        /*contentType: "application/json;charset=utf-8",*/
        contentType: false,
        /*async: false,*/
        processData: false,
        success: function (response) {
            GetRegistrationList();
            //window.location("~/Registration/List");
        }
    });
}
var GetRegistrationList = function () {
    debugger;
    $.ajax({
        url: "/Registration/GetRegistrationList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblRegistrationList tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.RegId + "</td><td>" + elementValue.FirstName + "</td><td>" +
                    elementValue.LastName + "</td><td>" + elementValue.Email +
                    "</td><td>" + elementValue.Password + "</td><td><img src='../Content/img/" +  elementValue.Photo + "'style='height:40px; width:40px;'/></td><td>" +
                    "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/></td></tr>";

            });

            $("#tblRegistrationList tbody").append(html);
        }
    });
}

//here the function which get list by joining 3 tables.
var GetRegListWithCityState = function () {
    debugger;
    $.ajax({
        url: "/Registration/GetRegListWithCityState",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblRegListWithCityState tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.FirstName + "</td><td>" +
                    elementValue.LastName + "</td><td>" + elementValue.Email +
                    "</td><td><img src='../Content/img/" + elementValue.Photo + "'style='height:40px; width:40px;'/></td><td>" + elementValue.CityName
                    + "</td><td>" + elementValue.StateName + "</td></tr>"
                    
            });

            $("#tblRegListWithCityState tbody").append(html);
        }
    });
}


//var GetDetails = function (id) {

//    debugger;
//    var model = { Id: id };

//    $.ajax({
//        url: "/PhotoGallery/EditPhotGalleryRow",
//        method: "POST",
//        data: JSON.stringify(model),
//        datatype: "json",
//        contentType: "application/json;charset=utf-8",
//        async: false,
//        success: function (response) {
//            $('#CategoryModal').modal('show');

//            $("#lblId").text(response.model.Id);
//            $("#lblTitle").text(response.model.Title);
//            $("#lblImage1").text(response.model.Image1);
//            $("#lblImage2").text(response.model.Image2); 
           

//            $("#tblRegistrationList tbody").append(html);
//        }
//    });
//}


var DeleteRecord = function (id) {
    debugger;
    var model = { RegId: id };
    $.ajax({
        url: "/Registration/DeleteRegistrationRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert("row deleted successfully");
            GetRegistrationList;
                                  //for detailIndex page where page get list after click on delete button.
        }
    });
}


var EditRecord = function (id) {
    debugger;
    var model = { RegId: id };
    $.ajax({
        url:"/Registration/EditRegistrationRecord",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert(response);
            console.log(response);
            $("#hdnId").val(response.model.RegId);
            $("#txtFirstName").val(response.model.FirstName);
            $("#txtLastName").val(response.model.LastName);
            $("#Email").val(response.model.Email);
            $("#txtpassword").val(response.model.Password);
            
        }
    });
}

var GetDetails = function (id) {
    debugger;
    var model = { RegId:id };
    $.ajax({
        url: "/Registration/GetRegistrationList",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {

            $("#CategoryModal").modal('show');

            $("#lblId").text(response.model.RegId);
            $("#lblFirstName").text(response.model.FirstName);
            $("#lblLastName").text(response.model.LastName);
            $("#lblEmail").text(response.model.Email);
            $("#lblPassword").text(response.model.Password);

        }


    });
}

var GetDetailIndex = function (RegId) {
    debugger;
    window.location.href = "/Registration/DetailIndex?id=" + RegId;
}

var GetRegistrationListById = function (id) {
    debugger;
    var id = $("#hdnId").val();
    model = { RegId: id };

    $.ajax({
        url: "/Registration/GetRegistrationListById",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $('#CategoryModal').modal('show');
            var html = "";
            $("#tblRegListById tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.RegId + "</td><td>" + elementValue.FirstName + "</td><td>" +
                    elementValue.LastName + "</td><td>" + elementValue.Email + "</td><td>" + elementValue.Password +
                   
                    "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")' /></td></tr>";

            });

            $("#tblRegListById tbody").append(html);
        }

    });
} 