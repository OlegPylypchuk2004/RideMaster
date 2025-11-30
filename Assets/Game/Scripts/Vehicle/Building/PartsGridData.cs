using Vehicle.Part;

namespace Vehicle.Building
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