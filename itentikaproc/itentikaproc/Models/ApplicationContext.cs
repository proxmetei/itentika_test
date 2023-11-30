using Microsoft.EntityFrameworkCore;

namespace itentikaproc.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Event> Events { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
