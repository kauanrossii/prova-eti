using Eti.TravelManager.Infra.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace Eti.TravelManager.Extensions;

public static class WebApplicationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            Console.WriteLine("Aplicando migrations pendentes...");
            dbContext.Database.Migrate();
            Console.WriteLine("Migrations aplicadas com sucesso.");
        }
        else
        {
            Console.WriteLine("Não foi encontrada nenhuma migration pendente.");
        }
    }
}