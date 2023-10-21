using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Tag;

namespace VitalCareWeb.ViewModels.Article
{
    public class AddEditArticleViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }
        public string PublishedDateString { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        public bool IsPublished { get; set; } = false;

        [Required]
        [Display(Name = "Article Category")]
        public int ArticleCategoryId { get; set; }
        public SelectList ArticleCategorySelectList { get; set; }

        public List<int> TagIds { get; set; }
        public SelectList TagSelectList { get; set; }

        public void Initialize(List<ArticleCategoryViewModel> categoryList, List<TagViewModel> tagList)
        {
            TagIds = new List<int>();
            ArticleCategorySelectList = new SelectList(categoryList, "Id", "Name");
            TagSelectList = new SelectList(tagList, "Id", "Name");
            PublishedDate = DateTime.Now;
            PublishedDateString = PublishedDate.ToString("yyyy-MM-dd");
        }

        public void InitializeForEdit(List<ArticleCategoryViewModel> categoryList, List<TagViewModel> tagList, List<int> selectedTagIds)
        {
            TagIds = selectedTagIds;
            ArticleCategorySelectList = new SelectList(categoryList, "Id", "Name");
            TagSelectList = new SelectList(tagList, "Id", "Name");
            PublishedDateString = PublishedDate.ToString("yyyy-MM-dd");
        }

    }
}
