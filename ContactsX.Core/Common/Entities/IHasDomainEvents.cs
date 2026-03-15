namespace ContactsX.Domain.Common.Entities;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    public List<IDomainEvent> PopDomainEvents();
}
