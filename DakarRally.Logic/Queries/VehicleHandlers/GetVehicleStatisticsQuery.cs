using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class GetVehicleStatisticsQuery : IRequest<RequestResult<VehicleStatistics>>
    {
        public int VehicleId { get; set; }
    }
}
