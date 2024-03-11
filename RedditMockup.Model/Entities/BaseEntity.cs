using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedditMockup.Model.Entities;

public class BaseEntity
{
    #region [Constructor]

    public BaseEntity() => CreationDate = LastUpdated = DateTime.Now;

    #endregion

    #region [Properties]

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Sieve(CanFilter = true, CanSort = true)]
    public int Id { get; set; }

    [Sieve(CanSort = true)]
    public DateTime CreationDate { get; set; }

    [Sieve(CanSort = true)]
    public DateTime LastUpdated { get; set; }

    #endregion
}