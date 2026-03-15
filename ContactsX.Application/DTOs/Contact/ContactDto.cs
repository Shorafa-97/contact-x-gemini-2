using ContactsX.Domain.ValueOpjects;
using ContactsX.Application.DTOs.Shared;

namespace ContactsX.Application.DTOs.Contact;

public class ContactDto
{
    public Guid Id { get; set; }
    public string? Prefix { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Suffix { get; set; }
    public string? PrefixAr { get; set; }
    public string? FirstNameAr { get; set; }
    public string? MiddleNameAr { get; set; }
    public string? LastNameAr { get; set; }
    public string? SuffixAr { get; set; }
    public Gender? Gender { get; set; }
    public string? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? NationalId { get; set; }
    public ContactType ContactType { get; set; }
    public string? CurrentPosition { get; set; }
    public Guid? CurrentEntityId { get; set; }
    public string? CurrentEntityName { get; set; }
    public List<EmailDto>? Emails { get; set; }
    public List<PhoneDto>? Phones { get; set; }
    public List<AddressDto>? Addresses { get; set; }
    public List<string>? Classifications { get; set; }
    public int ProfileCompleteness { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
