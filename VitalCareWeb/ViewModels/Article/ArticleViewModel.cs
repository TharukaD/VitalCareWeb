using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.ViewModels.Article
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublishedDateString { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; } = false;
        public int ArticleCategoryId { get; set; }
        public string ArticleCategoryName { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
