using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class StartRaceCommand : IRequest<RequestResult>
    {
        public int Year { get; set; }
    }
}
