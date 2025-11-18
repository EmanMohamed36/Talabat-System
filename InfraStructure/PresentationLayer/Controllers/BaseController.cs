using DomainLayer.Exceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class BaseController:ControllerBase
    {
        protected string GetUserEmail()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email) 
                        ?? throw new EmailNotFoundException();
            return Email;
        }
    }
}
