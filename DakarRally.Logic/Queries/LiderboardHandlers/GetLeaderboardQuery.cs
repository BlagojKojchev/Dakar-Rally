using DakarRally.Domain.Results;
using MediatR;
using System.Collections.Generic;

namespace DakarRally.Logic.Queries.LiderboardHandlers
{
    public class GetLeaderboardQuery : IRequest<RequestResult<IEnumerable<VehicleRaceStatus>>>
    {
    }
}
