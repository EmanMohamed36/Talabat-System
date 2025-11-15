using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTOs
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        [Range(1,int.MaxValue)]
        public Decimal Price { get; set; }
        [Range(1, int.MaxValue)]

        public int Quantity { get; set; }
    }
}
