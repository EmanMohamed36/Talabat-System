using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginatedResult<DTO>
    {
        public PaginatedResult(int pageSize, int pageIndex, int totalCount, IEnumerable<DTO> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<DTO> Data { get; set; }
    }
}
