using DakarRally.Data.Models;
using DakarRally.Domain.Enums;

namespace DakarRally.Services.RaceServices
{
    public interface IRaceService
    {
        RaceStatusEnum GetRaceStatus(Race race);
        void FinishRace(object state);
    }
}
