using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Uniteds.CaseStudy.Domain.Models;

namespace Uniteds.CaseStudy.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";
      

        public AppDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CaseStudy;Persist Security Info=True";
                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            }
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
