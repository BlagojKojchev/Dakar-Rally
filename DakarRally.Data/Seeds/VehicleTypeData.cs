using DakarRally.Data.Models;
using System.Collections.Generic;

namespace DakarRally.Data.Seeds
{
    public static class VehicleTypeData
    {
        public static List<VehicleType> VehicleTypeDataList()
        {
            return new List<VehicleType>
            {
                new VehicleType
                {
                    Id =1,
                    Type = "Car",
                    SubType = "Sports",
                    Speed = 140,
                    HeavyDefect = 2,
                    LightDefect = 12,
                    RepairTime = 5

                },
                new VehicleType
                {
                    Id =2,
                    Type = "Car",
                    SubType = "Terrain",
                    Speed = 100,
                    HeavyDefect = 1,
                    LightDefect = 3,
                    RepairTime = 5
                },
                new VehicleType
                {
                    Id =3,
                    Type = "Truck",
                    Speed = 80,
                    HeavyDefect = 4,
                    LightDefect = 6,
                    RepairTime =7
                },
                new VehicleType
                {
                    Id =4,
                    Type = "Motorcycle",
                    SubType = "Cross",
                    Speed = 85,
                    HeavyDefect = 2,
                    LightDefect = 3,
                    RepairTime = 3
                },
                new VehicleType
                {
                    Id =5,
                    Type = "Motorcycle",
                    SubType = "Sport",
                    Speed = 140,
                    HeavyDefect = 10,
                    LightDefect = 18,
                    RepairTime = 3
                },
            };
        }
    }
}
