 //$(document).ready(function () {
        //$('#myTable').DataTable(
        //    {
        //        ajax: {
        //            url: "Category/GetPaginatedCategory",
        //            type: "POST",
        //        },
        //        processing: true,
        //        serverSide: true,
        //        filter: true,
        //        //columns: [
        //        //    { data: "category", name: "Category" },
        //        //    { data: "totalOrders", name: "TotalOrders" },
        //        //    { data: "", name: "" },
        //        //]
        //    });
 //});
     var Categories = function () {
         return {
             InitializeCategoryDataTable(selector, columns, url, requestType = 'GET', requestData = undefined, GridLoadCallBack = undefined, orderNumber = 0) {
                 GridLoadCallBack = GridLoadCallBack || function (oSettings, json) { };
                 $(selector).DataTable({
                     "deferRender": false,
                     "processing": true,
                     "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
                     "serverSide": true,
                     "ajax": {
                         "url": url,
                         "type": requestType,
                         "data": requestData
                     },
                     "info": true,
                     "searching": true,
                     "paging": true,
                     "dom": 'B<"text-right mb-md"T><"row"<"col-lg-6 col-md-6 mt-1"l><"#importbtn.col-lg-6 col-md-6 mt-1"f>><"table-responsive"t>p',
                     "buttons": [
                         {
                             extend: 'excel'
                         },
                         {
                             extend: 'csv'
                         },
                     ],
                     "columns": columns,
                     "order": [[orderNumber, "desc"]],
                     "fnInitComplete": GridLoadCallBack
                 });
             },
             MaintainPaginationOnReload: function (tableId) {

                 myTable = $('#' + tableId).DataTable();
                 var previousPagination = ($("#" + tableId + "_wrapper .pagination-panel-input").val()) - 1;
                 myTable.columns.adjust().draw(false);
                 var doIdraw = false;
                 var recordLength = $(document).find("#" + tableId + " tbody tr .fa-trash-o").length;
                 if (recordLength - 1 == 0) {
                     doIdraw = true;
                 }
                 if (previousPagination > 1 && doIdraw) {
                     $("#" + tableId + "_paginate .pagination-panel").find(".fa-angle-left").click();
                 }
                 else if (previousPagination == 1 && doIdraw) {
                     myTable.columns.adjust().draw(doIdraw);
                 }
             }
         }
     }();


   
