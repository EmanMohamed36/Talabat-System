using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Product Name Required")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "PictureUrl Required")]

        public string PictureUrl { get; set; } = null!;
    }
}
