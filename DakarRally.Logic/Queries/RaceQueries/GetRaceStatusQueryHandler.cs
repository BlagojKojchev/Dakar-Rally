using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using DakarRally.Services.RaceServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Queries.RaceQueries
{
    public class GetRaceStatusQueryHandler : IRequestHandler<GetRaceStatusQuery, RequestResult<RaceStatus>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRaceStatisticsService raceStatisticsService;
        private readonly IRaceService raceService;

        public GetRaceStatusQueryHandler(IUnitOfWork unitOfWork, IRaceStatisticsService raceStatisticsService, IRaceService raceService)
        {
            this.unitOfWork = unitOfWork;
            this.raceStatisticsService = raceStatisticsService;
            this.raceService = raceService;
        }
        public Task<RequestResult<RaceStatus>> Handle(GetRaceStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var race = unitOfWork.Repository<Race>()
                        .FindByInclude(x => x.Year == request.RaceYear).FirstOrDefault();

                var vehicles = unitOfWork.Repository<Vehicle>()
                        .FindByInclude(x => x.RaceId == race.Id, x => x.HeavyMalfunction, x => x.LightMalfunctions, x => x.Type);

                var vehiclesStatistics = raceStatisticsService.GetStatistics(vehicles);

                var groupedByStatus = new List<string>();
                var groupedByType = new List<string>();

                foreach (var line in vehiclesStatistics.GroupBy(x => x.Status)
                        .Select(group => new {
                            Status = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Status))
                {
                    groupedByStatus.Add(string.Format("{0} {1}", line.Status, line.Count));
                }

                foreach (var line in vehiclesStatistics.GroupBy(x => x.Vehicle.Type)
                        .Select(group => new {
                            Type = group.Key,
                            Count = group.Count()
                        })
                        .OrderBy(x => x.Type))
                {
                    groupedByType.Add(string.Format("{0} {1}", line.Type, line.Count));
                }


                var raceStatus = new RaceStatus
                {
                    NumberOfVehiclesByStatus = groupedByStatus,
                    NumberOfVehiclesByType = groupedByType,
                    status = raceService.GetRaceStatus(race).ToString()
                };

                return Task.FromResult(
                             new RequestResult<RaceStatus>
                             {
                                 IsSuccess = true,
                                 Payload = raceStatus
                             });
            }
            catch(Exception ex)
            {
                return Task.FromResult(
                             new RequestResult<RaceStatus>
                             {
                                 IsSuccess = false,
                                 Message = ex.Message
                             });
            }
        }
    }
}
