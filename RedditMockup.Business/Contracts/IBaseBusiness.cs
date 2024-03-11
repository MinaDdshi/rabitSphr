using Microsoft.EntityFrameworkCore.Query;
using Sieve.Models;

namespace RedditMockup.Business.Contracts;

public interface IBaseBusiness<T>
{
    Task<T?> CreateAsync(T t, CancellationToken cancellationToken);

    Task<T?> LoadByIdAsync(int id, CancellationToken cancellationToken, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null);

    Task<IEnumerable<T>?> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken, Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null);

    Task<T?> UpdateAsync(T t, CancellationToken cancellationToken);

    Task<T?> DeleteAsync(int id, CancellationToken cancellationToken);
}