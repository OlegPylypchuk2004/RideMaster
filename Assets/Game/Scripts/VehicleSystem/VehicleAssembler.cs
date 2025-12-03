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

            //CreateJoints(spawnedParts, rows, columns);
        }

        private void CreateJoints(GameplayPart[,] parts, int rows, int cols)
        {
            Vector2Int[] directions =
            {
                new Vector2Int(1, 0),
                new Vector2Int(0, 1)
            };

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    GameplayPart currentPart = parts[x, y];

                    if (currentPart == null)
                    {
                        continue;
                    }

                    foreach (var dir in directions)
                    {
                        GameplayPart neighbor = GetNeighbor(parts, x, y, dir, rows, cols);

                        if (neighbor != null)
                        {
                            ConnectParts(currentPart, neighbor);
                        }
                    }
                }
            }
        }

        private void ConnectParts(GameplayPart part1, GameplayPart part2)
        {
            FixedJoint joint = part1.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = part2.GetComponent<Rigidbody>();
            joint.enableCollision = false;
        }

        private GameplayPart GetNeighbor(GameplayPart[,] parts, int x, int y, Vector2Int direction, int rows, int columns)
        {
            int newX = x + direction.x;
            int newY = y + direction.y;

            if (newX >= 0 && newX < rows && newY >= 0 && newY < columns)
            {
                return parts[newX, newY];
            }

            return null;
        }
    }
}