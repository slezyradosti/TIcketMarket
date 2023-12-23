using System.Data.Entity.Infrastructure;
using Domain.Models.Tables;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces.Tables;
using Microsoft.EntityFrameworkCore;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace Domain.Repositories.Repos.Tables;

public class UserRepository : IDisposable, IUserRepository
{
    private readonly DataContext _dbContext;
    private readonly DbSet<ApplicationUser> _userTable;

    public UserRepository() : this(new DataContext())
    {
    }

    public UserRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
        _userTable = dbContext.Set<ApplicationUser>();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public async Task<ApplicationUser> GetUser(Guid userId)
    {
        return await _userTable.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<int> SaveAsync(ApplicationUser user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        return await SaveChangesAsync();
    }

    internal async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            //Thrown when there is a concurrency error
            //for now, just rethrow the exception
            throw ex;
        }
        catch (DbUpdateException ex)
        {
            //Thrown when database update fails
            //Examine the inner exception(s) for additional 
            //details and affected objects
            //for now, just rethrow the exception
            throw ex;
        }
        catch (CommitFailedException ex)
        {
            //handle transaction failures here
            //for now, just rethrow the exception
            throw ex;
        }
        catch (Exception ex)
        {
            //some other exception happened and should be handled
            throw ex;
        }
    }
}