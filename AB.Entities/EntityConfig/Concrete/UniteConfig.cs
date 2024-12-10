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
    public class UniteConfig:BaseConfig<Unite>
    {
        public override void Configure(EntityTypeBuilder<Unite> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.UniteName).HasMaxLength(50);
            builder.HasIndex(p => p.UniteName).IsUnique();
        }
    }
}
