using VitalCareWeb.ViewModels.Product;

namespace VitalCareWeb.ViewModels.Brand
{
    public class BrandViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
