using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using si730pc2u20221b127.API.Shared.Domain.Repositories;
using si730pc2u20221b127.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using si730pc2u20221b127.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using si730pc2u20221b127.API.Shared.Interfaces.ASP.Configuration;
using si730pc2u20221b127.API.Subscriptions.Application.Internal.CommandServices;
using si730pc2u20221b127.API.Subscriptions.Application.Internal.QueryServices;
using si730pc2u20221b127.API.Subscriptions.Domain.Repositories;
using si730pc2u20221b127.API.Subscriptions.Domain.Services;
using si730pc2u20221b127.API.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",new OpenApiInfo
            {
                Title = "SI730PC2u20221B127.API",
                Version = "v1",
                Description = "SI730PC2U20221B127 API",
                TermsOfService = new Uri("https://si730-pc2.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "ACME Studios",
                    Email = "acme@domain.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            }
        );
        c.EnableAnnotations();
    }
    );

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Subscriptions Bounded Context Injection Configuration
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPlanCommandService, PlanCommandService>();
builder.Services.AddScoped<IPlanQueryService, PlanQueryService>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();