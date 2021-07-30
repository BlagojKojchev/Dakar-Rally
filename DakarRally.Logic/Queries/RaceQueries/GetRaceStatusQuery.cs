using DakarRally.Domain.Results;
using MediatR;

namespace DakarRally.Logic.Queries.RaceQueries
{
    public class GetRaceStatusQuery : IRequest<RequestResult<RaceStatus>>
    {
        public int RaceYear { get; set; }
    }
}
