using FastEndpoints;

namespace ContactsX.API.Endpoints.Auth;

public class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("auth", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
