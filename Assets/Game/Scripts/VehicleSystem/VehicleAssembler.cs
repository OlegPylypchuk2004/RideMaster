using UnityEngine;
using VehicleSystem.Building;
using VehicleSystem.Parts;
using Zenject;

namespace VehicleSystem
{
    public class VehicleAssembler : IInitializable
    {
        private DiContainer _container;
        private readonly Vehicle _vehicle;
        private readonly PartsGridData _partsGridData;

        public VehicleAssembler(DiContainer container, Vehicle vehicle, PartsGridData partsGridData)
        {
            _container = container;
            _vehicle = vehicle;
            _partsGridData = partsGridData;
        }

        public void Initialize()
        {
            BuildVehicle();
        }

        public void BuildVehicle()
        {
            int rows = _partsGridData.partConfigs.GetLength(0);
            int columns = _partsGridData.partConfigs.GetLength(1);

            GameplayPart[,] spawnedParts = new GameplayPart[rows, columns];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    PartConfig config = _partsGridData.partConfigs[rowIndex, columnIndex];

                    if (config == null)
                    {
                        continue;
                    }

                    Vector3 localPosition = new Vector3(0f, rows - 1 - rowIndex, columnIndex);

                    GameplayPart part = _container.InstantiatePrefabForComponent<GameplayPart>(config.GameplayPrefab, _vehicle.transform);
                    part.transform.localPosition = localPosition;

                    spawnedParts[rowIndex, columnIndex] = part;

                    _vehicle.AddPart(part);
                }
            }
        }
    }
}