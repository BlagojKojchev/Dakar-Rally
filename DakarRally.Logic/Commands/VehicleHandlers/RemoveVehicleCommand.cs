using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class RemoveVehicleCommand : IRequest<RequestResult>
    {
        public int VehicleId { get; set; }
    }
}
