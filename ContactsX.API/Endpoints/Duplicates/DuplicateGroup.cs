using FastEndpoints;

namespace ContactsX.API.Endpoints.Duplicates;

public class DuplicateGroup : Group
{
    public DuplicateGroup()
    {
        Configure("duplicates", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
