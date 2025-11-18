using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using DomainLayer.Models.BasketModels;
using ServiceAbstractionLayer;
using Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    internal class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var createOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);
            if (createOrUpdateBasket is not null) return basket;
            else  throw new Exception("Can not update or create basket,Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string key)
            => await _basketRepository.DeleteBasketAsync(key);

           

        public async Task<BasketDTO> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);
            if (basket is not null) return _mapper.Map<BasketDTO>(basket);
            else throw new BasketNotFoundException(key);
        }
    }
}
