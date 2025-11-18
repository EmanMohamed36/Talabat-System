using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class DeliveryMethodDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }


        [Required(ErrorMessage = "DeliveryTime Required")]
        public string DeliveryTime { get; set; } = null!;

        [Required(ErrorMessage = "Price Required")]

        public decimal Price { get; set; }
    }
}
