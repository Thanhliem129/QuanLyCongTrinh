using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        // DbSet đại diện cho bảng trong SQL Server
        public DbSet<User> NGUOIDUNG { get; set; }
    }
}
