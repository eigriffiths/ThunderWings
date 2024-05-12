﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThunderWings.Core.DTO.Aircraft;
using ThunderWings.Core.Services.Interfaces;

namespace ThunderWings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly IAircraftService _aircraftService;

        public AircraftController(IAircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAircraft([FromQuery] AircraftFilterParams aircraftFilterParams)
        {
            var aircraft = await _aircraftService.GetAllAircraft(aircraftFilterParams);

            return Ok(aircraft);
        }
    }
}
