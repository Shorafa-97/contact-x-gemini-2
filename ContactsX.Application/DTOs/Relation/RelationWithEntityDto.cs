using ContactsX.Application.DTOs.Entity;

namespace ContactsX.Application.DTOs.Relation;

public class RelationWithEntityDto : RelationDto
{
    public EntityDto? Entity { get; set; }
}
