using FastEndpoints;

namespace ContactsX.API.Endpoints.Auth;

public class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("api/auth", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
