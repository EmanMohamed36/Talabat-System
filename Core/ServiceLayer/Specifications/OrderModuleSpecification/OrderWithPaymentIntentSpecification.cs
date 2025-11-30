using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Specifications.OrderModuleSpecification
{
    public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId) 
            : base(o =>o.PaymentIntentId == paymentIntentId )
        {

        }
    }
}
