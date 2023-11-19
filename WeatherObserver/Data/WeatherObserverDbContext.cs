using Microsoft.EntityFrameworkCore;
using WeatherObserver.Models;

namespace WeatherObserver.Data
{
    public class WeatherObserverDbContext: DbContext
    {
        public WeatherObserverDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<WeatherExcelInfo> weather { get; set; }
    }
}
