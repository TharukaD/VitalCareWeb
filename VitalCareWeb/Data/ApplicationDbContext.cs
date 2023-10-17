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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new TagConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
			modelBuilder.ApplyConfiguration(new ArticleCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new SpecialityConfiguration());
			modelBuilder.ApplyConfiguration(new ServiceConfiguration());
		}
	}
}