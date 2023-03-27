using Microsoft.AspNetCore.Http;
using System.Reflection;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;

namespace Tasks.Logic
{
    public class Pagination
    {
        public static (int, int, List<User>) GetPagedResult(List<User> users, HttpRequest request)
        {
            int pageSize = 10;

            int pageCount = (int)Math.Ceiling((double)users.Count / pageSize);
            var pageCurrent = request.Query.ContainsKey("page") ? int.Parse(request.Query["page"]) : 1;
            var startIndex = (pageCurrent - 1) * pageSize;
            var displayedUsers = users.Skip(startIndex).Take(pageSize).ToList();

            return (pageCurrent, pageCount, displayedUsers);
        }
    }
}