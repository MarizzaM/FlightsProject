using AutoMapper;
using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.DTO;

namespace WebAPI.Mappers
{
    public class TicketProfile
    {
        private void AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                           out LoggedInAdministratorFacade facadeAdmin)
        {


            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin("admin", "9999", out tokenAdmin);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;
        }
        public TicketProfile(out MapperConfiguration config)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                                       out LoggedInAdministratorFacade facadeAdmin);

            config = new MapperConfiguration(cfg => cfg.CreateMap<Ticket, TicketDTO>()
                   .ForMember(dest => dest.Id_Ticket,
               opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Id_Flight,
               opt => opt.MapFrom(src => src.Id_Flight))
                    .ForMember(dest => dest.First_Name,
               opt => opt.MapFrom(src => facadeAdmin.GetCustomer((int)src.Id_Customer).First_Name))
                    .ForMember(dest => dest.Last_Name,
               opt => opt.MapFrom(src => facadeAdmin.GetCustomer((int)src.Id_Customer).Last_Name)
               ));

        }
    }
}
