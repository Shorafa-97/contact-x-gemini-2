using FastEndpoints;

namespace ContactsX.API.Endpoints.Import;

public class ImportGroup : Group
{
    public ImportGroup()
    {
        Configure("import", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
