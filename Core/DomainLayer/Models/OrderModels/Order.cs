using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        { 
        
        }
        public Order(string userEmail, ShippingAddress orderAddress, DeliveryMethod deliveryMethod, int deliveryMethodId, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;

            OrderAddress = orderAddress;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethodId;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        [Required(ErrorMessage ="User Email Required")]
        public string UserEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus orderStatus { get; set; }

        public ShippingAddress OrderAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }

        public ICollection<OrderItem> Items { get; set; } = [];

        public decimal SubTotal { get; set; }

        [NotMapped]
        public decimal  Total { get => SubTotal + DeliveryMethod.Price; }
        public string PaymentIntentId { get; set; }

    }
}
