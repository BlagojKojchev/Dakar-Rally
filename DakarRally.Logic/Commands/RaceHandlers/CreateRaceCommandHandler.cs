using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Logic.Commands.RaceHandlers
{
    public class CreateRaceCommandHandler : IRequestHandler<CreateRaceCommand, RequestResult>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateRaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<RequestResult> Handle(CreateRaceCommand request, CancellationToken cancellationToken)
        {
            var race = new Race
            {
                Year = request.Year,
                DistanceKm = request.DistanceInKm
            };
            try
            {
                this.unitOfWork.Repository<Race>().Insert(race);
                this.unitOfWork.SaveChanges();
                return Task.FromResult(
                   new RequestResult
                   {
                       IsSuccess = true,
                       Message = string.Format("Race {0} created",request.Year)
                   });
            }
            catch(Exception ex)
            {
                return Task.FromResult(
                    new RequestResult
                    {
                        IsSuccess = false,
                        Message = ex.Message
                    });
            }
        }
    }
}
