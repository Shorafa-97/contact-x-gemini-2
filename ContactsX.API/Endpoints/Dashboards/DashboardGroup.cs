using FastEndpoints;

namespace ContactsX.API.Endpoints.Dashboards;

public class DashboardGroup : Group
{
    public DashboardGroup()
    {
        Configure("dashboard", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
