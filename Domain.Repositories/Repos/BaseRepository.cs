using System.Data.Entity.Infrastructure;
using Domain.Models.Base;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace Domain.Repositories.Repos;

public class BaseRepository<T> : IDisposable, IRepository<T> where T : BaseModel, new()
{
    private readonly DbSet<T> _table;

    public BaseRepository() : this(new DataContext())
    {
    }

    public BaseRepository(DataContext context)
    {
        Context = context;
        _table = Context.Set<T>();
    }

    protected DataContext Context { get; }

    public void Dispose()
    {
        Context?.Dispose();
    }

    public async Task<int> AddAsync(T entity)
    {
        await _table.AddAsync(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> AddRangeAsync(IList<T> entities)
    {
        await _table.AddRangeAsync(entities);
        return await SaveChangesAsync();
    }

    public async Task<int> DeleteAsStateAsync(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Context.Entry(new T { Id = id, CreatedAt = createdAt, UpdatedAt = updatedAt }).State = EntityState.Deleted;
        return await SaveChangesAsync();
    }

    public async Task<int> DeleteAsStateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        return await SaveChangesAsync();
    }

    public async Task<List<T>> ExecuteQueryAsync(string sqlQuery)
    {
        return await _table.FromSqlRaw(sqlQuery).ToListAsync();
    }

    public async Task<List<T>> ExecuteQueryAsync(string sqlQuery, object[] sqlParametersObjects)
    {
        return await _table.FromSqlRaw(sqlQuery, sqlParametersObjects).ToListAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _table.ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _table.CountAsync();
    }

    public async Task<T> GetOneAsync(Guid? id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<int> RemoveAsync(T entity)
    {
        Context.Remove(entity);
        return await SaveChangesAsync();
    }

    public async Task<int> RemoveAsync(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Context.Remove(new T { Id = id, CreatedAt = createdAt, UpdatedAt = updatedAt });
        return await SaveChangesAsync();
    }

    public async Task<int> SaveAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return await SaveChangesAsync();
    }

    internal async Task<int> SaveChangesAsync()
    {
        try
        {
            return await Context.SaveChangesAsync();
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