using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pexel.Core.Entities;


namespace Pexel.Infrastructrue.EntityConfigruation
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new DeliveryMethod { Id = 1, CompanyName = "Noon", DeliveryTime = "Two Days", Description = "Best Delivery Company And Fast", Price = 22 });
            builder.HasData(new DeliveryMethod { Id = 2, CompanyName = "Jahez", DeliveryTime = "1 Day", Description = "Best Delivery Company And Fast", Price = 19 });




    }
    } }
