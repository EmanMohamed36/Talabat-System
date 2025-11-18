using DomainLayer.Models.OrderModels;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IOrderService
    {
        public Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO order, string email);
        public Task<IEnumerable<DeliveryMethodDTO>> GetAllDeliveryMethodAsync();
        public Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersToSpecificUserAsync(string email);
        public Task<OrderToReturnDTO> GetOrderByIdToSpecificUserAsync(Guid orderId, string email);
    }
}
