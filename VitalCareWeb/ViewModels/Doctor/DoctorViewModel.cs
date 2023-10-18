namespace VitalCareWeb.ViewModels.Doctor
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Qualifications { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Gender { get; set; }

        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public string? FacebookUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
    }
}
