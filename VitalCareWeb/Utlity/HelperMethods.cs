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
    }
}
