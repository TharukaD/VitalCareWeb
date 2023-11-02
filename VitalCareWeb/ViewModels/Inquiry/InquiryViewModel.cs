namespace VitalCareWeb.ViewModels.Inquiry
{
    public class InquiryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnString { get; set; }
    }
}
