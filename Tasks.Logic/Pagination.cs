using Microsoft.AspNetCore.Http;

namespace Tasks.Logic
{
    public class Pagination
    {
        public static (int, int, int, int, IQueryable<T>) GetPagedResult<T>(IQueryable<T> source, HttpRequest request, int pageSize = 10)
        {
            var pageCurrent = request.Query.ContainsKey("page") ? int.Parse(request.Query["page"]) : 1;
            var pageCount = (int)Math.Ceiling((double)source.Count() / pageSize);
            var startIndex = (pageCurrent - 1) * pageSize;
            var displayedData = source.Skip(startIndex).Take(pageSize);

            return (pageCurrent, pageSize, pageCount, startIndex, displayedData);
        }
    }
}