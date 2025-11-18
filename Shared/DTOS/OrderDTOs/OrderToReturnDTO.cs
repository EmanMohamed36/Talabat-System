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
        public string UserEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; } 
       
        [Required(ErrorMessage = "Order Address Required")]
        public AddressDTO OrderAddress { get; set; } = null!;

        [Required(ErrorMessage = "Delivery Method Name Required")]
        public string DeliveryMethodName { get; set; } = null!;
        

        [Required(ErrorMessage = "order Status Required")]

        public string orderStatus { get; set; } = null!;



        public ICollection<OrderItemDTO> Items { get; set; } 

        public decimal SubTotal { get; set; }

      
        public decimal Total {  get; set; }
    }
}
