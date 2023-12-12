using Application.Core;
using Application.DTOs.Users.DTOS;
using Application.Services;
using Domain.Models.Tables;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Handlers.Logic
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public UserHandler(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AccountResult<UserDto, string>> GetCurrentUserAsync(ClaimsPrincipal claimsUser)
        {
            var user = await _userManager.FindByEmailAsync(claimsUser.FindFirstValue(ClaimTypes.Email));

            if (user != null)
            {
                var userDto = await CreateUserDto(user);

                return new AccountResult<UserDto, string>(userDto, true);
            }

            return new AccountResult<UserDto, string>(isSuccessful: false,
                errors: "Failed to find the user");
        }

        public async Task AddIdClaimAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "UserId"))
            {
                await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id.ToString().ToLower()));
            }
        }

        public async Task AddSellerIdClaimAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "SellerId"))
            {
                await _userManager.AddClaimAsync(user, new Claim("SellerId", user.Id.ToString().ToUpper()));
            }
        }

        public async Task AddCustomerIdClaimAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "CustomerId"))
            {
                await _userManager.AddClaimAsync(user, new Claim("CustomerId", user.Id.ToString().ToUpper()));
            }
        }

        public async Task<UserDto> CreateUserDto(ApplicationUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Phone = user.Phone,
                DOB = user.DOB,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
