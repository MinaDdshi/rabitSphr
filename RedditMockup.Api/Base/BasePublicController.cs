using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("PublicApi/[controller]")]
public class PublicBaseController<TDto> : ControllerBase
    where TDto : class
{
    #region [Fields]

    private readonly IDtoBaseBusiness<TDto> _dtoBaseBusiness;

    #endregion

    #region [Constructor]

    public PublicBaseController(IDtoBaseBusiness<TDto> dtoBaseBusiness)
    {
        _dtoBaseBusiness = dtoBaseBusiness;
    }

    #endregion

    #region [Methods]

    [HttpGet]
    public async Task<CustomResponse<IEnumerable<TDto>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _dtoBaseBusiness.LoadAllAsync(sieveModel, cancellationToken);

    [Route("id")]
    [HttpGet]
    public async Task<CustomResponse<TDto>> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _dtoBaseBusiness.LoadByIdAsync(id, cancellationToken);

    [Authorize]
    [HttpPost]
    public async Task<CustomResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken) =>
        await _dtoBaseBusiness.CreateAsync(dto, cancellationToken);

    [Authorize]
    [HttpDelete]
    public async Task<CustomResponse<TDto>> DeleteAsync(int id, CancellationToken cancellationToken) =>
        await _dtoBaseBusiness.DeleteAsync(id, cancellationToken);

    [Authorize]
    [HttpPut]
    public async Task<CustomResponse<TDto>> UpdateAsync(TDto dto, CancellationToken
    cancellationToken) =>
        await _dtoBaseBusiness.UpdateAsync(dto, cancellationToken);

    [HttpOptions]
    public void Options() =>
      Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

    #endregion
}