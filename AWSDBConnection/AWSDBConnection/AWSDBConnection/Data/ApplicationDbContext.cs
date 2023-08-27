using AWSDBConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSDBConnection.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ArticulosEntity> Articulo { get; set; }
    }
}
