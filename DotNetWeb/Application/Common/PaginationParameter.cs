using DataTransferObject;
using Microsoft.AspNetCore.Http;
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
