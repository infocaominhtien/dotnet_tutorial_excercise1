using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Database;
using WebApplication7.Utils;

namespace WebApplication7.Extensions;

public static class ServiceExtension
{
    public static void ConfigureMySql(this IServiceCollection services)
    {
        Console.WriteLine("Database connecting");
        string connectionString = ConfigHelper.Config.GetConnectionString("MySQL");
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(connectionString, "connection string is required");
        string password = ConfigHelper.Config.GetValue<string>("ConnectionStrings:Password", string.Empty);
        connectionString = string.Format(connectionString, password);
        services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
    }

    public static void ConfigureJsonOptions(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
    }
}