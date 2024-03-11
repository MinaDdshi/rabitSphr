using Microsoft.EntityFrameworkCore.Query;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.DataAccess.Contracts;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T t, CancellationToken cancellationToken);

    Task<List<T>> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default, Func<IQueryable<T>,IIncludableQueryable<T, object?>>? include = null);

    Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken = default, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null);

    T Update(T t);

    T Delete(T t);
}