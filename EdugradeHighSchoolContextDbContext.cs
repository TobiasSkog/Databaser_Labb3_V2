using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Databaser_Labb3_V2.Models;

public partial class EdugradeHighSchoolContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."))
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("EdugradeHighSchool");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    //public virtual DbSet<View_GetGradesFromLastMonth> LastMonthsGrades { get; set; }


}
