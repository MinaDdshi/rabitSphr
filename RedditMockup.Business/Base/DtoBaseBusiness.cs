using AutoMapper;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.Base;

public class DtoBaseBusiness<TDto, TBase> : BaseBusiness<TBase>, IDtoBaseBusiness<TDto>
    where TBase : BaseEntity
    where TDto : class
{
    private readonly IMapper _mapper;

    #region [Constructor]

    public DtoBaseBusiness(IUnitOfWork unitOfWork, IBaseRepository<TBase> repository, IMapper mapper) : base(unitOfWork, repository) =>
        _mapper = mapper;

    #endregion

    #region [Methods]

    public virtual async Task<CustomResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var baseEntity = _mapper.Map<TBase>(dto);

        baseEntity = await CreateAsync(baseEntity, cancellationToken);

        if (baseEntity is null)
        {
            return new()
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError,
            };
        }

        var tDto = _mapper.Map<TDto>(baseEntity);

        return new()
        {
            Data = tDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.Created
        };
    }

    public new async Task<CustomResponse<TDto>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var baseEntity = await base.DeleteAsync(id, cancellationToken);

        if (baseEntity is null)
        {
            return new()
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var dto = _mapper.Map<TDto>(baseEntity);

        return new()
        {
            Data = dto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<IEnumerable<TDto>>> LoadAllAsync(SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var baseEntities = await base.LoadAllAsync(sieveModel, cancellationToken);

        var dtos = _mapper.Map<IEnumerable<TDto>>(baseEntities);

        return new()
        {
            Data = dtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<TDto>> LoadByIdAsync(int id, CancellationToken cancellationToken)
    {
        var baseEntity = await base.LoadByIdAsync(id, cancellationToken);

        if (baseEntity is null)
        {
            return new()
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var dto = _mapper.Map<TDto>(baseEntity);

        return new()
        {
            Data = dto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var baseEntity = _mapper.Map<TBase>(dto);

        baseEntity = await UpdateAsync(baseEntity, cancellationToken);

        if (baseEntity is null)
        {
            return new()
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError,
            };
        }

        var tDto = _mapper.Map<TDto>(baseEntity);

        return new()
        {
            Data = tDto,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}
