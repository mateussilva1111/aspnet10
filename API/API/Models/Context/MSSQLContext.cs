using Microsoft.EntityFrameworkCore;

namespace API.Models.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext(DbContextOptions<MSSQLContext> options) 
            : base(options) {}
        public DbSet<Person> Persons { get; set; }
    }
}
