namespace ContactsX.Application.DTOs.Relation;

public class CreateRelationDto
{
    public Guid TargetEntityId { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}
