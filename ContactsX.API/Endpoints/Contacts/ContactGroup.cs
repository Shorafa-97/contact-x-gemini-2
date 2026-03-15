using FastEndpoints;

namespace ContactsX.API.Endpoints.Contacts;

public class ContactGroup : Group
{
    public ContactGroup()
    {
        Configure("contacts", ep =>
        {
            ep.AllowAnonymous(); // Global group configuration
        });
    }
}
