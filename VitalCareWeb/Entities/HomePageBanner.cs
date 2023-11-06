using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class HomePageBanner
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TextColor { get; set; }
        public string? SmallImage { get; set; }
        public string? LargeImage { get; set; }
        public int Priority { get; set; }
    }

    public class HomePageBannerConfiguration : IEntityTypeConfiguration<HomePageBanner>
    {
        public void Configure(EntityTypeBuilder<HomePageBanner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SmallImage)
               .HasMaxLength(200);

            builder.Property(x => x.LargeImage)
               .HasMaxLength(200);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.TextColor)
               .IsRequired()
               .HasMaxLength(200);
        }
    }
}
