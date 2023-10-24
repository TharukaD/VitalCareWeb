using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string IdentityNo { get; set; }
        public string ReasonForVisit { get; set; }
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime PreferredDateTime { get; set; }
    }

    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNo)
               .IsRequired()
               .HasMaxLength(20);

            builder.Property(x => x.IdentityNo)
             .IsRequired()
             .HasMaxLength(20);

            builder.Property(x => x.ReasonForVisit)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(x => x.SpecialityId)
               .IsRequired();

            builder.Property(x => x.SpecialityName)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(x => x.DoctorId)
               .IsRequired();

            builder.Property(x => x.DoctorName)
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(x => x.PreferredDateTime)
              .IsRequired();
        }
    }
}
