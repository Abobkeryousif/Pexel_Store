using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pexel.Core.Entities;


namespace Pexel.Infrastructrue.EntityConfigruation
{
    public class ProductConfigruation : IEntityTypeConfiguration<Productes>
    {
        public void Configure(EntityTypeBuilder<Productes> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.OldPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.NewPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(c=> c.Category).WithMany(p=> p.products).HasForeignKey(p=>p.CategoryId);
            builder.HasMany(i => i.images).WithOne(p => p.Product).HasForeignKey(p => p.ImageId);
        }
    }
}
