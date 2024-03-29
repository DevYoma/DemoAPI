using DemoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<StudentEntity> StudentRegister { get; set; } // Students will be the name on the table
    }
}
