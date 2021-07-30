using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Commands.VehicleHandlers
{
    public class UpdateVehicleInfoCommandHandler : IRequestHandler<UpdateVehicleInfoCommand, RequestResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateVehicleInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<RequestResult> Handle(UpdateVehicleInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = this.unitOfWork.Repository<Vehicle>().
                 FindByInclude(x => x.Id == request.VehicleId, x => x.Race, x => x.Type).FirstOrDefault();

                if (vehicle.Race.Start != null)
                {
                    return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = "Race alredy started"
                     });
                }

                var vehicleType = this.unitOfWork.Repository<VehicleType>().
                  FindBy(x => x.Type == request.Vehicle.Type && x.SubType == request.Vehicle.SubType).FirstOrDefault();

                var race = this.unitOfWork.Repository<Race>().
                    FindBy(x => x.Year == request.Vehicle.RaceYear).FirstOrDefault();


                vehicle.ManufacturingDate = request.Vehicle.ManufacturingDate;
                vehicle.Model = request.Vehicle.Model;
                vehicle.RaceId = race.Id;
                vehicle.TeamName = request.Vehicle.TeamName;
                vehicle.VehicleTypeId = vehicleType.Id;

                this.unitOfWork.Repository<Vehicle>().Update(vehicle);

                return Task.FromResult(
                   new RequestResult
                   {
                       IsSuccess = true,
                       Message = "Vehicle Updated"
                   });
            }
            catch(Exception ex)
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
