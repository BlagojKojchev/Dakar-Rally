using DakarRally.Domain.Results;
using MediatR;
using System.Collections.Generic;

namespace DakarRally.Logic.Queries.LiderboardHandlers
{
    public class GetLeaderboardForCarTypeQuery : IRequest<RequestResult<IEnumerable<VehicleRaceStatus>>>
    {
        public string Type { get; set; }
    }
}
