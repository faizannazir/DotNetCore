        // Pagination key value pair changed working
        public async Task<KeyValuePair<int, List<CategoryDto>>> GetAllCategoriesServerSideGrid(Pagination pagination)
        {
            var query = _categoryRepository.GetReadOnlyList().Where(x => x.IsDeleted != true)
                .Include(x => x.Products).OrderByDescending(x => x.Id).AsQueryable();
            pagination.searchValue = string.IsNullOrEmpty(pagination.searchValue) ? null : pagination.searchValue;
            if (!string.IsNullOrEmpty(pagination.searchValue))
            {
                query = query.Where(x => x.Name.Contains(pagination.searchValue.Trim().ToLower()));
            }
            var dataQuery = query.Select(x => new CategoryDto
            {
                Name = x.Name,
                TotalOrders = x.TotalOrders,
                Id = x.Id
            });

            dataQuery = dataQuery.OrderBy($"{pagination.sortColumn} {pagination.sortColumnDirection}");  // using System.Linq.Dynamic.Core
            var totalRecord = dataQuery.Count();
            if (pagination.take > 0)
            {
                int count = 0;
                var PendingList = await dataQuery.Skip(pagination.skip).Take(pagination.take).ToListAsync();
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
            else
            {
                var PendingList = await dataQuery.Take(totalRecord).ToListAsync();
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
        }

// Services 
        // Pagination key value pair
        public async Task<KeyValuePair<int, List<CategoryDto>>> GetAllCategoriesServerSideGrid(Pagination pagination)
        {

            var query = _categoryRepository.GetReadOnlyList().Where(x => x.IsDeleted != true)
                .Include(x => x.Products).OrderByDescending(x => x.Id).AsQueryable();
            pagination.searchValue = string.IsNullOrEmpty(pagination.searchValue) ? null : pagination.searchValue;
            if (!string.IsNullOrEmpty(pagination.searchValue))
            {
                query = query.Where(x => x.Name.Contains(pagination.searchValue.Trim().ToLower()));
            }
            var dataQuery = query.Select(x => new CategoryDto
            {
                Name = x.Name,
                Id = x.Id
            });
            dataQuery = dataQuery.OrderBy(x=>pagination.sortColumn).ThenBy(x=>pagination.sortColumnDirection);
            //dataQuery = dataQuery.OrderBy($"{pagination.sortColumn} {pagination.sortColumnDirection}");      using System.Linq.Dynamic.Core
            var totalRecord = dataQuery.Count();
            if (pagination.take > 0)
            {
                int count = 0;
                var PendingList = await dataQuery.Skip(pagination.skip).Take(pagination.take).ToListAsync();
                //foreach (var item in PendingList)
                //{
                //    item.SerialNo = count + 1;
                //    //item.Id = pagination.skip + 1;
                //    pagination.skip++;
                //    count++;

                //    item.Action += "<a href=javascript:void(0) class='send_email btn btn-sm btn-edit-icon mr-1 text-uppercase text-white' data-CourseId= '" + item.Id + "' onclick=Edu_Student.EnrollCourseForaStudent(this) title='Enroll Course'><i class='fa-solid fa-book-medical'></i></a>";

                //}
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
            else
            {
                var PendingList = await dataQuery.Take(totalRecord).ToListAsync();
                foreach (var item in PendingList)
                {
                    //item.Id = pagination.skip + 1;
                    pagination.skip++;
                }
                return new KeyValuePair<int, List<CategoryDto>>(totalRecord, PendingList);
            }
        }


// Controller Example:
        public async Task<IActionResult> GetAllCategoriesForView()
        {
            var paginationParameter = PaginationParameter.Pagination(HttpContext);
            var data = await _categoryServices.GetAllCategoriesServerSideGrid(paginationParameter);
            return Json(new { draw = paginationParameter.draw, recordsFiltered = data.Key, recordsTotal = data.Key, data = data.Value });
        }


// Services
        public IQueryable<Category> GetAllCategoriesDatatable()
        {
            return _categoryRepository.GetAll();
            //    .Select(c => new Category
            //    {
            //    Name = c.Name,
            //    TotalOrders = c.TotalOrders
            //});
        }

//controller 

        public async Task<IActionResult> GetPaginatedCategory()
        {
            try
            {
                var pagniation =  PaginationParameter.Pagination(HttpContext);
                //var test = HttpContext.Request.Query["draw"];

                int recordsTotal = 0;
                var customerData = _categoryServices.GetAllCategoriesDatatable();
                if (!(string.IsNullOrEmpty(pagniation.sortColumn) && string.IsNullOrEmpty(pagniation.sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(pagniation.sortColumn + " " + pagniation.sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(pagniation.searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(pagniation.searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(pagniation.skip).Take(pagniation.take).ToList();
                var jsonData = new { draw = pagniation.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

// Same for both
//Pagination DTO

namespace DataTransferObject
{
    public class Pagination
    {
        public int draw { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string sortColumnDirection { get; set; }
        public string searchValue { get; set; }
        public string columnNum { get; set; }
        public string sortColumn { get; set; }
    }
}



// Common Code pagination
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
namespace Common
{
    public static class PaginationParameter
    {

        public static Pagination Pagination(this HttpContext httpContext)
        {
            var parameter = new Pagination
            {
                draw = int.Parse(httpContext.Request.Query["draw"]),
                skip = int.Parse(httpContext.Request.Query["start"]),
                take = int.Parse(httpContext.Request.Query["length"].ToString()),
                sortColumnDirection = httpContext.Request.Query["order[0][dir]"].ToString(),
                searchValue = httpContext.Request.Query["search[value]"].ToString(),
                columnNum = httpContext.Request.Query["order[0][column]"].ToString(),
                sortColumn = httpContext.Request.Query["columns[" + httpContext.Request.Query["order[0][column]"].ToString() + "][name]"].ToString()
            };
            return parameter;
        }

    }
}



// Index code 

<!-- Data Table Css -->
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.13.4/css/jquery.dataTables.min.css" />  

<!-- DataTable JavaScript -->
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js"></script>

    <script src="/js/CategoryDataTable.js" type="text/javascript"></script>

    <script>
        $(document).ready(function () {
            Categories.InitializeCategoryDataTable('#myTable', [
                { className: "text-center", data: 'name', name: "name" },
                { className: "text-center", data: 'totalOrders', name: "totalOrders", },
            ], "/Admin/Category/GetPaginatedCategory", 'GET', undefined, undefined, 1);
       //     ], "/Admin/Category/GetAllCategoriesForView", 'GET', undefined, undefined, 1);
        });
    </script>



// Js code 
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



// Simple client side 

<script>  
    $(document).ready(function () {  
        $('#myTable').DataTable();  
    });  
</script> 
   


   