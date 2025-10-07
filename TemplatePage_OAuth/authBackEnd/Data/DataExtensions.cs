using Microsoft.EntityFrameworkCore;

namespace authbackend.Data;

public static class DataExtension
{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope = app.Services.CreateScope(); 
        var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>(); 
        await dbContext.Database.MigrateAsync();

    }

}