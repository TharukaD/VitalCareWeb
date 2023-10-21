using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VitalCareWeb.ViewModels.ArticleCategory
{
    public class AddEditArticleCategoryViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
