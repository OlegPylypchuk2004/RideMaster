using CameraManagment;
using UnityEngine;
using Vehicle.Building;
using Vehicle.Part;
using WorldLayoutGroup;
using Zenject;

public class VehicleAssembler : MonoBehaviour
{
    [SerializeField] private WorldGridLayoutGroup _worldGridLayoutGroup;
    [SerializeField] private FollowCamera _followCamera;

    private PartsGridData _partsGridData;
    private GameplayPart[,] _gameplayParts;

    [Inject]
    private void Construct(PartsGridData partsGridData)
    {
        _partsGridData = partsGridData;
    }

    private void Start()
    {
        SpawnParts();
        CreateJoints();

        foreach (GameplayPart gameplayPart in _gameplayParts)
        {
            if (gameplayPart == null)
            {
                continue;
            }

            gameplayPart.transform.SetParent(null);
            _followCamera.SetTarget(gameplayPart.transform);
        }

        Destroy(gameObject);
    }

    private void SpawnParts()
    {
        int rows = _partsGridData.partConfigs.GetLength(0);
        int columns = _partsGridData.partConfigs.GetLength(1);

        _gameplayParts = new GameplayPart[rows, columns];

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                PartConfig partConfig = _partsGridData.partConfigs[rowIndex, columnIndex];

                if (partConfig == null)
                {
                    GameObject gameObject = new GameObject("null");
                    gameObject.transform.SetParent(_worldGridLayoutGroup.transform);
                }
                else
                {
                    GameplayPart gameplayPart = Instantiate(partConfig.GameplayPrefab, _worldGridLayoutGroup.transform);
                    _gameplayParts[rowIndex, columnIndex] = gameplayPart;
                }
            }
        }

        _worldGridLayoutGroup.Columns = columns;
        _worldGridLayoutGroup.UpdateLayout();
    }

    private void CreateJoints()
    {
        int rows = _gameplayParts.GetLength(0);
        int columns = _gameplayParts.GetLength(1);

        Vector2Int[] directions =
        {
            new Vector2Int(0, 1),
            new Vector2Int(1, 0)
        };

        for (int rowIndex = 0; rowIndex < rows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            {
                GameplayPart part = _gameplayParts[rowIndex, columnIndex];

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
        int rows = _gameplayParts.GetLength(0);
        int columns = _gameplayParts.GetLength(1);

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

        return _gameplayParts[newRow, newColumn];
    }
}