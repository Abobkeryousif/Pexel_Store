using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.EntityConfigruation
{
    public class ImageConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(i => i.Product)
            .WithMany(p => p.images)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
