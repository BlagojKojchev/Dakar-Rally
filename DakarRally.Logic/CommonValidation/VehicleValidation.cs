using DakarRally.Data;
using DakarRally.Data.Models;
using System;
using System.Linq;

namespace DakarRally.Logic.CommonValidation
{
    public class VehicleValidation : IVehicleValidation
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool CheckVehicle(int vehicleId)
        {
            var vehicle = this.unitOfWork.Repository<Vehicle>().
                 FindBy(x => x.Id == vehicleId).FirstOrDefault();

            return vehicle != null;
        }

        public bool CheckVehicleTeam(string Team)
        {
            if(Team == null)
            {
                return true;
            }

            var vehicle = this.unitOfWork.Repository<Vehicle>().
                FindBy(x => x.TeamName == Team).FirstOrDefault();
            return vehicle != null;
        }

        public bool CheckVehicleModel(string Model)
        {
            if (Model == null)
            {
                return true;
            }

            var vehicle = this.unitOfWork.Repository<Vehicle>().
                FindBy(x => x.Model == Model).FirstOrDefault();
            return vehicle != null;
        }

        public bool CheckVehicleManufacturingDate(DateTime? manufacturingDate)
        {
            if (manufacturingDate == null)
            {
                return true;
            }

            var vehicle = this.unitOfWork.Repository<Vehicle>().
                FindBy(x => x.ManufacturingDate == manufacturingDate).FirstOrDefault();
            return vehicle != null;
        }
    }
}
