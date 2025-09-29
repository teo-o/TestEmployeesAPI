using Microsoft.EntityFrameworkCore;
using TestHyGCasa.Shared.Entities;

namespace TestHyGCasa.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
