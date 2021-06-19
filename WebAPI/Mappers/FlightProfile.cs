

using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightsProject.POCO;
using WebAPI.DTO;
using FlightsProject.Facade;

namespace FlightsProject.Mappers
{
    public class FlightProfile : Profile
    {
        private void AuthenticateAndGetFacade(out AnonymousUserFacade facade)
        {
            facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
        }
        public FlightProfile(out MapperConfiguration config)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

             config = new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightDTO>()
                                .ForMember(dest => dest.Id,
                            opt => opt.MapFrom(src => src.Id))
                                .ForMember(dest => dest.Airline_Company_Name,
                            opt => opt.MapFrom(src => facade.GetAirlineCompany((int)src.Airline_Company_Id).Name))
                                .ForMember(dest => dest.Origin_Country_Name,
                            opt => opt.MapFrom(src => facade.GetCountry(src.Origin_Country_Id).Name))
                                .ForMember(dest => dest.Destination_Country_Name,
                            opt => opt.MapFrom(src => facade.GetCountry(src.Destination_Country_Id).Name))
                                .ForMember(dest => dest.Departure_Time,
                            opt => opt.MapFrom(src => src.Departure_Time))
                                .ForMember(dest => dest.Landing_Time,
                            opt => opt.MapFrom(src => src.Landing_Time))
                                .ForMember(dest => dest.Tickets_Remaining,
                            opt => opt.MapFrom(src => src.Tickets_Remaining)));
        }
    }
}
