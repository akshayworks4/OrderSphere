using OrderSphere.Infrastructure;
using OrderSphere.Infrastructure.DependencyInjection;
using OrderSphere.Persistence.Context;
using OrderSphere.Persistence.DependencyInjection;
using OrderSphere.Persistence.Seeding;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Clean Architecture layers
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

#endregion

var app = builder.Build();

#region Middleware Pipeline

// Swagger (only in development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global middlewares
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
#endregion

#region Database Seeding

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderSphereDbContext>();
        await RoleSeeder.SeedAsync(dbContext);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seeding failed: {ex.Message}");
    }
}

#endregion

app.Run();