using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public int Priority { get; set; }
        public string? PurchaseUrl { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ShortDescription)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(x => x.LongDescription)
                .IsRequired()
                .HasMaxLength(800);

            builder.Property(x => x.Image)
                .HasMaxLength(200);

            builder.Property(x => x.Price)
               .IsRequired();

            builder.HasOne(x => x.Brand)
                .WithMany(s => s.Products)
                .HasForeignKey(x => x.BrandId);
        }
    }
}
