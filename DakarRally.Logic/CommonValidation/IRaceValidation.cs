using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRally.Logic.CommonValidation
{
    public interface IRaceValidation
    {
        bool RaceExist(int raceYear);
        bool RaceNotExist(int raceYear);
    }
}
