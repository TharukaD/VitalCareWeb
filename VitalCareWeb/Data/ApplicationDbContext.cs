using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VitalCareWeb.Entities;

namespace VitalCareWeb.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Tag> Tags { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<ArticleTag> ArticleTags { get; set; }
		public DbSet<ArticleCategory> ArticleCategories { get; set; }
		public DbSet<Speciality> Specilities { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<AppointmentReason> AppointmentReasons { get; set; }
		public DbSet<Inquiry> Inquiries { get; set; }
		public DbSet<WhyChooseUsRecord> WhyChooseUsRecords { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new TagConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new SpecialityConfiguration());
			modelBuilder.ApplyConfiguration(new ServiceConfiguration());
			modelBuilder.ApplyConfiguration(new LocationConfiguration());
			modelBuilder.ApplyConfiguration(new DoctorConfiguration());
			modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
			modelBuilder.ApplyConfiguration(new AppointmentReasonConfiguration());
			modelBuilder.ApplyConfiguration(new InquiryConfiguration());
			modelBuilder.ApplyConfiguration(new WhyChooseUsRecordConfiguration());
		}
	}
}