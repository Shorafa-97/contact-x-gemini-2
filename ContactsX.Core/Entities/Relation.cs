using ContactsX.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsX.Domain.Entities;

[Table("relations")]
public class Relation : BaseEntity
{
    [Column("contact_id")]
    [Required]
    public Guid ContactId { get; set; }
    
    [ForeignKey(nameof(ContactId))]
    public Contact? Contact { get; set; }
    
    [Column("entity_id")]
    [Required]
    public Guid EntityId { get; set; }
    
    [ForeignKey(nameof(EntityId))]
    public Entity? Entity { get; set; }
    
    [Column("role")]
    [Required]
    public string Role { get; set; } = string.Empty;

    [Column("is_primary")]
    public bool IsPrimary { get; set; } = false;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("start_date")]
    public string? StartDate { get; set; }

    [Column("end_date")]
    public string? EndDate { get; set; }
}
