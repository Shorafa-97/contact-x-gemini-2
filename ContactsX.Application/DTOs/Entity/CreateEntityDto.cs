using ContactsX.Domain.ValueOpjects;

namespace ContactsX.Application.DTOs.Entity;

public class CreateEntityDto
{
    public string NameEn { get; set; } = string.Empty;
    public string? NameAr { get; set; }
    public EntityType Type { get; set; }
    public string? Country { get; set; }
    public string? Sector { get; set; }
    public string? RegistrationId { get; set; }
    public Guid? ParentEntityId { get; set; }
    public string Addresses { get; set; } = "[]";
    public string ContactPoints { get; set; } = "[]";
}