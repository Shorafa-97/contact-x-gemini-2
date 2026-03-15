namespace ContactsX.Domain.Common.Entities;  

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
