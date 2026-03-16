using ContactsX.Domain.Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsX.Domain.Entities;

public class AuditLog : BaseEntity
{
    [Column("entity_type")]
    public string EntityType { get; set; } = string.Empty;

    [Column("entity_id")]
    public Guid EntityId { get; set; }

    [Column("action")]
    public string Action { get; set; } = string.Empty;

    [Column("changes", TypeName = "jsonb")]
    public string Changes { get; set; } = "{}";

    [Column("performed_by")]
    public Guid? PerformedBy { get; set; }
}
