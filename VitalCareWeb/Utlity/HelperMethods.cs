namespace VitalCareWeb.Utlity
{
    public static class HelperMethods
    {
        public static string ReturnServiceImagePath(string serviceImage)
        {
            string ImagePath = "/img/ServiceImages/default.jpg";
            if (!string.IsNullOrEmpty(serviceImage))
            {
                ImagePath = $"/img/ServiceImages/{serviceImage}";
            }

            return ImagePath;
        }

        public static string ReturnDoctorImagePath(string doctorImage)
        {
            string ImagePath = "/img/DoctorImages/default.jpg";
            if (!string.IsNullOrEmpty(doctorImage))
            {
                ImagePath = $"/img/DoctorImages/{doctorImage}";
            }

            return ImagePath;
        }

        public static string ReturnArticleImagePath(string articleImage)
        {
            string ImagePath = "/img/ArticleImages/default.jpg";
            if (!string.IsNullOrEmpty(articleImage))
            {
                ImagePath = $"/img/ArticleImages/{articleImage}";
            }

            return ImagePath;
        }

        public static string ReturnLocationImagePath(string locationImage)
        {
            string ImagePath = "/img/LocationImages/default.jpg";
            if (!string.IsNullOrEmpty(locationImage))
            {
                ImagePath = $"/img/LocationImages/{locationImage}";
            }

            return ImagePath;
        }

        public static string ReturnAppointmentNo(int id)
        {
            string output = id.ToString().PadLeft(5, '0');
            output = "AP" + output;
            return output;
        }

        public static string ToDateString(DateTime input)
        {
            return input.ToString("MM/dd/yyyy");
        }

        public static string ToDateTimeString(DateTime input)
        {
            return input.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
