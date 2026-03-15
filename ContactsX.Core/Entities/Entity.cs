using ContactsX.Domain.Common.Entities;
using ContactsX.Domain.ValueOpjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsX.Domain.Entities;

public class Entity:BaseEntity
{
    [Required]
    [Column("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [Column("name_ar")]
    public string? NameAr { get; set; }

    [Required]
    [Column("entity_type")]
    public EntityType Type { get; set; } = EntityType.Public;

    [Column("country")]
    public string? Country { get; set; }

    [Column("sector")]
    public string? Sector { get; set; }

    [Column("registration_id")]
    public string? RegistrationId { get; set; }

    [Column("parent_entity_id")]
    public Guid? ParentEntityId { get; set; }
    [ForeignKey("ParentEntityId")]
    public Entity? ParentEntity { get; set; }

    [Column("addresses", TypeName = "jsonb")]
    public string Addresses { get; set; } = "[]";

    [Column("contact_points", TypeName = "jsonb")]
    public string ContactPoints { get; set; } = "[]";

    [Column("profile_completeness")]
    public int ProfileCompleteness { get; set; } = 0;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;


}
