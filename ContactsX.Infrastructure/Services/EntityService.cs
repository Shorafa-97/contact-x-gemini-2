using AutoMapper;
using ContactsX.Application.DTOs.Entity;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.Interfaces.Services;
using ContactsX.Domain.Entities;

public class EntityService : IEntityService
{
    private readonly IRepository<Entity> _entityRepository;
    private readonly IMapper _mapper;

    public EntityService(IRepository<Entity> entityRepository, IMapper mapper)
    {
        _entityRepository = entityRepository;
        _mapper = mapper;
    }

    public async Task<Entity> CreateAsync(CreateEntityDto dto)
    {
        var entity = _mapper.Map<Entity>(dto);
        entity.ProfileCompleteness = CalculateEntityCompleteness(entity);
        await _entityRepository.AddAsync(entity);
        await _entityRepository.SaveChangesAsync();

        return entity;
    }

    public async Task<Entity?> UpdateAsync(UpdateEntityDto dto)
    {
        var entity = await _entityRepository.GetByIdAsync(dto.Id);

        if (entity == null)
            return null;

        _mapper.Map(dto, entity);
        entity.ProfileCompleteness = CalculateEntityCompleteness(entity);
        _entityRepository.Update(entity);
        await _entityRepository.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _entityRepository.GetByIdAsync(id);

        if (entity == null)
            return false;

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        _entityRepository.Update(entity);
        await _entityRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await _entityRepository.GetAllAsync();
    }

    public async Task<Entity?> GetByIdAsync(Guid id)
    {
        return await _entityRepository.GetByIdAsync(id);
    }
    private int CalculateEntityCompleteness(Entity entity)
    {
        int totalWeight = 100;
        int filledWeight = 0;

        if (!string.IsNullOrWhiteSpace(entity.NameEn)) filledWeight += 20;
        filledWeight += 15;
        if (!string.IsNullOrWhiteSpace(entity.Country)) filledWeight += 15;
        if (!string.IsNullOrWhiteSpace(entity.Sector)) filledWeight += 15;
        if (!string.IsNullOrWhiteSpace(entity.NameAr)) filledWeight += 10;
        if (!string.IsNullOrWhiteSpace(entity.RegistrationId)) filledWeight += 10;
        if (!string.IsNullOrWhiteSpace(entity.Addresses) && entity.Addresses != "[]") filledWeight += 10;
        if (!string.IsNullOrWhiteSpace(entity.ContactPoints) && entity.ContactPoints != "[]") filledWeight += 5;

        return (int)Math.Round((double)filledWeight / totalWeight * 100);
    }
}