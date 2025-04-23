

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
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OTP> OTPs { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Verficiation> Verficiations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }

}
