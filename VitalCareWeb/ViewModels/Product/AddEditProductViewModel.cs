using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VitalCareWeb.ViewModels.Brand;

namespace VitalCareWeb.ViewModels.Product
{
    public class AddEditProductViewModel
    {

        [HiddenInput]
        public int? Id { get; set; }

        [HiddenInput]
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Old Price")]
        public double OldPrice { get; set; }

        [Required]
        [Display(Name = "Purchase Url")]
        public string PurchaseUrl { get; set; }

        public double Priority { get; set; }

        [Required]
        public int BrandId { get; set; }
        public SelectList BrandSelectList { get; set; }

        public void Initialize(List<BrandViewModel> brandList)
        {
            BrandSelectList = new SelectList(brandList, "Id", "Name");
        }
    }
}
