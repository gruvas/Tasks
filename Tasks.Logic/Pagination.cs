using Microsoft.AspNetCore.Http;
using System.Reflection;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;

namespace Tasks.Logic
{
    public class Pagination
    {
        public static (int, int, List<T>) GetPagedResult<T>(List<T> items, HttpRequest request)
        {
            int pageSize = 10;

            int pageCount = (int)Math.Ceiling((double)items.Count / pageSize);
            var pageCurrent = request.Query.ContainsKey("page") ? int.Parse(request.Query["page"]) : 1;
            var startIndex = (pageCurrent - 1) * pageSize;
            var displayedUsers = items.Skip(startIndex).Take(pageSize).ToList();

            return (pageCurrent, pageCount, displayedUsers);
        }
    }
}