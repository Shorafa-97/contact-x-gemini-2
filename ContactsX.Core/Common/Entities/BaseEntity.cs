using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsX.Domain.Common.Entities;

public class BaseEntity: ISoftDelete
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("created_by")]
    public int CreatedBy { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("updated_by")]
    public int UpdatedBy { get; set; }
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
