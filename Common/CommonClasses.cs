using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationWithAuth.Common
{
    public class GetEnrolleesQueryObject
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; }
        public string FilterName { get; set; }
    }

    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
    }
}
