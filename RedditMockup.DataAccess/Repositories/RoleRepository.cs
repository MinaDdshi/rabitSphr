using Microsoft.EntityFrameworkCore;
using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class RoleRepository : BaseRepository<Role>
{
    #region [Fields]

    private readonly RedditMockupContext _context;

    #endregion

    #region [Constructor]

    public RoleRepository(RedditMockupContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) =>
        _context = context;

    #endregion

    #region [Methods]

    public async Task<List<Role?>> LoadByUserIdAsync(int userId, CancellationToken cancellationToken = default) =>
        await _context.UserRoles!
            .Where(x => x.UserId == userId)
            .Include(x => x.Role)
            .Select(x => x.Role)
            .ToListAsync(cancellationToken);

    #endregion
}