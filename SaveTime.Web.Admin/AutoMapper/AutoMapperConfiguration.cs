using AutoMapper;
using SaveTime.DataModels.Organization;
using SaveTime.Web.Admin.Models;

namespace SaveTime.Web.Admin.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static IMapper Config()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<Company, CompanyViewModel>();
                c.CreateMap<Company, CompanyEditViewModel>();
                c.CreateMap<Company, CompanyEditViewModel>().ReverseMap();
                c.CreateMap<Company, CompanyDetailViewModel>();
                c.CreateMap<Company, CompanyDeleteViewModel>();
                c.CreateMap<Company, CompanyCreateViewModel>();
                c.CreateMap<Company, CompanyCreateViewModel>().ReverseMap();

                c.CreateMap<Branch, BranchViewModel>();
                c.CreateMap<Branch, BranchEditViewModel>();
                c.CreateMap<Branch, BranchEditViewModel>().ReverseMap();
                c.CreateMap<Branch, BranchDetailViewModel>();
                c.CreateMap<Branch, BranchDeleteViewModel>();
                c.CreateMap<Branch, BranchCreateViewModel>();
                c.CreateMap<Branch, BranchCreateViewModel>().ReverseMap();

                c.CreateMap<Barber, BarberViewModel>();
                c.CreateMap<Barber, BarberEditViewModel>();
                c.CreateMap<Barber, BarberEditViewModel>().ReverseMap();
                c.CreateMap<Barber, BarberDetailViewModel>();
                c.CreateMap<Barber, BarberDeleteViewModel>();
                c.CreateMap<Barber, BarberCreateViewModel>();
                c.CreateMap<Barber, BarberCreateViewModel>().ReverseMap();

                c.CreateMap<Account, AccountViewModel>();
                c.CreateMap<Account, AccountEditViewModel>();
                c.CreateMap<Account, AccountEditViewModel>().ReverseMap();
                c.CreateMap<Account, AccountDetailViewModel>();
                c.CreateMap<Account, AccountDeleteViewModel>();
                c.CreateMap<Account, AccountCreateViewModel>();
                c.CreateMap<Account, AccountCreateViewModel>().ReverseMap();

            }).CreateMapper();
        }
    }
}