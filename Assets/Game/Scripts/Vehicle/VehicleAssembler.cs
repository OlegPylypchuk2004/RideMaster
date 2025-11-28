using UnityEngine;
using Vehicle;
using Vehicle.Building;
using Vehicle.Part;
using Vehicle.Part.Configs;
using Zenject;

public class VehicleAssembler : MonoBehaviour
{
    [SerializeField] private GameplayVehicle _vehiclePrefab;

    private PartsGridData _partsGridData;
    private GameplayPart[,] _spawnedParts;

    [Inject]
    private void Construct(PartsGridData partsGridData)
    {
        _partsGridData = partsGridData;
    }

    private void Start()
    {
        GameplayVehicle vehicle = Instantiate(_vehiclePrefab);

        SpawnParts(vehicle);
        CreateJoints();
    }

    private void SpawnParts(GameplayVehicle vehicle)
    {
        int rows = _partsGridData.partConfigs.GetLength(0);
        int columns = _partsGridData.partConfigs.GetLength(1);

        _spawnedParts = new GameplayPart[rows, columns];

        float offsetX = (columns - 1) / 2f;
        float offsetY = (rows - 1) / 2f;

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                PartConfig partConfig = _partsGridData.partConfigs[rowIndex, columnIndex];

                if (partConfig == null)
                    continue;

                GameplayPart gameplayPart = Instantiate(partConfig.GameplayPrefab, vehicle.transform);
                gameplayPart.transform.localPosition = new Vector3(columnIndex - offsetX, rowIndex - offsetY, 0f);

                _spawnedParts[rowIndex, columnIndex] = gameplayPart;
            }
        }
    }

    private void CreateJoints()
    {
        int rows = _spawnedParts.GetLength(0);
        int columns = _spawnedParts.GetLength(1);

        Vector2Int[] directions =
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0)
        };

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                GameplayPart part = _spawnedParts[rowIndex, columnIndex];

                if (part == null)
                {
                    continue;
                }

                foreach (Vector2Int direction in directions)
                {
                    GameplayPart neighbor = GetNeighbor(rowIndex, columnIndex, direction);

                    if (neighbor == null)
                    {
                        continue;
                    }

                    FixedJoint joint = part.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = neighbor.GetComponent<Rigidbody>();
                }
            }
        }
    }

    private GameplayPart GetNeighbor(int row, int column, Vector2Int direction)
    {
        int rows = _spawnedParts.GetLength(0);
        int columns = _spawnedParts.GetLength(1);

        int newRow = row + direction.x;
        int newColumn = column + direction.y;

        if (newRow < 0 || newRow >= rows)
        {
            return null;
        }

        if (newColumn < 0 || newColumn >= columns)
        {
            return null;
        }

        return _spawnedParts[newRow, newColumn];
    }
}