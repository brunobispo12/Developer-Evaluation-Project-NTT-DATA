using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;

using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName);
            });

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole(nameof(UserRole.Admin)));

                options.AddPolicy("ManagerOrAdmin", policy =>
                    policy.RequireRole(nameof(UserRole.Manager), nameof(UserRole.Admin)));

                options.AddPolicy("OperatorAndAbove", policy =>
                    policy.RequireRole(nameof(UserRole.Operator), nameof(UserRole.Manager), nameof(UserRole.Admin)));
            });

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DefaultContext>();

                    context.Database.Migrate();

                    if (!context.Users.Any(u => u.Role == UserRole.Admin))
                    {
                        var hasher = new BCryptPasswordHasher();
                        var hashedPassword = hasher.HashPassword("Admin@123");

                        var adminUser = new User
                        {
                            Id = Guid.NewGuid(),
                            Email = "admin@ds.com.br",
                            Password = hashedPassword,
                            Role = UserRole.Admin,
                            CreatedAt = DateTime.UtcNow,
                            Status = UserStatus.Active
                        };

                        context.Users.Add(adminUser);
                        context.SaveChanges();
                        Log.Information("Admin user created in development environment.");
                    }
                }
            }


            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseMiddleware<NotFoundExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}