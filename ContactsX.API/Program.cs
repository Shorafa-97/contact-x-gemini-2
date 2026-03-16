using ContactsX.API.Endpoints.Import;
    using ContactsX.API.Endpoints.Kpis;
    using ContactsX.API.Middleware;

    using ContactsX.Infrastructure.DependencyInjection;
    using ContactsX.Persistence.DatabBaseContext;
    using Microsoft.EntityFrameworkCore;
    using FastEndpoints;
    using FastEndpoints.Swagger;
    using ContactsX.Application;




    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddInfrastructure();
    // builder.Services.AddControllers();
    builder.Services.AddFastEndpoints();
    builder.Services.AddApplicationServices();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ContactsX.Application.ApplicationServiceRegistration).Assembly));


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAuthorization();

    builder.Services.SwaggerDocument(options =>
    {
        options.DocumentSettings = s =>
        {
            s.Title = "ContactsX API";
            s.Version = "v1";
            s.Description = "ContactsX Backend API";
        };
    });

    var app = builder.Build();

    app.UseSwaggerGen();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseFastEndpoints();
    
    var importGroup = app.MapGroup("/api/import");
    importGroup.MapImportContacts();
    importGroup.MapImportEntities();

    var kpiGroup = app.MapGroup("/api/kpis");
    kpiGroup.MapGetWeakContacts();
    kpiGroup.MapGetWeakEntities();
    kpiGroup.MapGetOrphanContacts();
    kpiGroup.MapGetOrphanEntities();
    kpiGroup.MapGetVipIncompleteContacts();





    // app.MapControllers();

    app.Run();