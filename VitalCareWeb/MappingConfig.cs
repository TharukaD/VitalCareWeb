using AutoMapper;
using VitalCareWeb.Entities;
using VitalCareWeb.Utlity;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Service;
using VitalCareWeb.ViewModels.Speciality;

namespace VitalCareWeb
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                #region Speciality
                config.CreateMap<Speciality, SpecialityViewModel>();
                config.CreateMap<Speciality, AddEditSpecialityViewModel>().ReverseMap();
                #endregion

                #region Service
                config.CreateMap<Service, ServiceViewModel>()
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnServiceImagePath(src.Image)));
                config.CreateMap<Service, AddEditServiceViewModel>().ReverseMap();
                #endregion

                #region Location
                config.CreateMap<Location, LocationViewModel>();
                config.CreateMap<Location, AddEditLocationViewModel>().ReverseMap();
                #endregion

                #region Doctor
                config.CreateMap<Doctor, DoctorViewModel>()
                    .ForMember(r => r.LocationName, opt => opt.MapFrom(src => src.Location.Name))
                    .ForMember(r => r.SpecialityName, opt => opt.MapFrom(src => src.Speciality.Name))
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnDoctorImagePath(src.Image)));
                config.CreateMap<Doctor, AddEditDoctorViewModel>().ReverseMap();
                #endregion
            });
            return mappingConfig;
        }
    }
}
