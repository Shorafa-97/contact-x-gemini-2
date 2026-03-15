using FastEndpoints;

namespace ContactsX.API.Endpoints.Entities;

public class EntityGroup : Group
{
    public EntityGroup()
    {
        Configure("entities", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
