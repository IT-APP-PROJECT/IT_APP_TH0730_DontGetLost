using DontGetLost.Models;
using Microsoft.EntityFrameworkCore;

namespace DontGetLost.Contracts
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Test> Users { get; set; }
    }
}