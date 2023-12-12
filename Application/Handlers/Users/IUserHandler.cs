using Application.Core;
using Application.DTOs.Users.DTOS;
using Domain.Models.Tables;
using System.Security.Claims;

namespace Application.Handlers.Logic
{
    public interface IUserHandler
    {
        public Task<AccountResult<UserDto, string>> GetCurrentUserAsync(ClaimsPrincipal claimsUser);
        public Task AddIdClaimAsync(string email);
        public Task AddSellerIdClaimAsync(string email);
        public Task AddCustomerIdClaimAsync(string email);
        public Task<UserDto> CreateUserDto(ApplicationUser user);
    }
}
