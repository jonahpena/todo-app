using Microsoft.EntityFrameworkCore;

namespace TaskAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks { get; set; }

    }

}

