using ContactsX.Application.Features.Contacts.Commands;
using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Domain.Entities;
using MediatR;

namespace ContactsX.Application.Features.Contacts.Handlers;

public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IRepository<Contact> _repository;

    public DeleteContactHandler(IRepository<Contact> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _repository.GetByIdAsync(request.Id);
        if (contact == null) return false;

        _repository.Delete(contact);
        await _repository.SaveChangesAsync();
        return true;
    }
}
