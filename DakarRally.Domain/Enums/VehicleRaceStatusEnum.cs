using System.ComponentModel;

namespace DakarRally.Domain.Enums
{
    public enum VehicleRaceStatusEnum
    {
        [Description("Running")]
        Running,
        [Description("Reparing")]
        Reparing,
        [Description("Broken Down")]
        BrokenDown,
        [Description("Finished")]
        Finished
    }
}
