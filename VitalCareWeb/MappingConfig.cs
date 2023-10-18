using AutoMapper;
using VitalCareWeb.Entities;
using VitalCareWeb.Utlity;
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
            });
            return mappingConfig;
        }
    }
}
