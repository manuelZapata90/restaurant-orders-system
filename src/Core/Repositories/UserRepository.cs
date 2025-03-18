using Repositories.Interfaces;

namespace Repositories;

public class UserRepository<TEntity, TKey> : IUserRepository<TEntity, TKey> where TEntity:class
{
    public Task AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetLoginAsync(TEntity id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
