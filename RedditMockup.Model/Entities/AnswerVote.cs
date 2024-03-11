using Sieve.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class AnswerVote : BaseEntity
{
    #region [Properties]

    [Sieve(CanSort = true)]
    public bool Kind { get; set; }

    public int AnswerId { get; set; }

    [ForeignKey("AnswerId")]
    public virtual Answer? Answer { get; set; }

    #endregion
}
