using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.ForbiddenExceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications.OrderModuleSpecification;
using Shared.DTOS.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class OrderService(IUnitOfWork _unitOfWork,
                                IMapper _mapper,
                                IBasketRepository _basketRepository
                                
                               ) 
        : IOrderService
    {
        public async Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO ,string email)

        {
            //Basket Id , Address DTO , DeliveryMethodId customer email
            var orderAddress = _mapper.Map<ShippingAddress>(orderDTO.ShipToAddress);


            var basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId)
                                                ?? throw new BasketNotFoundException(orderDTO.BasketId);

            ArgumentNullException.ThrowIfNull(basket.PaymentIntentId);

            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
            var ExistingOrder =  await OrderRepo.GetByIdAsync(OrderSpec);

            if (ExistingOrder is not null) OrderRepo.Remove(ExistingOrder);

            var ProductRepo =  _unitOfWork.GetRepository<Product, int>();
            List<OrderItem> OrderItems = [];
            foreach (var BasketItem in basket.Items)
            {
                var OriginalProduct = await ProductRepo.GetByIdAsync(BasketItem.Id)
                                ?? throw new ProductNotFoundException(BasketItem.Id);
                var Item = new OrderItem()
                {

                    Price = OriginalProduct.Price,
                    productItemOrdered = new ProductItemOrdered()
                    {
                        ProductId = OriginalProduct.Id,
                        PictureUrl = OriginalProduct.PictureUrl,
                        ProductName = OriginalProduct.Name,
                    },
                    Quantity = BasketItem.Quantity
                };
                OrderItems.Add(Item);
            }
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                                  .GetByIdAsync(orderDTO.DeliveryMethodId)
                                                  ??throw new DeliveryMethodNotFoundException(orderDTO.DeliveryMethodId);

            var subTotal = OrderItems.Sum(i => i.Quantity * i.Price);

            var order = new Order(email,orderAddress, DeliveryMethod,orderDTO.DeliveryMethodId,OrderItems, subTotal,basket.PaymentIntentId);

            await OrderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OrderToReturnDTO>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDTO>> GetAllDeliveryMethodAsync()
        { 
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDTO>>(deliveryMethod);
        }

        public async Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersToSpecificUserAsync(string email)
        {
            
            var spec = new OrderSpecification(email);
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);

            return _mapper.Map<IEnumerable<OrderToReturnDTO>>(orders);
        }

        public async Task<OrderToReturnDTO> GetOrderByIdToSpecificUserAsync(Guid orderId, string email)
        {
            var spec = new OrderSpecification(orderId);
            var order = await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(spec)
                        ?? throw new OrderNotFoundException(orderId);

            if (order.UserEmail != email) throw new GetOrderByIdException(email);

            return _mapper.Map<OrderToReturnDTO>(order);
        }
    }
}
