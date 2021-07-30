using AutoMapper;
using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using DakarRally.Services.RaceServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Queries.LiderboardHandlers
{
    public class GetLeaderboardForCarTypeQueryHandler : IRequestHandler<GetLeaderboardForCarTypeQuery, RequestResult<IEnumerable<VehicleRaceStatus>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRaceStatisticsService raceStatisticsService;
        private readonly IMapper mapper;

        public GetLeaderboardForCarTypeQueryHandler(IUnitOfWork unitOfWork, IRaceStatisticsService raceStatisticsService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.raceStatisticsService = raceStatisticsService;
            this.mapper = mapper;
        }

        public Task<RequestResult<IEnumerable<VehicleRaceStatus>>> Handle(GetLeaderboardForCarTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var race = this.unitOfWork.Repository<Race>().
                          FindByInclude(x => x.Start != null && x.Finish == null).FirstOrDefault();

                if (race == null)
                {
                    return Task.FromResult(
                         new RequestResult<IEnumerable<VehicleRaceStatus>>
                         {
                             IsSuccess = false,
                             Message = "There is no race running"
                         });
                }



                var vehicles = this.unitOfWork.Repository<Vehicle>().
                          FindByInclude(x => x.RaceId == race.Id && x.Type.Type == request.Type, x => x.Type);

                var vehiclePlaces = mapper.Map<IEnumerable<VehicleRaceStatus>>(raceStatisticsService.GetStatistics(vehicles));

                return Task.FromResult(
                         new RequestResult<IEnumerable<VehicleRaceStatus>>
                         {
                             IsSuccess = true,
                             Payload = vehiclePlaces.OrderBy(x => x.Distance).ToList()
                         });
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                        new RequestResult<IEnumerable<VehicleRaceStatus>>
                        {
                            IsSuccess = false,
                            Message = ex.Message
                        });
            }
        }
    }
}
