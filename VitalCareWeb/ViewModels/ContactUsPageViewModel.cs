using VitalCareWeb.ViewModels.Inquiry;
using VitalCareWeb.ViewModels.Location;

namespace VitalCareWeb.ViewModels
{
    public class ContactUsPageViewModel
    {
        public List<ContactUsTabViewModel> Contacts { get; set; }
        public CreateInquiryViewModel CreateInquiryViewModel { get; set; }

        public void Initialize(IEnumerable<LocationViewModel> locations)
        {
            Contacts = new List<ContactUsTabViewModel>();
            int counter = 1;
            foreach (var location in locations)
            {
                string tabId = "pills-tab" + counter;
                var viewModel = new ContactUsTabViewModel()
                {
                    Name = location.Name,
                    Address = location.Address,
                    PhoneNo = location.PhoneNo,
                    ViberWhatsupNo = location.ViberWhatsupNo,
                    EmailAddress = location.EmailAddress,
                    FacebookURL = location.FacebookURL,
                    InstagramURL = location.InstagramURL,
                    IFrameURL = location.IFrameURL,
                    TabId = tabId + "-tab",
                    TabClass = "nav-link",
                    TabContentClass = "tab-pane fade",
                    DataTarget = "#" + tabId,
                    AreaControl = tabId,
                };
                Contacts.Add(viewModel);
                counter += 1;
            }
            if (counter >= 1)
            {
                Contacts[0].TabClass = Contacts[0].TabClass + " active";
                Contacts[0].TabContentClass = Contacts[0].TabContentClass + " show active";
            }
        }

    }

    public class ContactUsTabViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string ViberWhatsupNo { get; set; }
        public string EmailAddress { get; set; }
        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        public string IFrameURL { get; set; }

        public string TabId { get; set; }
        public string TabClass { get; set; }
        public string TabContentClass { get; set; }
        public string DataTarget { get; set; }
        public string AreaControl { get; set; }
    }
}
