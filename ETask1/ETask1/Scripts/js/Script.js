
$(document).ready(function () {
    highlightActiveMenuItem();

    /*************************function for calender control & default date in AddProject Page*********************/

    $("#frmAddProject").ready(function () {
        $('#txtStartDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        $('#txtDueDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        var date = new Date();
        $('#txtStartDate').val(makeDateString(date));
    });

    /**************************function for calender control & default date in Tasks Page**********************/

    $("#frmTasks").ready(function () {
        $('#txtFromDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        $('#txtToDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        var date = new Date();
        $('#txtToDate').val(makeDateString(date));
        date.setDate(date.getDate() - 7);
        $('#txtFromDate').val(makeDateString(date));
    });

    /**********************function for calender control & default date in EditProject Page************************/

    $("#frmEditProject").ready(function () {
        $('#txtEstStartDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        $('#txtDueDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        //        var date = new Date();
        //        $('#txtStartDate').val(makeDateString(date));
    });


    /**********************function for calender control & default date in ManagerEditProject Page************************/

    $("#frmManagerEditProject").ready(function () {
        $('#txtActStartDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        $('#txtActDueDate').datepick({ dateFormat: 'mm/dd/yyyy' });
        var date = new Date();
        $('#txtActStartDate').val(makeDateString(date));
        $('#txtActDueDate').val(makeDateString(date));
    });

    /****************************function for change password in MyProfile page**************/

    //Function for  validating MyProfile Page:

    $("#frmMyProfile").submit(function () {

        //email

        if (!emptyValidation($('#txtEmail'), "Enter E-mail", $('#msgEmail'))) {
            return false;
        }

        if (!emailValidation($('#txtEmail'), "Enter Valid E-mail", $('#msgEmail'))) {
            return false;
        }

        //mobile

        if (!emptyValidation($('#txtMobile'), "Enter Mobile No:", $('#msgMobile'))) {
            return false;
        }

        if (!isNumeric($('#txtMobile'), "Mobile Number should be Numeric", $('#msgMobile'))) {
            return false;
        }

        if (!maxCount($('#txtMobile'), 10, "Mobile number should be 10 digits", $('#msgMobile'))) {
            return false;
        }

        //if change password is clicked 

        if (frmMyProfile.chkChangePswd.checked == true) {

            //old password

            if (!emptyValidation($('#txtOldPassword'), "Enter Old Password", $('#msgOldPassword'))) {
                return false;
            }

            //new password

            if (!emptyValidation($('#txtNewPassword'), "Enter New Password", $('#msgNewPassword'))) {
                return false;
            }
            if (!passwordValidation($('#txtNewPassword'), "Enter minimum of 8 characters", $('#msgNewPassword'))) {
                return false;
            }

            //confirm password

            if (!emptyValidation($('#txtConfirmPassword'), "Enter Confirm Password", $('#msgConfirmPassword'))) {
                return false;
            }

            //password match

            if (!passwordMatch($('#txtNewPassword'), $('#txtConfirmPassword'), "Password is not matching", $('#msgConfirmPassword'))) {
                return false;
            }
            else {
                return true;
            }
        }
    });



    $("#chkChangePswd").click(function (elem) {

        switch (elem.target.checked) {
            case true: $(".newrow").fadeIn();
                break;
            case false: $(".newrow").css("display", "none");
                break;
        }
    });


    $("#txtOldPassword").blur(function (data) {
        alert("hai");
        var password = $("#txtOldPassword").val();
        alert(password);
        $.get('/User/CheckPassword', { id: password }, function (data) {
            alert(data);
            if (data == true) {
                $("#msgOldPassword").html("Password correct");
            }
            else {
                $("#msgOldPassword").html("Password Incorrect");
            }


        });
    });

    $("table.tblDemo tr:even").addClass("oddrow");

    //Function for adding user in Projects Page:

    $("#btnAdd").click(function () {

        if (!isChecked($('#chkUser'), "Select Users", $('#msgUser'))) {
            return false;
        }
        else {
            $('#display').css("display", "none");
            return true;
        }
    });

    //			
    //function for status change in Tasks page 		

    //    $("#ddlStatus").change(function (elem) {
    //        switch (elem.target.value) {
    //            case "New":
    //                $(".tblTask1").fadeIn();
    //                $(".tblTask2").css("display", "none");
    //                $(".tblTask3").css("display", "none");
    //                $(".tblTask4").css("display", "none");
    //                break;
    //            case "In Progress":
    //                $(".tblTask2").fadeIn();
    //                $(".tblTask1").css("display", "none");
    //                $(".tblTask3").css("display", "none");
    //                $(".tblTask4").css("display", "none");
    //                break;
    //            case "Resolved":
    //                $(".tblTask3").fadeIn();
    //                $(".tblTask1").css("display", "none");
    //                $(".tblTask2").css("display", "none");
    //                $(".tblTask4").css("display", "none");
    //                break;
    //            case "Closed":
    //                $(".tblTask4").fadeIn();
    //                $(".tblTask1").css("display", "none");
    //                $(".tblTask2").css("display", "none");
    //                $(".tblTask3").css("display", "none");
    //                break;
    //        }
    //    });


    /**********************Function to populate status wise tasks - Manager page*********************/

    $("#ddlStatus").change(function () {
        $("#tblStatusList").empty();
        var status = $("#ddlStatus").val();
        var projectid = $("#projectID").val();
        var count = 0;
        //callback function
        $.get('/Task/SearchTasksByStatus', { status: status, projectid: projectid }, function (data) {
            if (status == "Closed") {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/Details/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            else {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/Details/' + value.TaskID + '>' + value.TaskID + '</a></td></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td><td><a href=/Task/Edit/' + value.TaskID + '><img src="/Content/images/edit-img.jpg" width="25" height="25"/></a></td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            $("#tblStatusList").html(table);
            $("table.tblDemo tr:even").addClass("oddrow");
        });
    });

    /***********************************Function to populate status,assignee& date wise tasks - Manager page******************************/

    $("#btnShow").click(function () {
        $("#tblStatusList").empty();
        var status = $("#ddlStatus").val();
        var projectid = $("#projectID").val();
        var assignee = $("#AssignedTo").val();
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        var count = 0;
        $.get('/Task/SearchTasksByDate', { status: status, projectid: projectid, assignee: assignee, fromDate: fromDate, toDate: toDate }, function (data) {
            if (status == "Closed") {

                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/Details/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + correctstartdate + '</td><td>' + value.Priority + '</td><td>' + correctduedate + '</td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            else {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {

                    table += '<tr><td><a href=/Task/Details/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td><td><a href=/Task/Edit/' + value.TaskID + '><img src="/Content/images/edit-img.jpg" width="25" height="25"/></a></td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            $("#tblStatusList").html(table);
            $("table.tblDemo tr:even").addClass("oddrow");
        });
    });

    /****************************Function to populate status wise tasks - User page****************************/

    $("#ddlTaskStatus").change(function () {

        $("#tblUserStatusList").empty();
        var status = $("#ddlTaskStatus").val();
        var count = 0;
        $.get('/Task/SearchUserTasksByStatus', { status: status }, function (data) {
            if (status == "Resolved") {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/TaskDetails/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            else {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/TaskDetails/' + value.TaskID + '>' + value.TaskID + '</a></td></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td><td><a href=/Task/UpdateTasks/' + value.TaskID + '><img src="/Content/images/edit-img.jpg" width="25" height="25"/></a></td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            $("#tblUserStatusList").html(table);
            $("table.tblDemo tr:even").addClass("oddrow");
        });
    });

    /*********************Function to populate status & date wise tasks - User page*****************/

    $("#btnShow1").click(function () {
        $("#tblUserStatusList").empty();
        var status = $("#ddlTaskStatus").val();
        var fromDate = $("#txtFromDate").val();
        var toDate = $("#txtToDate").val();
        var count = 0;
        $.get('/Task/SearchUserTasksByDate', { status: status, fromDate: fromDate, toDate: toDate }, function (data) {
            if (status == "Resolved") {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/TaskDetails/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            else {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th>Task ID</th><th>Subject</th><th>Start Date</th><th>Priority</th><th>Due Date</th><th></th></tr>';
                $.each(data, function (key, value) {
                    table += '<tr><td><a href=/Task/TaskDetails/' + value.TaskID + '>' + value.TaskID + '</a></td><td>' + value.Subject + '</td><td>' + value.StartDate + '</td><td>' + value.Priority + '</td><td>' + value.DueDate + '</td><td><a href=/Task/UpdateTasks/' + value.TaskID + '><img src="/Content/images/edit-img.jpg" width="25" height="25"/></a></td></tr>';
                    count++;
                });
                if (count > 0) {
                    table += '</table>';
                }
                else {
                    var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
                }
            }
            $("#tblUserStatusList").html(table);
            $("table.tblDemo tr:even").addClass("oddrow");
        });
    });

    /*******************************Users Search By Department********************/

    $("#ddlDepartment").change(function () {
        $("#tblUserList").empty();
        //        $("#paging").hide();
        var deptid = $("#ddlDepartment").val();
        var count = 0;
        $.get('/User/SearchUsersByDepartment', { id: deptid }, function (data) {
            var table = '<table class="tblDemo" id="no-more-tables"><tr><th>UserName</th><th>Designation</th><th>UserType</th><th>DOJ</th><th>Email</th><th>Phone No:</th><th></th></tr>';
            $.each(data, function (key, value) {
                table += '<tr><td>' + value.UserName + '</td><td>' + value.Designation + '</td><td>' + value.UserType + '</td><td>' + value.DOJ + '</td><td>' + value.Email + '</td><td>' + value.PhoneNo + '</td><td><a href=Edit/' + value.UserId + '><img src="/Content/images/edit-img.jpg" width="25" height="25"/></a></td></tr>';
                count++;
            });
            //            alert(count);
            //            alert(table);
            if (count > 0) {
                table += '</table>';
            }
            else {
                var table = '<table class="tblDemo" id="no-more-tables"><tr><th></th><th>Sorry No records Found</th></tr></table>';
            }
            $("#tblUserList").html(table);
            //            var footer = createFooter(count, $("#tblUserList"));
            //            alert(footer);
            $("table.tblDemo tr:even").addClass("oddrow");
        });
    });
});

/**********************************Function for empty validation in any field************************************/

function emptyValidation(elem, msg, msgdiv) {
    var name = elem.val();
    if (name.length == 0) {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/**********************************Function to check for alphanumeric data*************************************/

function isAlphaNumeric(elem, msg, msgdiv) {
    var alphaExp = /^[a-zA-Z0-9]+$/;
    var name = elem.val();
    if (name.match(alphaExp)) {
        msgdiv.html("");
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
}

/********************************Function to check for alphabets data*******************************************/

function isAlphabet(elem, msg, msgdiv) {
    var alphaExp = /^[a-zA-Z ]+$/;
    var name = elem.val();
    if (name.match(alphaExp)) {
        msgdiv.html("");
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
}

/***********************************Function for validating Password************************************/

function passwordValidation(elem, msg, msgdiv) {
    var alphaExp = /^[a-zA-Z0-9]{8,15}$/;
    var name = elem.val();

    if (name.match(alphaExp)) {
        msgdiv.html("");
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
}

/*************************************Function for DropDown  validation*********************************************/

function dropDownValidation(elem, msg, msgdiv) {
    var name = elem.val();
    alert(name);
    alert(msgdiv);
    if (name == "--Select--" || name == "") {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/**********************************Function for Email  validation*****************************************/

function emailValidation(mail, msg, msgdiv) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    var email = mail.val();
    if (email.match(emailPattern)) {
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        mail.focus();
        return false;
    }
}

/****************************************Function for Numeric data  validation*****************************************/

function isNumeric(elem, msg, msgdiv) {
    var numExp = /^[0-9]+$/;
    var value = elem.val();
    if (value.match(numExp)) {
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
}

/***************************************Function for maximum count validation***********************************/

function maxCount(elem, max, msg, msgdiv) {
    var value = elem.val();
    if (value.length == max) {
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        elem.focus();
        return false;
    }
}

/************************function for default date validation******************************/

function makeDateString(dateObj) {
    var day = dateObj.getDate();
    var month = dateObj.getMonth(); month++;
    var year = dateObj.getFullYear();
    if (day < 10)
        day = "0" + day;
    if (month < 10)
        month = "0" + month;
    var strDate = (month + "/" + day + "/" + year);
    return strDate;
}

/********************************function for From & To date validation*****************/

function dateCompare(date1, date2, msg, msgdiv) {
    var fromDate = date1.val();
    var toDate = date2.val();
    fromDate = createDateObj(fromDate);
    toDate = createDateObj(toDate);
    if (toDate <= fromDate) {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/*******************************function to make date string***********************************/

function createDateObj(strDate) {
    var month = strDate.substring(0, 2);
    var day = strDate.substring(3, 5);
    var year = strDate.substring(6, 10);
    var dateObj = new Date(year, (month - 1), day);
    return dateObj;
}

/******************************function for due date validation*************************************/

function isFutureDate(elem, msg, msgdiv) {
    var strDate = elem.val();
    strDate = createDateObj(strDate);
    var currentDate = new Date();
    if (currentDate > strDate) {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/*********************************function for checkbox validation*****************************************/

function isChecked(elem, msg, msgdiv) {
    var value = $("input[type='checkbox']:checked").length;
    if (value == 0) {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/*****************************************function for password matching***************************************/

function passwordMatch(pswd, confirmpswd, msg, msgdiv) {
    if (pswd.val() == confirmpswd.val()) {
        msgdiv.html("");
        return true;
    }
    else {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        confirmpswd.focus();
        return false;
    }
}

/****************************function for date of joining validation & start date validation*************************/

function dateValidation(elem, msg, msgdiv) {
    var month = elem.val().substring(0, 2);
    var date = elem.val().substring(3, 5);
    var year = elem.val().substring(6, 10);
    var dojDate = new Date(year, month - 1, date);
    var today = new Date();
    if (dojDate > today) {
        msgdiv.html(msg).css({ "color": "red", "font-size": "11px" });
        return false;
    }
    else {
        msgdiv.html("");
        return true;
    }
}

/***************************to highlight the current menu item************************/

highlightActiveMenuItem = function () {

    var path = window.location.pathname;
    $("#cssmenu a").each(function () {
        var href = $(this).attr('href');
        if (path.substring(0, href.length) === href) {
            $(this).closest('li').addClass('active');
        }
    });
};

/*************************************To add Paging Div for all JSON tables*********************************/

//function createFooter(records, passingDiv) {

//    alert(records);
//    var rowsperpage = 3;
//    var result = 0;
//    if (rowsperpage != 0) {
//        result = records / rowsperpage;
//    }
//    var footer = "<table id=paging><tr><td>Page ";
//    for (var i = 1; i < (result + 1); i++) {
//        footer = footer + "<a href='#'>" + i + "</a>&nbsp;";
//    }
//    footer = footer + "</td></tr></table>";
//    passingDiv.after(footer);
//    return footer;
//}


