using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public class DeliveryMethod:BaseEntity<int>
    {
        
        [Required(ErrorMessage ="Name Required")]
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }


        [Required(ErrorMessage = "DeliveryTime Required")]
        public string DeliveryTime { get; set; } = null!;
        
        [Required(ErrorMessage = "Cost Required")]

        public decimal Price { get; set; }

    }
}
