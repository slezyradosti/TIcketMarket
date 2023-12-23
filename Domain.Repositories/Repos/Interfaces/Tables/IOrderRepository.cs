using Domain.Models.Tables;

namespace Domain.Repositories.Repos.Interfaces.Tables;

public interface IOrderRepository : IRepository<Order>
{
    public Task<List<Order>> GetCustomersListSortedAsync(Guid userId);
    public Task<bool> HasUserAccessToTheOrderAsync(Guid orderId, Guid userId);
}