using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class StartRaceCommandHandler : IRequestHandler<StartRaceCommand, RequestResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public StartRaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<RequestResult> Handle(StartRaceCommand request, CancellationToken cancellationToken)
        {
            try{

                var startedRace = this.unitOfWork.Repository<Race>().FindBy(x => x.Start != null && x.Finish ==null ).FirstOrDefault();

                if(startedRace != null)
                {
                    return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = string.Format("race {0} alredy starded", startedRace.Year)
                     });
                }

                var race = this.unitOfWork.Repository<Race>().FindBy(x => x.Year == request.Year).FirstOrDefault();

                var vehicles = this.unitOfWork.Repository<Vehicle>().
                       FindByInclude(x => x.RaceId == race.Id, x => x.Type, x => x.LightMalfunctions, x => x.HeavyMalfunction).ToList();

               if(vehicles == null || vehicles.Count() == 0)
                {
                    return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = "Can not start race with no vehicles"
                     });
                }

                race.Start = DateTime.UtcNow;

                foreach(var vehicle in vehicles)
                {

                    DetectMalfunction(race, vehicle);
                }


                this.unitOfWork.Repository<Race>().Update(race);
                unitOfWork.SaveChanges();

                return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = true,
                         Message = "Race started"
                     });
            }
            catch(Exception ex)
            {
                return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = string.Format("Exception {0}", ex.Message)
                     });
            }
        }


        private void DetectMalfunction(Race race, Vehicle vehicle)
        {
            int totalHours = race.DistanceKm / vehicle.Type.Speed;

            Random random = new Random();

            for (int i = 1; i <= totalHours; i++)
            {
                if(random.NextDouble() < (vehicle.Type.HeavyDefect / 100.0))
                {
                    var heavyMalfunction = new HeavyMalfunction
                    {
                        Time = race.Start.Value.AddHours(GetRandomNumber(i - 1, i)),
                        VehicleId = vehicle.Id
                    };

                    this.unitOfWork.Repository<HeavyMalfunction>().Insert(heavyMalfunction);
                    return;
                }
                else if (random.NextDouble() < (vehicle.Type.LightDefect / 100.0))
                {
                    var lightMalfunction = new LightMalfunction
                    {
                       Time = race.Start.Value.AddHours(GetRandomNumber(i-1, i)),
                       VehicleId = vehicle.Id
                    };

                    this.unitOfWork.Repository<LightMalfunction>().Insert(lightMalfunction);

                    i += vehicle.Type.RepairTime;
                    totalHours += vehicle.Type.RepairTime;
                    
                }
            }
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

    }
}
