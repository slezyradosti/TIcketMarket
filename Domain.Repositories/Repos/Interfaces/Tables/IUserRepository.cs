using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables;

public interface IUserRepository
{
    Task<int> SaveAsync(ApplicationUser user);
    public Task<ApplicationUser> GetUser(Guid userId);
}