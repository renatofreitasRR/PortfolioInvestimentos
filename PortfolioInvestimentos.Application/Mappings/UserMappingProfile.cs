using AutoMapper;
using PortfolioInvestimentos.Domain.Entities;
using PortfolioInvestimentos.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<User, UserResult>()
                .ReverseMap();
        }
    }
}
