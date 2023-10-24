using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class AppointmentReason
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }

    public class AppointmentReasonConfiguration : IEntityTypeConfiguration<AppointmentReason>
    {
        public void Configure(EntityTypeBuilder<AppointmentReason> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
