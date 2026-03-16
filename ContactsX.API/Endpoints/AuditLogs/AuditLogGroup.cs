using FastEndpoints;

namespace ContactsX.API.Endpoints.AuditLogs;

public class AuditLogGroup : Group
{
    public AuditLogGroup()
    {
        Configure("audit-logs", ep =>
        {
            ep.AllowAnonymous();
        });
    }
}
