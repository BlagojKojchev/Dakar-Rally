using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using DakarRally.Services.RaceServices;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class GetVehicleStatisticsQueryHandler : IRequestHandler<GetVehicleStatisticsQuery, RequestResult<VehicleStatistics>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRaceStatisticsService raceStatisticsService;

        public GetVehicleStatisticsQueryHandler(IUnitOfWork unitOfWork, IRaceStatisticsService raceStatisticsService)
        {
            this.unitOfWork = unitOfWork;
            this.raceStatisticsService = raceStatisticsService;
        } 

        public Task<RequestResult<VehicleStatistics>> Handle(GetVehicleStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var race = this.unitOfWork.Repository<Race>().
                               FindByInclude(x => x.Start != null).FirstOrDefault();

                if (race == null)
                {
                    return Task.FromResult(
                         new RequestResult<VehicleStatistics>
                         {
                             IsSuccess = false,
                             Message = "There is no race started"
                         });
                }

                var vehicle = this.unitOfWork.Repository<Vehicle>().
                              FindByInclude(x => x.Id == request.VehicleId, x => x.Type, x => x.HeavyMalfunction, x => x.LightMalfunctions);

                var vehicleStatistics = raceStatisticsService.GetStatistics(vehicle).FirstOrDefault();

                return Task.FromResult(
                         new RequestResult<VehicleStatistics>
                         {
                             IsSuccess = true,
                             Payload = vehicleStatistics
                         });
            }
            catch(Exception ex)
            {
                return Task.FromResult(
                         new RequestResult<VehicleStatistics>
                         {
                             IsSuccess = false,
                             Message = ex.Message
                         });
            }
        }
    }
}
