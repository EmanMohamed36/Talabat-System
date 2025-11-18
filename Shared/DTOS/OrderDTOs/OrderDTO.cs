using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class OrderDTO
    {
        [Required(ErrorMessage = ("BasketId Required"))]

        public string BasketId { get; set; } = null!;
        [Required(ErrorMessage =("Shipping Address Required"))]
        public AddressDTO ShippingAddressDTO { get; set; } = null!;
       

        public int DeliveryMethodId { get; set; }
        //[Required(ErrorMessage = ("Customer Email Required"))]
        //[EmailAddress]
        //public string CustomerEmail { get; set; } = null!;
    }
}
