using ContactsX.Application.Features.Entities.Commands;
using ContactsX.Domain.Entities;
using ContactsX.Application.Interfaces.Repositories;
using MediatR;

namespace ContactsX.Application.Features.Entities.Handlers;

public class DeleteEntityHandler : IRequestHandler<DeleteEntityCommand, bool>
{
    private readonly IRepository<Entity> _repository;

    public DeleteEntityHandler(IRepository<Entity> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;

        _repository.Delete(entity);
        await _repository.SaveChangesAsync();

        return true;
    }
}
