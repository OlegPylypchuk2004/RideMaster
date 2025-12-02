using VehicleSystem.Parts;

namespace VehicleSystem.Building
{
    public class PartsGridData
    {
        public PartConfig[,] partConfigs;

        public PartsGridData()
        {
            partConfigs = new PartConfig[0, 0];
        }
    }
}