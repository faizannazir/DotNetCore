// Get List of Role
function GetRolesList() {
    $("#Modal").modal('hide');
    $.ajax({
        type: 'Get',
        url: '/Admin/ManageAccounts/RolesList/',
        success: function (response) {
            $("#roleTable").html(response);

        },
        error: function () {
            
        }
    });
}


// Get Create Form
function CreateRoleGet()
{
    $.ajax({
        type: 'Get',
        url: '/Admin/ManageAccounts/CreateRole/',
        success: function (response) {
                $(".guard_form").html(response);
        },
        error: function () {
            toastr.error('Failed to load form', 'Error', { timeOut: 0 });
        }
    });
}


// Post Create Role
function CreateRolePost() {
    var formData = $('#myForm').serialize();
    $.ajax({
        type: 'Post',
        data:formData,
        url: '/Admin/ManageAccounts/CreateRole/',
        success: function (response) {
            if (response.success) {
                GetRolesList();
            }
            else {
                $(".guard_form").html(response);
            }
        },
        error: function () {
            toastr.error('Failed to submit form', 'Error', { timeOut: 0 });
        }
    });
}




// Get Update Form
function UpdateRoleGet(id) {
    $.ajax({
        type: 'Get',
        data: {Id:id},
        url: '/Admin/ManageAccounts/UpdateRole/',
        success: function (response) {
            $(".guard_form").html(response);
        },
        error: function () {
            toastr.error('Failed to get form', 'Error', { timeOut: 0 });
        }
    });
}


// Post Update Role
function UpdateRolePost() {
    var formData = $('#myForm').serialize();
    $.ajax({
        type: 'Post',
        data: formData,
        url: '/Admin/ManageAccounts/UpdateRole/',
        success: function (response) {
            if (response.success) {
                GetRolesList();
            }
            else {
                $(".guard_form").html(response);
            }
        },
        error: function () {
            toastr.error('Failed to submit form', 'Error', { timeOut: 0 });
        }
    });
}






// Get Delete Page
function DeleteRoleGet(id) {
    $.ajax({
        type: 'Get',
        data: { Id: id },
        url: '/Admin/ManageAccounts/DeleteRole/',
        success: function (response) {
            $(".guard_form").html(response);

        },
        error: function () {
           
        }
    });
}


//  Delete Role
function DeleteRolePost(id) {
    $.ajax({
        type: 'Post',
        data: { Id: id },
        url: '/Admin/ManageAccounts/DeleteRole/',
        success: function (response) {
            if (response.success) {
                GetRolesList();
            }
            else {
                $(".guard_form").html(response);
            }
        },
        error: function () {
            toastr.error('Failed to delete ', 'Error', { timeOut: 0 });
        }
    });
}