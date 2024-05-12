using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Core.DTO.Aircraft;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Mappers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aircraft, AircraftDto>();
            CreateMap<AircraftDto, Aircraft>();
        }
    }
}
