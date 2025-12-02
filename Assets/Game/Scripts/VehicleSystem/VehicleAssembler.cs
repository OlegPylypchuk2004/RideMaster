using RoadSystem;
using UnityEngine;
using VehicleSystem.Building;
using VehicleSystem.Parts;
using Zenject;

namespace VehicleSystem
{
    public class VehicleAssembler : IInitializable
    {
        private readonly DiContainer _container;
        private readonly PartsGridData _partsGridData;
        private readonly Road _road;

        public VehicleAssembler(DiContainer container, PartsGridData partsGridData, Road road)
        {
            _container = container;
            _partsGridData = partsGridData;
            _road = road;
        }

        public void Initialize()
        {
            BuildVehicle();
        }

        public void BuildVehicle()
        {
            Vehicle vehicle = new GameObject("Vehicle")
                .AddComponent<Vehicle>();

            vehicle.transform.position = _road.VehicleStartPoint;

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

                    GameplayPart part = _container.InstantiatePrefabForComponent<GameplayPart>(config.GameplayPrefab, vehicle.transform);
                    part.transform.localPosition = localPosition;

                    spawnedParts[rowIndex, columnIndex] = part;

                    vehicle.AddPart(part);
                }
            }

            CreateJoints(spawnedParts, rows, columns);

            _container.Bind<Vehicle>()
                .FromInstance(vehicle)
                .AsSingle();
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