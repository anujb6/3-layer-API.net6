using Data_Access_layer.Repository.Entities;
using Data_Access_layer.Repository.Models;
using Microsoft.EntityFrameworkCore;
namespace Data_Access_layer.Repository
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options): base(options) { }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
