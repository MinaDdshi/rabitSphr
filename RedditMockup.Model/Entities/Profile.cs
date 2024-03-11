using Sieve.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class Profile : BaseEntity
{
    #region [Properties]

    public string? Bio { get; set; } = string.Empty;

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Email { get; set; } = string.Empty;

    public int UserId { get; set; }

    #endregion

    #region [Navigation Properties]

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    #endregion
}