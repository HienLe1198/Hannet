﻿using AutoMapper;
using KioskManagement.Model.Models;
using KioskManagement.Model.ViewModels;
using KioskManagement.Model.MappingModels;

namespace KioskManagement.WebApi.Infrastructure.Extentsions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppGroup, AppGroupViewModel>().ReverseMap();
            CreateMap<AppUser, AppUserViewModel>().ReverseMap();
            CreateMap<AppRole, AppRoleViewModel>().ReverseMap();
            CreateMap<AppMenu, AppMenuViewModel>().ReverseMap();
            CreateMap<TDepartment, TDepartmentViewModel>().ReverseMap();
            CreateMap<TRegency, TRegencyViewModel>().ReverseMap();
            CreateMap<TDevice, TDeviceViewModel>().ReverseMap();
            CreateMap<AZone, AZoneViewModel>().ReverseMap();
            CreateMap<TDeviceType, TDeviceTypeViewModel>().ReverseMap();
            CreateMap<TGroupAccess, TGroupAccessViewModel>().ReverseMap();
            CreateMap<TEmployee, TEmployeeMapping>().ReverseMap();
            CreateMap<TEmployee, TEmployeeViewModel>().ReverseMap();
            CreateMap<AScheduleDeviceDetail, AScheduleDeviceDetailViewModel>().ReverseMap();
            CreateMap<TGroupAccessDetail, TGroupAccessDetailViewModel>().ReverseMap();
        }
    }
}