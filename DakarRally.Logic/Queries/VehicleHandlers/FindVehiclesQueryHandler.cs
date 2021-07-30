using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Enums;
using DakarRally.Domain.Results;
using DakarRally.Services.RaceServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class FindVehiclesQueryHandler : IRequestHandler<FindVehiclesQuery, RequestResult<IEnumerable<VehicleStatistics>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRaceStatisticsService raceStatisticsService;

        public FindVehiclesQueryHandler(IUnitOfWork unitOfWork, IRaceStatisticsService raceStatisticsService)
        {
            this.unitOfWork = unitOfWork;
            this.raceStatisticsService = raceStatisticsService;
        }

        public Task<RequestResult<IEnumerable<VehicleStatistics>>> Handle(FindVehiclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var race = unitOfWork.Repository<Race>()
                    .FindByInclude(x => x.Start != null).FirstOrDefault();

                if (race == null)
                {
                    return Task.FromResult(
                         new RequestResult<IEnumerable<VehicleStatistics>>
                         {
                             IsSuccess = false,
                             Message = "There is no race started"
                         });
                }

                var vehicles = unitOfWork.Repository<Vehicle>().AllInclude(x => x.Type, x => x.HeavyMalfunction, x => x.LightMalfunctions);

                var vehicleStatistics = raceStatisticsService.GetStatistics(vehicles).
                    Where(x => (request.manufacturingDate == null || x.Vehicle.ManufacturingDate == request.manufacturingDate)
                    && (request.Distance == null || x.Distance <= request.Distance)
                    && (request.Model == null || x.Vehicle.Model == request.Model)
                    && (request.status == null || x.Status == request.status.ToString())
                    && (request.Team == null || x.Vehicle.TeamName == request.Team));

                if(request.SortOrder == SortOrderEnum.Ascending)
                {
                    vehicleStatistics.OrderBy(x => x.Distance);
                }
                else
                {
                    vehicleStatistics.OrderByDescending(x => x.Distance);
                }

                return Task.FromResult(
                         new RequestResult<IEnumerable<VehicleStatistics>>
                         {
                             IsSuccess = true,
                            Payload = vehicleStatistics
                         });
            }
            catch(Exception ex)
            {
                return Task.FromResult(
                         new RequestResult<IEnumerable<VehicleStatistics>>
                         {
                             IsSuccess = false,
                             Message = ex.Message
                         });
            }

        }
    }
}
