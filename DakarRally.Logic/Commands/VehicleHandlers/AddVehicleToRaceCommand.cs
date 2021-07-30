using DakarRally.Domain;
using DakarRally.Domain.Dtos;
using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class AddVehicleToRaceCommand : IRequest<RequestResult>
    {
        public VehicleDto Vehicle { get; set; }
    }
}
