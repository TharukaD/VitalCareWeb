namespace VitalCareWeb.ViewModels.Appoinment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string AppointmentNo { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string IdentityNo { get; set; }
        public string ReasonForVisit { get; set; }
        public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime PreferredDateTime { get; set; }
        public string PreferredDateTimeString { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
    }
}
