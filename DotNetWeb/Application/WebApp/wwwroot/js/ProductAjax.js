function CreateUpdateProduct(productId = '') {
    $.ajax({
        type: 'Get',
        data: { Id: productId },
        url: '/Admin/Product/AddUpdate/',
        success: function (response) {
            $(".guard_form").html(response);

        },
        error: function (response) {

        }
    });
}

//// Post Data
//function SubmitProduct() {
//    //debugger
//    var data = {
//        Name: $('#Name').val(),
//        Description: $('#Description').val(),
//        Price: $('#Price').val(),
//        File: $('#File')[0].files[0]
//    }
//    // Create an AJAX call to the controller action.
//    $.ajax({
//        url: '/Admin/Product/AddUpdate/',
//        type: 'post',
//        data: data,
//        success: function (result) {
//            // Render the partial view with the updated data.
//            $('#Modal').modal('hide');
//        },
//        error: function (response) {
//            $(".guard_form").html(response);
//            alert(response)
//        }

//    });
//}

function SubmitProduct() {
    var formData = new FormData();
    var fileInput = document.getElementById('File');
    formData.append('File', fileInput.files[0]);
    formData.append('ImageUrl', document.getElementById('ImageUrl').value);
    formData.append('Name', document.getElementById('Name').value);
    formData.append('Description', document.getElementById('Description').value);
    formData.append('Price', document.getElementById('Price').value);
    formData.append('CategoryId', document.getElementById('CategoryId').value);
    formData.append('Id', document.getElementById('Id').value);
    var form = $("#myForm");
    form.validate();
    var isValid = form.valid();
    if (isValid) { 
    $.ajax({
        url: '/Admin/Product/AddUpdate/',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            // Render the partial view with the updated data.
            $('#Modal').modal('hide');
            //$.each(result, function (index, item) {
            //    // Access each item in the result
            //    console.log(index);
            //});
            if (result.isSuccessful) {
                toastr.success(result.message, 'Success', { positionClass: 'toast-top-right' }); // Show success toast on top-right position
                window.location.href = result.url;

            }
            else {
                toastr.error(result.message, 'Error', { positionClass: 'toast-top-right' }); // Show error toast on top-right position
                //$(".guard_form").html(result.url);

            }
        },
        error: function (xhr, status, error) {
            //$(".guard_form").html(response);
            toastr.clear(); // Clear submitting data toast
            toastr.error("An error occurred while submitting data.", 'Error', { positionClass: 'toast-top-right' }); // Show error toast on top-right position
        },
    });
    }
}


function deleteProduct(productId) {
    if (confirm("Are you sure you want to delete this product?")) {
        // Create an AJAX call to the controller action.
        $.ajax({
            url: '/Admin/Product/Delete/' + productId,
            type: 'post',
            success: function (result) {
                // Handle the success response.
                if (result.success) {
                    // Refresh the page or update the UI as needed.
                    location.reload(); // Example: Reload the page
                } else {
                    // Handle the error case.
                    //alert(result.message);
                }
            },
            error: function () {
                // Handle the AJAX error case.
                alert("An error occurred while deleting the product.");
            }
        });
    }
}





// Post Data
//function SubmitProduct() {
//    var formData = new FormData();
//    var fileInput = $('#file')[0]; // Replace 'fileInput' with the ID or selector of your file input element
//    var file = fileInput.files[0];

//    if (file) {
//        formData.append('File', file);
//    }

//    // Append other form data to formData if needed
//    formData.append('Name', $('#Name').val());
//    formData.append('Description', $('#Description').val());
//    formData.append('Price', $('#Price').val());
//    formData.append('CategoryId', $('#CategoryId').val());
//    console.log(formData);
///*    formData.append('File', file);*/
//    console.log(formData);
//    // Create an AJAX call to the controller action.
//    $.ajax({
//        url: '/Admin/Product/AddUpdate/',
//        type: 'post',
//        data: formData,
//        success: function (result) {
//            // Render the partial view with the updated data.
//            $('#Modal').modal('hide');
//            window.location.reload();
//        },
//        error: function (response) {
//            $(".guard_form").html(response);
//            alert(response)
//        }

//    });
//}