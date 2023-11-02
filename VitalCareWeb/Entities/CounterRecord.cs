using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class CounterRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountInfo { get; set; }
        public string? Image { get; set; }
        public int Priority { get; set; }
    }

    public class CounterRecordConfiguration : IEntityTypeConfiguration<CounterRecord>
    {
        public void Configure(EntityTypeBuilder<CounterRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Image)
               .HasMaxLength(200);

            builder.Property(x => x.CountInfo)
             .IsRequired()
             .HasMaxLength(100);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
