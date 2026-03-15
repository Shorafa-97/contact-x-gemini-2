namespace ContactsX.Application.DTOs.Relation;

public class RelationDto
{
    public Guid Id { get; set; }
    public Guid ContactId { get; set; }
    public Guid EntityId { get; set; }
    public string Role { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public bool IsActive { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
