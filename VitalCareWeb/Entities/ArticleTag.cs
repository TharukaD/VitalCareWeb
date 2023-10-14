using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
	public class ArticleTag
	{
		public int Id { get; set; }

		public int ArticleId { get; set; }
		public virtual Article Article { get; set; }

		public int TagId { get; set; }
		public virtual Tag Tag { get; set; }
	}

	public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
	{
		public void Configure(EntityTypeBuilder<ArticleTag> builder)
		{
			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.Article)
				.WithMany(r => r.ArticleTags)
				.HasForeignKey(a => a.ArticleId);

			builder.HasOne(a => a.Tag)
				.WithMany(r => r.ArticleTags)
				.HasForeignKey(a => a.TagId);
		}
	}
}
