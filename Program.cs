using Databaser_Labb3_V2.Application;
using Databaser_Labb3_V2.Models;
using Databaser_Labb3_V2.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Databaser_Labb3_V2;

public class Program
{

    static void Main(string[] args)
    {
        //var builder = Application.CreateBuilder(args);
        var services = new ServiceCollection();

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<App>();
        services.AddDbContext<EdugradeHighSchoolContext>();

        var serviceProvider = services.BuildServiceProvider();

        var labb3 = serviceProvider.GetRequiredService<App>();

        labb3.Run().Wait();


    }
}
