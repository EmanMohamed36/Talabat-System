using Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string key);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);

        Task<bool> DeleteBasketAsync(string key);

    }
}
