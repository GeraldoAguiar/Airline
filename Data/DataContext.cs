using AIRLINE.API.Models;
using AIRLINE.API.Models.Company;
using AIRLINE.API.Models.Providers;
using Microsoft.EntityFrameworkCore;

namespace AIRLINE.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<PhysicalPerson> Physical {get; set;}
        public DbSet<LegalPerson> Legal {get; set;}
        
        public DbSet<Company> Companies { get; set; }
    }
}