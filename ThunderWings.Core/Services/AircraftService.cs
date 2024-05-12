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

        public async Task<List<AircraftDto>> GetAllAircraft(AircraftFilterParams aircraftFilterParams)
        {
            var aircraft = await All();

            if (!string.IsNullOrEmpty(aircraftFilterParams.Name))
            {
                aircraft = aircraft.Where(a => a.Name.ToLower().Contains(aircraftFilterParams.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(aircraftFilterParams.Manufacturer))
            {
                aircraft = aircraft.Where(a => a.Manufacturer.ToLower().Contains(aircraftFilterParams.Manufacturer.ToLower()));
            }

            if (!string.IsNullOrEmpty(aircraftFilterParams.Country))
            {
                aircraft = aircraft.Where(a => a.Country.ToLower().Contains(aircraftFilterParams.Country.ToLower()));
            }

            if (!string.IsNullOrEmpty(aircraftFilterParams.Role))
            {
                aircraft = aircraft.Where(a => a.Role.ToLower().Contains(aircraftFilterParams.Role.ToLower()));
            }

            if (aircraftFilterParams.TopSpeed > 0)
            {
                aircraft = aircraft.Where(a => a.TopSpeed == aircraftFilterParams.TopSpeed);
            }

            if (aircraftFilterParams.Price > 0)
            {
                aircraft = aircraft.Where(a => a.Price == aircraftFilterParams.Price);
            }

            // Pagination (default page number is 1 and default page size is 10 this set in AircraftFilterParams)
            aircraft =
                aircraft
                .Skip((aircraftFilterParams.PageNumber - 1) * aircraftFilterParams.PageSize)
                .Take(aircraftFilterParams.PageSize);

            return _mapper.Map<List<AircraftDto>>(aircraft);
        }

        public async Task<AircraftDto> GetAircraftById(int id)
        {
            var aircraft = await Get(id);

            return _mapper.Map<AircraftDto>(aircraft);
        }
    }
}
