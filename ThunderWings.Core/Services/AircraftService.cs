using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Core.DTO.Aircraft;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.DAL;
using ThunderWings.Repo.Models;

namespace ThunderWings.Core.Services
{
    public class AircraftService : GenericRepository<Aircraft>, IAircraftService
    {
        private readonly IMapper _mapper;

        public AircraftService(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<AircraftDto>> GetAllAircraft()
        {
            var aircraft = await All();

            return _mapper.Map<List<AircraftDto>>(aircraft);
        }
    }
}
