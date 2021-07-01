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
    public class FlightMogifyProfile : Profile
    {
        private void AuthenticateAndGetFacade(out AnonymousUserFacade facade)
        {
            facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
        }
        public FlightMogifyProfile(out MapperConfiguration configCreation)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            configCreation = new MapperConfiguration(cfg => cfg.CreateMap<FlightDTO, Flight>()
                     .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Airline_Company_Id,
               opt => opt.MapFrom(src => facade.GetAirlineCompanyByName(src.Airline_Company_Name).Id))
                   .ForMember(dest => dest.Origin_Country_Id,
               opt => opt.MapFrom(src => facade.GetCountryByName(src.Origin_Country_Name).Id))
                   .ForMember(dest => dest.Destination_Country_Id,
               opt => opt.MapFrom(src => facade.GetCountryByName(src.Destination_Country_Name).Id))
                   .ForMember(dest => dest.Departure_Time,
               opt => opt.MapFrom(src => src.Departure_Time))
                   .ForMember(dest => dest.Landing_Time,
               opt => opt.MapFrom(src => src.Landing_Time))
                   .ForMember(dest => dest.Tickets_Remaining,
               opt => opt.MapFrom(src => src.Tickets_Remaining)));
        }
    }
}
