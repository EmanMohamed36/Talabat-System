using DomainLayer.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class OrderController(IServiceManager _serviceManager):BaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDTO orderDTO)

        { 
            var result = await _serviceManager.OrderService.CreateOrderAsync(orderDTO, GetUserEmail());
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetAllDeliveryMethod()
        { 
            var result = await _serviceManager.OrderService.GetAllDeliveryMethodAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrdersToSpecificUser()
        { 
            var result = await _serviceManager.OrderService.GetAllOrdersToSpecificUserAsync(GetUserEmail());
            
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetOrderByIdToSpecificUser(Guid id)
        {
            var result = await _serviceManager.OrderService.GetOrderByIdToSpecificUserAsync(id,GetUserEmail());

            return Ok(result);
        }
    }
}
