using AutoMapper;
using Orcamentaria.CompanyService.Domain.DTOs.Company;
using Orcamentaria.CompanyService.Domain.Models;
using System;

namespace Orcamentaria.CompanyService.Domain.Mappers
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<Company, CompanyInsertDTO>()
                .ForMember(s => s.CorporateName, opt => opt.MapFrom(d => d.CorporateName))
                .ForMember(s => s.TradeName, opt => opt.MapFrom(d => d.TradeName))
                .ForMember(s => s.Cnpj, opt => opt.MapFrom(d => d.Cnpj))
                .ForMember(s => s.Address, opt => opt.MapFrom(d => d.Address))
                .ForMember(s => s.TelePhone, opt => opt.MapFrom(d => d.TelePhone))
                .ForMember(s => s.CellPhone, opt => opt.MapFrom(d => d.CellPhone))
                .ForMember(s => s.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(s => s.Active, opt => opt.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<Company, CompanyUpdateDTO>()
                .ForMember(s => s.CorporateName, opt => opt.MapFrom(d => d.CorporateName))
                .ForMember(s => s.TradeName, opt => opt.MapFrom(d => d.TradeName))
                .ForMember(s => s.Cnpj, opt => opt.MapFrom(d => d.Cnpj))
                .ForMember(s => s.Address, opt => opt.MapFrom(d => d.Address))
                .ForMember(s => s.TelePhone, opt => opt.MapFrom(d => d.TelePhone))
                .ForMember(s => s.CellPhone, opt => opt.MapFrom(d => d.CellPhone))
                .ForMember(s => s.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(s => s.Active, opt => opt.MapFrom(d => d.Active))
                .ReverseMap();

            CreateMap<CompanyResponseDTO, Company>()
                .ForMember(s => s.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(s => s.CorporateName, opt => opt.MapFrom(d => d.CorporateName))
                .ForMember(s => s.TradeName, opt => opt.MapFrom(d => d.TradeName))
                .ForMember(s => s.Cnpj, opt => opt.MapFrom(d => d.Cnpj))
                .ForMember(s => s.Address, opt => opt.MapFrom(d => d.Address))
                .ForMember(s => s.TelePhone, opt => opt.MapFrom(d => d.TelePhone))
                .ForMember(s => s.CellPhone, opt => opt.MapFrom(d => d.CellPhone))
                .ForMember(s => s.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(s => s.Active, opt => opt.MapFrom(d => d.Active))
                .ReverseMap();
        }
    }
}
