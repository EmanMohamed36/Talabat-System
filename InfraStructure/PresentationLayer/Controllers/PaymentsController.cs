using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager):BaseController
    {
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        { 
            var res = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(res);
        }

        //stripe listen --forward-to https://localhost:7036/api/Payments/webhook

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            //Logic
            await _serviceManager.PaymentService.UpdatePaymentStatus(json,
                Request.Headers["Stripe-Signature"]);

            return new EmptyResult();


        }
    }
}
