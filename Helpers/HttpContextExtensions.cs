using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace PeliculasAPI.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamsPagination<T>(this HttpContext httpContext, IQueryable<T> queryable, int recordsPerPage)
        {
            double quantity = await queryable.CountAsync();
            double quantityPages = Math.Ceiling(quantity / recordsPerPage);
            httpContext.Response.Headers.Add("quantityPages",quantityPages.ToString());
        }
    }
}
