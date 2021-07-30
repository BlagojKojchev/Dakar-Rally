using DakarRally.Data;
using DakarRally.Data.Models;
using DakarRally.Domain.Dtos;
using System.Linq;

namespace DakarRally.Logic.CommonValidation
{
    public class VehicleTypeValidation : IVehicleTypeValidation
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleTypeValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool CheckVehicleType(VehicleDto vehicle)
        {
            var vehicleType = getVehicleType(vehicle);

            return vehicleType != null;
        }
        public bool CheckVehicleType(string Type)
        {
            var vehicleType = this.unitOfWork.Repository<VehicleType>().
                FindBy(x => x.Type == Type).FirstOrDefault();

            return vehicleType != null;
        }

        private VehicleType getVehicleType(VehicleDto vehicle)
        {
            return this.unitOfWork.Repository<VehicleType>().
                FindBy(x => x.Type == vehicle.Type && x.SubType == vehicle.SubType).FirstOrDefault();
        }
    }
}
