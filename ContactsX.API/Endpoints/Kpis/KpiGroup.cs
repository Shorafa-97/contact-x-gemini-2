using FastEndpoints;

namespace ContactsX.API.Endpoints.Kpis;

public class KpiGroup : Group
{
    public KpiGroup()
    {
        Configure("kpis", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
