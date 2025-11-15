using Shared.DTOS.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IAuthenticationService
    {
        // LogIn(email,password) -> displayname,email,token 

        public Task<UserDTO> LoginAsync(LoginDTO loginDTO);


        //Register() ->displayname,email,token

        public Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);

        //Check Email
        public Task<bool> CheckEmailAsync(string email);

        //Get Current User Address

        public Task<AddressDTO> GetCurrentUserAddressAsync(string email);
        public Task<UserDTO> GetCurrentUserAsync(string email);


        // Create or Update Current User Address
        public Task<AddressDTO> CreateOrUpdateCurrentUserAddressAsync(AddressDTO addressDTO, string email);
    }
}
