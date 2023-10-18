using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string? Image { get; set; }
        public string Qualifications { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public int SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }

    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Image)
                .HasMaxLength(200);

            builder.Property(x => x.Qualifications)
               .IsRequired()
               .HasMaxLength(300);

            builder.Property(x => x.ShortDescription)
               .IsRequired()
               .HasMaxLength(300);

            builder.Property(x => x.LongDescription)
               .IsRequired();

            builder.HasOne(x => x.Speciality)
                .WithMany(s => s.Doctors)
                .HasForeignKey(x => x.SpecialityId);

            builder.HasOne(x => x.Location)
                .WithMany(l => l.Doctors)
                .HasForeignKey(x => x.LocationId);

        }
    }
}
