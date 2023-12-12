using Application.Core;
using Application.DTOs.Users.DTOS;
using Application.Services;
using Domain.Models.Tables;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers.Logic
{
    public class Register : IRegister
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly IUserHandler _userHandler;

        public Register(UserManager<ApplicationUser> userManager,
            TokenService tokenService, IUserHandler userHandler)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userHandler = userHandler;
        }

        public async Task<AccountResult<UserDto, IEnumerable<string>>> RegisterHandlerAsync(RegisterDto registerDto)
        {
            if (registerDto == null) return null;

            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Firstname = registerDto.Firstname,
                Lastname = registerDto.Lastname,
                Phone = registerDto.Phone,
                DOB = registerDto.DOB,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                //addign claim to user for easier access to his Id
                await _userHandler.AddIdClaimAsync(user.Email);

                // add claim Customer or Seller
                if (!registerDto.isCustomer) await _userHandler.AddCustomerIdClaimAsync(user.Email);
                else await _userHandler.AddSellerIdClaimAsync(user.Email);

                var userDto = await _userHandler.CreateUserDto(user);

                return new AccountResult<UserDto, IEnumerable<string>>(userDto, true);
            }

            return new AccountResult<UserDto, IEnumerable<string>>(isSuccessful: false,
                errors: result.Errors.Select(x => x.Description).ToList());
        }
    }
}
