using Sieve.Models;

namespace RedditMockup.Api.Contracts;

public interface IBaseController<T>
{
    Task<T?> CreateAsync(T t, CancellationToken cancellationToken);

    Task<IEnumerable<T>?> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<T?> UpdateAsync(T t, CancellationToken cancellationToken);

    Task<T?> DeleteAsync(int id, CancellationToken cancellationToken);

    void Options();
}