using ContactsX.Application.DTOs.Entity;
using ContactsX.Domain.Entities;

namespace ContactsX.Application.Interfaces.Services;

public interface IEntityService
{
    Task<Entity> CreateAsync(CreateEntityDto dto);

    Task<Entity?> UpdateAsync(UpdateEntityDto dto);

    Task<bool> DeleteAsync(Guid id);

    Task<IEnumerable<Entity>> GetAllAsync();

    Task<Entity?> GetByIdAsync(Guid id);
}