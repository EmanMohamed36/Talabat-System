using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.OrderDTOs
{
    public class OrderToReturnDTO
        
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Email Required")]
        public string BuyerEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; } 
       
        [Required(ErrorMessage = "Order Address Required")]
        public AddressDTO shipToAddress { get; set; } = null!;

        [Required(ErrorMessage = "Delivery Method Name Required")]
        public string DeliveryMethod { get; set; } = null!;
        

        [Required(ErrorMessage = "order Status Required")]

        public string Status { get; set; } = null!;



        public ICollection<OrderItemDTO> Items { get; set; } 

        public decimal SubTotal { get; set; }

      
        public decimal Total {  get; set; }

        public decimal? DeliveryCost { get; set; } // client side
    }
}
