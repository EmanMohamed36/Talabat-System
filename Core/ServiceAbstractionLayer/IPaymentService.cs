using Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IPaymentService
    {
        public Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);
        public Task UpdatePaymentStatus(string jsonRequest, string stripeHeader);

    }
}
