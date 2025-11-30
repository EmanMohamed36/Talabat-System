using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions.NotFoundExceptions;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using ServiceAbstractionLayer;
using ServiceLayer.Specifications.OrderModuleSpecification;
using Shared.DTOS.BasketDTOs;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class PaymentService(IConfiguration _configuration ,
                                IUnitOfWork _unitOfWork 
                                ,IBasketRepository _basketRepository,
                                 IMapper _mapper) 
                                : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
        {
            //Configure Stripe -> install package stripe.net
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            
            //Get basket by id 
            var Basket = await _basketRepository.GetBasketAsync(basketId)
                                        ??throw new BasketNotFoundException(basketId);

            //Get Ammount
            var productRepo =  _unitOfWork.GetRepository<DomainLayer.Models.ProductModule.Product, int>();
            foreach (var item in Basket.Items)
            {
                var originalProduct = await productRepo.GetByIdAsync(item.Id)
                    ??throw new ProductNotFoundException(item.Id);
                item.Price = originalProduct.Price;
            }

            ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);
            var delieryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId!.Value)
                                            ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId!.Value);
            Basket.ShippingPrice = delieryMethod.Price;
            var BasketAmount = (long) (Basket.Items.Sum(p => p.Price * p.Quantity) + delieryMethod.Price) * 100;

            //Create Payment Intent Id 
            var paymentService = new PaymentIntentService();

            if (Basket.PaymentIntentId is null)//Create payment intent
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await paymentService.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
                
            }
            else // Update Payment Intent
            {
                var options = new PaymentIntentUpdateOptions() 
                {
                    Amount = BasketAmount
                };
                await paymentService.UpdateAsync(Basket.PaymentIntentId,options);
            }
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return _mapper.Map<BasketDTO>(Basket);
            
        }

        public async Task UpdatePaymentStatus(string jsonRequest, string stripeHeader)
        {
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                  stripeHeader, _configuration["StripeSettings:EndPointSecret"]);


            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentFailedAsync(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentReceivedAsync(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }
        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order.orderStatus = OrderStatus.PaymentReceived;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentSpecification(paymentIntentId));

            order.orderStatus = OrderStatus.PaymentFailed;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
