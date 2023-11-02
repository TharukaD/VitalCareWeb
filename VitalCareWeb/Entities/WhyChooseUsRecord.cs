using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class WhyChooseUsRecord
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }

    public class WhyChooseUsRecordConfiguration : IEntityTypeConfiguration<WhyChooseUsRecord>
    {
        public void Configure(EntityTypeBuilder<WhyChooseUsRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Image)
               .HasMaxLength(200);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Description)
                 .HasMaxLength(500);
        }
    }
}
