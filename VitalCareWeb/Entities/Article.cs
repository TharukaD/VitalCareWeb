﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VitalCareWeb.Entities
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public string AuthorName { get; set; }
		public DateTime PublishedDate { get; set; }
		public string? Image { get; set; }
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

			builder.Property(x => x.ShortDescription)
				.IsRequired();

			builder.Property(x => x.LongDescription)
				.IsRequired();

			builder.Property(x => x.AuthorName)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.Image)
				.HasMaxLength(100);

			builder.HasOne(r => r.ArticleCategory)
				.WithMany()
				.HasForeignKey(r => r.ArticleCategoryId);
		}
	}
}
