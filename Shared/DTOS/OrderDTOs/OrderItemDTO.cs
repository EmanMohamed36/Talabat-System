using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class OrderItemDTO
    {

        [Required(ErrorMessage = "Product Name Required")]
        public string ProductName { get; set; } = null!;

        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price Required")]
        public decimal Price { get; set; }// Price per unit.

        [Required(ErrorMessage = "Quantity Required")]
        public int Quantity { get; set; }//Number of units ordered.
    }
}
