using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Business.Contracts;

public interface IDtoBaseBusiness<TDto>
{
    Task<CustomResponse<TDto>> CreateAsync(TDto t, CancellationToken cancellationToken);

    Task<CustomResponse<TDto>> LoadByIdAsync(int id, CancellationToken cancellationToken);

    Task<CustomResponse<IEnumerable<TDto>>> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken);

    Task<CustomResponse<TDto>> UpdateAsync(TDto t, CancellationToken cancellationToken);

    Task<CustomResponse<TDto>> DeleteAsync(int id, CancellationToken cancellationToken);
}

