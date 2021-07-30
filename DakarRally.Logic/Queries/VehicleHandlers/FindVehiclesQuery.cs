using DakarRally.Domain.Enums;
using DakarRally.Domain.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace DakarRally.Logic.Queries.VehicleHandlers
{
    public class FindVehiclesQuery : IRequest<RequestResult<IEnumerable<VehicleStatistics>>>
    {
        public string Team { get; set; }
        public string Model { get; set; }
        public DateTime? manufacturingDate { get; set; }
        public VehicleRaceStatusEnum? status { get; set; }
        public double? Distance { get; set; }
        public SortOrderEnum? SortOrder { get; set; }
    }
}
