using Databaser_Labb3_V2.Application;
using Databaser_Labb3_V2.Models;
using Databaser_Labb3_V2.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Databaser_Labb3_V2;

public class Program
{

    static void Main(string[] args)
    {

        var services = new ServiceCollection();
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json")
                .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<App>();

        services.AddDbContext<EdugradeHighSchoolContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("EdugradeHighSchool"))
        );

        var serviceProvider = services.BuildServiceProvider();

        var labb3 = serviceProvider.GetRequiredService<App>();

        labb3.Run().Wait();

        // -Framework Microsoft.EntityFrameworkCore.SqlServer
    }
}
