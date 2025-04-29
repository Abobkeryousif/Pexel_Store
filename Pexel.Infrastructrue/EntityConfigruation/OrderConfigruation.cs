using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pexel.Core.Common.Enum;
using Pexel.Core.Entities;

namespace Pexel.Infrastructrue.EntityConfigruation
{
    public class OrderConfigruation : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(c=> c.customerAddress,o=> { o.WithOwner();});
            //if i delete order...orderitem will delete too
            builder.HasMany(oi => oi.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            //convert from enum to string
            builder.Property(s => s.orderStatues).HasConversion(o => o.ToString(), o => (OrderStatues)Enum.Parse(typeof(OrderStatues), o));
            builder.Property(p => p.SupTotal).HasColumnType("decimal(18,2)");
            
        }
    }
}
