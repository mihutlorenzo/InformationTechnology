$("#RegisterViewModel_UserTypeId").change(function () {
    var selectedValue = $('#RegisterViewModel_UserTypeId :selected').text();
    var teacherDIV = $("#teacher");
    if (selectedValue == "Teacher") {
        teacherDIV.show();
    } else {
        teacherDIV.hide();
    }

});
