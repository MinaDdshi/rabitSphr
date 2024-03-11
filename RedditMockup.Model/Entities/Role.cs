using Sieve.Attributes;

namespace RedditMockup.Model.Entities;

public class Role : BaseEntity
{
    #region [Properties]

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Title { get; set; }

    public virtual ICollection<UserRole>? UserRoles { get; set; }

    #endregion
}