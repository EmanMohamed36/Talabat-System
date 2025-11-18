using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.NotFoundExceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstractionLayer;
using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,
                                       IConfiguration _configuration,IMapper _mapper ) 
        : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            // check email exist or not
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null) throw new UserNotFoundException(loginDTO.Email);

            // password correct or not
            var isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            // return userDto

            if (isCorrectPassword)
            {
                return new UserDTO()
                {
                    Email = loginDTO.Email,
                    DisplayName = user.DisplayName,
                    Token = await CreateTokenAsync(user) // to do 
                };
            }
            else throw new UnauthorizedException();

        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var isEmailExist = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (isEmailExist is not null) throw new EmailOrPhoneAlreadyExistException("Email");

            var isPhoneExist = await _userManager.Users.Include(u => u.PhoneNumber == registerDTO.PhoneNumber)
                                                  .FirstOrDefaultAsync();
            if(isPhoneExist is not null) throw new EmailOrPhoneAlreadyExistException("Phone");
            var user = new ApplicationUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName,

            };
            var res = await _userManager.CreateAsync(user, registerDTO.Password);
            if (res.Succeeded)
            {
                return new UserDTO()
                {
                    Email = registerDTO.Email,
                    DisplayName = registerDTO.DisplayName,
                    Token = await CreateTokenAsync(user)
                };

            }
            else
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }

        }
            
       public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }
        public async Task<UserDTO> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) 
                                     ?? throw new UserNotFoundException(email);
            return new UserDTO()
            {
                Email = user.Email!,
                Token = await CreateTokenAsync(user),
                DisplayName = user.DisplayName,
            };
        }
        public async Task<AddressDTO> CreateOrUpdateCurrentUserAddressAsync(AddressDTO addressDTO, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u=> u.Email == email)??
                                               throw new UserNotFoundException(email);
            if (user.Address is not null)//update
            {
                user.Address = new Address()
                {
                    FirstName = addressDTO.FirstName,
                    LastName = addressDTO.LastName,
                    Street = addressDTO.Street,
                    City = addressDTO.City,
                    Country = addressDTO.Country,

                };
            }
            else //create
            { 
                user.Address = _mapper.Map<Address>(addressDTO);
            }
            var res =  await _userManager.UpdateAsync(user);
            if (res.Succeeded)
            {
                return addressDTO;
            }
            else
            {
                var errors = res.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
                
            }
               
        }
        public async Task<AddressDTO> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                          .FirstOrDefaultAsync()??
                                          throw new UserNotFoundException(email);
          
            if (user.Address is null) throw new AddressNotFoundException(user.UserName!);
            var addressDTO = _mapper.Map<AddressDTO>(user.Address);
            return addressDTO;    
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            // Header
            var secretKey = _configuration["JWTOptions:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //payload -> claims
            var payload = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                payload.Add(new Claim(ClaimTypes.Role,role));
            }

            // signature

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audiance"],
                claims : payload,
                signingCredentials : creds,
                expires:DateTime.Now.AddHours(1)

                );
            return new JwtSecurityTokenHandler().WriteToken(token);
         
        }

        
    }
}
