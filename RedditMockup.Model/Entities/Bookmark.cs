using Sieve.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class Bookmark : BaseEntity
{
    public bool IsBookmarked { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int UserId { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int QuestionId { get; set; }

    #region [Navigation Properties]

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    [ForeignKey("QuestionId")]
    public virtual Question? Question { get; set; }

    #endregion

}
