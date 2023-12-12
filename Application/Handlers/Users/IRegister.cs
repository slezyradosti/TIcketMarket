using Application.Core;
using Application.DTOs.Users.DTOS;

namespace Application.Handlers.Logic
{
    public interface IRegister
    {
        public Task<AccountResult<UserDto, IEnumerable<string>>> RegisterHandlerAsync(RegisterDto registerDto);
    }
}
