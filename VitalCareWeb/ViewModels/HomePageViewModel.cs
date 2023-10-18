﻿using VitalCareWeb.ViewModels.Doctor;
using VitalCareWeb.ViewModels.Service;

namespace VitalCareWeb.ViewModels;

public class HomePageViewModel
{
    public List<ServiceViewModel> Services { get; set; }
    public List<DoctorViewModel> Doctors { get; set; }

    public HomePageViewModel()
    {
        Services = new List<ServiceViewModel>();
        Doctors = new List<DoctorViewModel>();
    }
}
