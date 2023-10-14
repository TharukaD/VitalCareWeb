using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string AuthorName { get; set; }
		public DateTime PublishedDate { get; set; }
		public string ImageType { get; set; }
		public virtual IList<ArticleTag> ArticleTags { get; set; }
		public bool IsPublished { get; set; } = false;

		public int ArticleCategoryId { get; set; }
		public virtual ArticleCategory ArticleCategory { get; set; }
	}

	public class ArticleConfiguration : IEntityTypeConfiguration<Article>
	{
		public void Configure(EntityTypeBuilder<Article> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(x => x.Description)
				.IsRequired();

			builder.Property(x => x.AuthorName)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.ImageType)
				.IsRequired()
				.HasMaxLength(20);

			builder.HasOne(r => r.ArticleCategory)
				.WithMany()
				.HasForeignKey(r => r.ArticleCategoryId);
		}
	}
}
