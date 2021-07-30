using DakarRally.Domain.Dtos;
using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class UpdateVehicleInfoCommand : IRequest<RequestResult>
    {
        public int VehicleId { get; set; }
        public VehicleDto Vehicle { get; set; }
    }
}
