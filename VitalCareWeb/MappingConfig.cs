﻿using AutoMapper;
using VitalCareWeb.Entities;
using VitalCareWeb.Utlity;
using VitalCareWeb.ViewModels.Appoinment;
using VitalCareWeb.ViewModels.AppointmentReason;
using VitalCareWeb.ViewModels.Article;
using VitalCareWeb.ViewModels.ArticleCategory;
using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Inquiry;
using VitalCareWeb.ViewModels.Location;
using VitalCareWeb.ViewModels.Service;
using VitalCareWeb.ViewModels.Speciality;
using VitalCareWeb.ViewModels.Tag;
using VitalCareWeb.ViewModels.WhyChooseUsRecord;

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
                    .ForMember(r => r.LocationName, opt => opt.MapFrom(src => src.Location.Name))
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnServiceImagePath(src.Image)));
                config.CreateMap<Service, AddEditServiceViewModel>().ReverseMap();
                #endregion

                #region Location
                config.CreateMap<Location, LocationViewModel>()
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnLocationImagePath(src.Image)));
                config.CreateMap<Location, AddEditLocationViewModel>().ReverseMap();
                #endregion

                #region Doctor
                config.CreateMap<Doctor, DoctorViewModel>()
                    .ForMember(r => r.LocationName, opt => opt.MapFrom(src => src.Location.Name))
                    .ForMember(r => r.SpecialityName, opt => opt.MapFrom(src => src.Speciality.Name))
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnDoctorImagePath(src.Image)));
                config.CreateMap<Doctor, AddEditDoctorViewModel>().ReverseMap();
                #endregion

                #region Tag
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<Tag, AddEditTagViewModel>().ReverseMap();
                #endregion

                #region Article Category
                config.CreateMap<ArticleCategory, ArticleCategoryViewModel>();
                config.CreateMap<ArticleCategory, AddEditArticleCategoryViewModel>().ReverseMap();
                #endregion

                #region Article
                config.CreateMap<Article, ArticleViewModel>()
                    .ForMember(r => r.PublishedDateString, opt => opt.MapFrom(src => HelperMethods.ToDateString(src.PublishedDate)))
                    .ForMember(r => r.ArticleCategoryName, opt => opt.MapFrom(src => src.ArticleCategory.Name))
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnArticleImagePath(src.Image)))
                    .ForMember(r => r.Tags, opt => opt.MapFrom(src => src.ArticleTags.Select(r => new TagViewModel { Id = r.Id, Name = r.Tag.Name })));
                config.CreateMap<Article, AddEditArticleViewModel>().ReverseMap();
                #endregion

                #region Appointment Reason
                config.CreateMap<AppointmentReason, AppointmentReasonViewModel>();
                config.CreateMap<AppointmentReason, AddEditAppointmentReasonViewModel>().ReverseMap();
                #endregion

                #region Appointment
                config.CreateMap<Appointment, AppointmentViewModel>()
                    .ForMember(r => r.AppointmentNo, opt => opt.MapFrom(src => HelperMethods.ReturnAppointmentNo(src.Id)))
                    .ForMember(r => r.PreferredDateTimeString, opt => opt.MapFrom(src => HelperMethods.ToDateTimeString(src.PreferredDateTime)))
                    .ForMember(r => r.CreatedDateString, opt => opt.MapFrom(src => HelperMethods.ToDateTimeString(src.CreatedDate)));
                config.CreateMap<CreateAppoinmentViewModel, Appointment>();
                #endregion

                #region Inquiry
                config.CreateMap<Inquiry, InquiryViewModel>()
                    .ForMember(r => r.CreatedOnString, opt => opt.MapFrom(src => HelperMethods.ToDateTimeString(src.CreatedOn)));
                config.CreateMap<CreateInquiryViewModel, Inquiry>();
                #endregion

                #region WhyChooseUsRecord
                config.CreateMap<WhyChooseUsRecord, WhyChooseUsRecordViewModel>()
                    .ForMember(r => r.ImageUrl, opt => opt.MapFrom(src => HelperMethods.ReturnWhyChooseUsRecordImagePath(src.Image)));
                config.CreateMap<WhyChooseUsRecord, AddEditWhyChooseUsRecordViewModel>().ReverseMap();
                #endregion
            });
            return mappingConfig;
        }
    }
}
