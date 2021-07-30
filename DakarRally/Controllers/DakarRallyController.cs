using DakarRally.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DakarRally.Logic.Commands.RaceHandlers;
using DakarRally.Logic.Commands.VehicleHandlers;
using DakarRally.Logic.Queries.LiderboardHandlers;
using DakarRally.Logic.Queries.VehicleHandlers;
using DakarRally.Logic.Queries.RaceQueries;
using DakarRally.Domain.Results;

namespace DakarRally.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DakarRallyController : ControllerBase
    {
        private readonly IMediator mediator;

        public DakarRallyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("create-race")]
        public ActionResult<RequestResult> CreateRace([FromBody] CreateRaceCommand command)
        {
            return mediator.Send(command).Result;
        }

        [HttpPost]
        [Route("add-vehicle")]
        public ActionResult<RequestResult> AddVehicle([FromBody] AddVehicleToRaceCommand command)
        {
            return mediator.Send(command).Result;
        }

        [HttpPut]
        [Route("update-vehicle")]
        public ActionResult<RequestResult> UpdateVehicle([FromBody] UpdateVehicleInfoCommand command)
        {
            return mediator.Send(command).Result;
        }

        [HttpDelete]
        [Route("remove-vehicle")]
        public ActionResult<RequestResult> RemoveVehicle([FromBody] RemoveVehicleCommand command)
        {
            return mediator.Send(command).Result;
        }

        [HttpPost]
        [Route("start-race")]
        public ActionResult<RequestResult> StartRace([FromBody] StartRaceCommand command)
        {
            return mediator.Send(command).Result;
        }

        [HttpGet]
        [Route("get-leaderboard")]
        public ActionResult<RequestResult> GetLeaderboard([FromQuery] GetLeaderboardQuery query)
        {
            return mediator.Send(query).Result;
        }

        [HttpGet]
        [Route("get-type-leaderboard")]
        public ActionResult<RequestResult> GetTypeeLiderboard([FromQuery] GetLeaderboardForCarTypeQuery query)
        {
            return mediator.Send(query).Result;
        }

        [HttpGet]
        [Route("get-vehicle-statistics")]
        public ActionResult<RequestResult> GetVehicleStatistics([FromQuery] GetVehicleStatisticsQuery query)
        {
            return mediator.Send(query).Result;
        }

        [HttpGet]
        [Route("find-vehicles")]
        public ActionResult<RequestResult> FindVehicles([FromQuery] FindVehiclesQuery query)
        {
            return mediator.Send(query).Result;
        }

        [HttpGet]
        [Route("get-race-status")]
        public ActionResult<RequestResult> GetRaceStatus([FromQuery] GetRaceStatusQuery query)
        {
            return mediator.Send(query).Result;
        }
    }
}
