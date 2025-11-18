using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOS.IdentityDTOs;
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
    public class AuthenticationController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        { 
            var result = await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(result);
        }
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        { 
            var result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]

        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(result);
        }
        [Authorize]

        [HttpPut("Address")]

        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.AuthenticationService.CreateOrUpdateCurrentUserAddressAsync(addressDTO,email!);
            return Ok(result);
        }
    }
}
