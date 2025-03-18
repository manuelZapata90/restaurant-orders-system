using System;

namespace Repositories.Interfaces;

public interface IUserRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    public Task<bool> GetLoginAsync(TEntity id);
}