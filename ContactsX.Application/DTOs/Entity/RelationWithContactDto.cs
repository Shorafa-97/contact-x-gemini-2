using ContactsX.Application.DTOs.Contact;
using ContactsX.Application.DTOs.Relation;
using ContactsX.Application.DTOs.Shared;


namespace ContactsX.Application.DTOs.Entity;

public class RelationWithContactDto : RelationDto
{
    public ContactDto? Contact { get; set; }
}
