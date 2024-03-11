using Sieve.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities
{
    public class QuestionVote : BaseEntity
    {
        #region [Properties]

        [Sieve(CanSort = true)]
        public bool Kind { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question? Question { get; set; }

        #endregion

    }
}
