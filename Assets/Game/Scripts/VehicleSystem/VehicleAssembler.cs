using UnityEngine;
using VehicleSystem.Building;
using VehicleSystem.Parts;
using VehicleSystem.Parts.Gameplay;
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

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (!(spawnedParts[row, column] is GameplayAttachablePart attachablePart))
                    {
                        continue;
                    }

                    GameplayPart topPart = (row > 0) ? spawnedParts[row - 1, column] : null;
                    GameplayPart bottomPart = (row < rows - 1) ? spawnedParts[row + 1, column] : null;
                    GameplayPart leftPart = (column > 0) ? spawnedParts[row, column - 1] : null;
                    GameplayPart rightPart = (column < columns - 1) ? spawnedParts[row, column + 1] : null;

                    GameplayBasePart basePart = null;

                    if (topPart is GameplayBasePart topBasePart)
                    {
                        basePart = topBasePart;
                    }
                    else if (bottomPart is GameplayBasePart bottomBasePart)
                    {
                        basePart = bottomBasePart;
                    }
                    else if (leftPart is GameplayBasePart leftBasePart)
                    {
                        basePart = leftBasePart;
                    }
                    else if (rightPart is GameplayBasePart rightBasePart)
                    {
                        basePart = rightBasePart;
                    }

                    if (basePart != null)
                    {
                        attachablePart.SetBasePart(basePart);
                    }
                }
            }
        }
    }
}