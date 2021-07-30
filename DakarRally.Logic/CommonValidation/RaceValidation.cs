using DakarRally.Data;
using DakarRally.Data.Models;
using System.Linq;

namespace DakarRally.Logic.CommonValidation
{
    public class RaceValidation : IRaceValidation
    {
        private readonly IUnitOfWork unitOfWork;

        public RaceValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool RaceNotExist(int raceYear)
        {
            return Getrace(raceYear) == null;
        }

        public bool RaceExist(int raceYear)
        {
            return Getrace(raceYear) != null;
        }

        private Race Getrace(int raceYear)
        {
            return this.unitOfWork.Repository<Race>().
                FindBy(x => x.Year == raceYear).FirstOrDefault();
        }
    }
}
