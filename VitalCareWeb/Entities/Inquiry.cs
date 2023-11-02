using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(300);

            builder.Property(x => x.EmailAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNo)
                 .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Message)
                .IsRequired();
        }
    }
}
