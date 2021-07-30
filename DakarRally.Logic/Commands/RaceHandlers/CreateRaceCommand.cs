using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class CreateRaceCommand : IRequest<RequestResult>
    {
        public int Year { get; set; }
        public int DistanceInKm { get; set; }
    }
}
