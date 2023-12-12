using Application.Core;
using Application.DTOs.Users.DTOS;

namespace Application.Handlers.Logic
{
    public interface ILogin
    {
        public Task<AccountResult<UserDto, string>> LoginHandleAsync(LoginDto loginDto);
    }
}
