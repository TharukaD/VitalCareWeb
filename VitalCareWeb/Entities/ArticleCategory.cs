using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
	public class ArticleCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Article> Articles { get; set; }
	}

	public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
	{
		public void Configure(EntityTypeBuilder<ArticleCategory> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(200);
		}
	}
}
