

using Microsoft.EntityFrameworkCore;
using Pexel.Core.Entities;
using System.Reflection;

namespace Pexel.Application.Contracts.Interfaces
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Productes> Products { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }

}
