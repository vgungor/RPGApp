using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
    }
}