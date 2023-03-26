using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tasks.Logic
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }
        public int PageCurrent { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }

        public Pagination(List<T> data, HttpRequest request, int pageSize = 10)
        {
            Data = data;
            PageSize = pageSize;
            TotalCount = data.Count();

            PageCurrent = request.Query.ContainsKey("page") ? Convert.ToInt32(request.Query["page"]) : 1;

            PageCount = (int)Math.Ceiling((double)TotalCount / PageSize);

            if (PageCurrent > PageCount)
            {
                PageCurrent = PageCount;
            }
            else if (PageCurrent < 1)
            {
                PageCurrent = 1;
            }

            Data = data.Skip((PageCurrent - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}