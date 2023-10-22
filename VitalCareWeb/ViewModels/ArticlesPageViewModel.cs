using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.ViewModels
{
    public class ArticlesPageViewModel
    {
        public List<ArticleViewModel> AllArticles { get; set; }
        public List<ArticleViewModel> LatestArticles { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<ArticleCategoryViewModel> Categories { get; set; }
        public ArticleViewModel Article { get; set; }
    }
}
