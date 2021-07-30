using DakarRally.Data;
using DakarRally.Data.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DakarRally.Domain.Results;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class AddVehicleToRaceCommandHandler : IRequestHandler<AddVehicleToRaceCommand, RequestResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddVehicleToRaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<RequestResult> Handle(AddVehicleToRaceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicleType = this.unitOfWork.Repository<VehicleType>().
                FindBy(x => x.Type == request.Vehicle.Type && x.SubType == request.Vehicle.SubType).FirstOrDefault();

                var race = this.unitOfWork.Repository<Race>().
                    FindBy(x => x.Year == request.Vehicle.RaceYear).FirstOrDefault();

                if(race.Start != null)
                {
                    return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = "Race alredy started"
                     });
                }

                var vehicle = new Vehicle
                {
                    ManufacturingDate = request.Vehicle.ManufacturingDate,
                    Model = request.Vehicle.Model,
                    RaceId = race.Id,
                    TeamName = request.Vehicle.TeamName,
                    VehicleTypeId = vehicleType.Id,
                };


                this.unitOfWork.Repository<Vehicle>().Insert(vehicle);

                this.unitOfWork.SaveChanges();

                return Task.FromResult(
                    new RequestResult
                    {
                        IsSuccess = true,
                        Message = "Vehicle added"
                    });
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = string.Format("Exception {0}", ex.Message)
                     });
            }
        }
    }
}
