//Get Data 
function CreateUpdateCategory(categoryId = '') {
    //toastr.info('Loading form', '', { timeOut: 0 });
    $.ajax({
            type: 'Get',
            data: { Id: categoryId },
            url: '/Admin/Category/AddUpdate/',
            success: function (response) {
                $(".guard_form").html(response);
                //toastr.clear();
        },
        error: function () {
            toastr.error('Failed to load form', 'Error', { timeOut: 0 });
        }
        });
}


//Post Data
function SubmitCategory() {
    // Serialize the form data.
    //toastr.info('Submitting data...', 'Info', { timeOut: 0,positionClass: 'toast-top-right' }); // Show infinite toastr info

    var formData = $('#myForm').serialize();
//toastr.clear();
    // Create an AJAX call to the controller action.
    $.ajax({
        
        url: '/Admin/Category/AddUpdate/',
        type: 'post',
        data: formData,
        success: function (result) {
            toastr.clear(); // Clear submitting data toast
            if (result.isSuccessful) {
                toastr.success(result.message, 'Success', { positionClass: 'toast-top-right' }); // Show success toast on top-right position
                window.location.href = result.url;
            } else {
                toastr.error(result.message , 'Error', { positionClass: 'toast-top-right' }); // Show error toast on top-right position
            }
        },
        error: function (xhr, status, error) {
            toastr.clear(); // Clear submitting data toast
            toastr.error("An error occurred while submitting data.", "Error", { positionClass: 'toast-top-right' }); // Show error toast on top-right position
        },
        complete: function () {
            toastr.clear(); // Clear any remaining toastr
        }
    });
}


//Get Data to delete
function DeleteCategoryGet(categoryId) {
    $.ajax({
        type: 'Get',
        data: { Id: categoryId },
        url: '/Admin/Category/Delete/',
        success: function (response) {
            $(".guard_form").html(response);
          /*  window.location.href = 'index'*/
        }
    });
}

//Delete Entry
function DeleteCategory(categoryId) {
    $.ajax({
        type: 'Delete',
        data: { Id: categoryId },
        url: '/Admin/Category/Delete/',
        success: function (response) {
           /* $(".guard_form").html(response);*/
            window.location.href = '/Category/index'
            location.reload()
        }
    });
}





function confirmDelete(Id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "Cancel"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                data: { Id: Id },
                url: "/Admin/Category/Delete/",
                success: function () {
                    Swal.fire("Done!", "It was successfully deleted!", "success");
                    location.reload();
                },
                error: function () {
                    Swal.fire("Error", "Failed to delete the file.", "error");
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire("Cancelled", "Your imaginary file is safe :)", "error");
        }
    });
}

