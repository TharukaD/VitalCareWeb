using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string ViberWhatsupNo { get; set; }
        public string EmailAddress { get; set; }
        public string? FacebookURL { get; set; }
        public string? InstagramURL { get; set; }
        public string IFrameURL { get; set; }
        public virtual IList<Doctor> Doctors { get; set; }
        public virtual IList<Service> Services { get; set; }
        public int Priority { get; set; }
    }

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Image)
               .HasMaxLength(200);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNo)
                .HasMaxLength(200);

            builder.Property(x => x.ViberWhatsupNo)
               .HasMaxLength(200);

            builder.Property(x => x.ShortDescription)
                 .HasMaxLength(300);
        }
    }
}
