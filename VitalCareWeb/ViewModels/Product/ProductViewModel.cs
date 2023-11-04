namespace VitalCareWeb.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Price { get; set; }
        public string PriceString { get; set; }
        public double OldPrice { get; set; }
        public string OldPriceString { get; set; }
        public string PurchaseUrl { get; set; }
        public int Priority { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
