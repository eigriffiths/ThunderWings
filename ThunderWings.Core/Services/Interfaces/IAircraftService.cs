using ThunderWings.Core.DTO.Aircraft;

namespace ThunderWings.Core.Services.Interfaces
{
    public interface IAircraftService
    {
        Task<List<AircraftDto>> GetAllAircraft();
    }
}