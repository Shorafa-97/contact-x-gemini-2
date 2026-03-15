using ContactsX.Domain.ValueOpjects;
using ContactsX.Application.DTOs.Shared;

namespace ContactsX.Application.DTOs.Entity;

public class EntityDto
{
    public Guid Id { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string? NameAr { get; set; }
    public EntityType Type { get; set; }
    public string? Country { get; set; }
    public string? Sector { get; set; }
    public string? RegistrationId { get; set; }
    public Guid? ParentEntityId { get; set; }
    public List<AddressDto>? Addresses { get; set; }
    public List<ContactPointDto>? ContactPoints { get; set; }
    public int ProfileCompleteness { get; set; }
    public bool IsActive { get; set; }
}

public class ContactPointDto
{
    public string Type { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
