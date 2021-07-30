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
    public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand, RequestResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<RequestResult> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = this.unitOfWork.Repository<Vehicle>().
                    FindByInclude(x => x.Id == request.VehicleId, x => x.Race).FirstOrDefault();

                if (vehicle.Race.Start != null)
                {
                    return Task.FromResult(
                     new RequestResult
                     {
                         IsSuccess = false,
                         Message = "Race alredy started"
                     });
                }

                this.unitOfWork.Repository<Vehicle>().Delete(vehicle);

                this.unitOfWork.SaveChanges();

                return Task.FromResult(
                    new RequestResult
                    {
                        IsSuccess = true,
                        Message = "Vehicle removed"
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
