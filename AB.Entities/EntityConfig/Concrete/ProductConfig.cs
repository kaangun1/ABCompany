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
    public class ProductConfig:BaseConfig<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(p=>p.ProductName).HasMaxLength(50);
            builder.Property(p => p.ProductCode).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(500);

            builder.HasIndex(p => p.ProductName).IsUnique();//Ayni isimden 2. bir stok olamaz
            builder.HasIndex(p => p.ProductCode).IsUnique();//Ayni koddan ikinci bir stok olamaz



        }
    }
}
