using Sieve.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class Question : BaseEntity
{
    #region [Properties]

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Title { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Description { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int? UserId { get; set; }

    #endregion

    #region [Navigation Properties]

    public virtual ICollection<Answer>? Answers { get; set; }

    public virtual ICollection<QuestionVote>? Votes { get; set; }

    public virtual ICollection<Bookmark>? Bookmarks { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

    #endregion
}