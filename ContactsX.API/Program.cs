using ContactsX.API.Middleware;
using ContactsX.Infrastructure.DependencyInjection;
using ContactsX.Persistence.DatabBaseContext;
using Microsoft.EntityFrameworkCore;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddFastEndpoints();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ContactsX.Application.Features.Contacts.Handlers.CreateContactHandler).Assembly));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ContactsX API",
        Version = "v1",
        Description = "ContactsX Backend API"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints();
app.MapControllers();

app.Run();