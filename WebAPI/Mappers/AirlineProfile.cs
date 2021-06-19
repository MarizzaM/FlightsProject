using AutoMapper;
using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.Mappers
{
    public class AirlineProfile
    {
        private void AuthenticateAndGetFacade(out AnonymousUserFacade facade)
        {
            facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
        }
        public AirlineProfile(out MapperConfiguration config)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            config = new MapperConfiguration(cfg => cfg.CreateMap<AirlineCompany, AirlineCompanyDTO>()
                   .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Country_Name,
               opt => opt.MapFrom(src =>facade.GetCountry(src.Country_Id).Name)));
        }
    }
}
