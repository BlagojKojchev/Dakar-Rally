using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Enums;
using System.Linq;

namespace DakarRally.Services.RaceServices
{
    public class RaceService : IRaceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRaceStatisticsService raceStatisticsService;

        public RaceService(IUnitOfWork unitOfWork, IRaceStatisticsService raceStatisticsService)
        {
            this.unitOfWork = unitOfWork;
            this.raceStatisticsService = raceStatisticsService;
        }
        public void FinishRace(object state)
        {
            var race = unitOfWork.Repository<Race>()
                    .FindByInclude(x => x.Start.HasValue && !x.Finish.HasValue).FirstOrDefault();
            if(race == null)
            {
                return;
            }

            var vehicles = unitOfWork.Repository<Vehicle>()
                    .FindByInclude(x => x.RaceId == race.Id, x => x.Type, x => x.LightMalfunctions, x => x.HeavyMalfunction);

            var vehiclesStatistics = raceStatisticsService.GetStatistics(vehicles);

            var finishedVehicles = vehiclesStatistics.Where(x => x.Status == VehicleRaceStatusEnum.Finished.ToString()).Count();
            var brokendonwVehicles = vehiclesStatistics.Where(x => x.Status == VehicleRaceStatusEnum.BrokenDown.ToString()).Count();

            var finishTime = vehiclesStatistics.Where(x => x.Status == VehicleRaceStatusEnum.Finished.ToString()).LastOrDefault()?.FinishTime;

           

            if (finishedVehicles + brokendonwVehicles  == vehicles.Count())
            {
                race.Finish = race.Start.Value.AddHours(finishTime.Value);
                unitOfWork.Repository<Race>().Update(race);
                unitOfWork.SaveChanges();
            }
        }

        public RaceStatusEnum GetRaceStatus(Race race)
        {
            var racestatus = RaceStatusEnum.Pending;
            if(race.Start.HasValue && !race.Finish.HasValue)
            {
                racestatus = RaceStatusEnum.Running;
            }
            else if(race.Start.HasValue && race.Finish.HasValue)
            {
                racestatus = RaceStatusEnum.Finished;
            }

            return racestatus;
        }
    }
}
