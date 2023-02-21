using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TDD.API.Entity;

namespace TDD.API.DAO
{
    public class ApplicationContext : DbContext
    {
        DbSet<User> User { get; set; }
        private IConfiguration _configuration;
        public ApplicationContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ServiceDatabase"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
