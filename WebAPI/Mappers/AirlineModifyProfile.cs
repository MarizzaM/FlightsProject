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
    public class AirlineModifyProfile : Profile
    {
        private void AuthenticateAndGetFacade(out AnonymousUserFacade facade)
        {
            facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
        }
        public AirlineModifyProfile(out MapperConfiguration configModify)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            configModify = new MapperConfiguration(cfg => cfg.CreateMap<AirlineModifyDTO, AirlineCompany>()
                    .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Country_Id,
               opt => opt.MapFrom(src => facade.GetCountryByName(src.Country_Name).Id)));
        }
    }
}
