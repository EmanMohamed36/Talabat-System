using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QueryParams
{
    public class QueryProductParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string? SearchValue { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginated { get; set; }

    }
}
