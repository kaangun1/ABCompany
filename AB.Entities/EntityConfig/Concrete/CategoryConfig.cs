using AB.Entities.EntityConfig.Abstract;
using AB.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.Entities.EntityConfig.Concrete
{
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder); // Burayi silmeyin. Burasi BaseConfig'i çalistirir
            builder.Property(p => p.CategoryName).HasMaxLength(50);
            builder.HasIndex(p => p.CategoryName).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
