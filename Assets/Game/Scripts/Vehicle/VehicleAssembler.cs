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

    [Inject]
    private void Construct(PartsGridData partsGridData)
    {
        _partsGridData = partsGridData;
    }

    private void Start()
    {
        GameplayVehicle vehicle = Instantiate(_vehiclePrefab);

        for (int rowIndex = 0; rowIndex < _partsGridData.partConfigs.GetLength(0); rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < _partsGridData.partConfigs.GetLength(1); columnIndex++)
            {
                PartConfig partConfig = _partsGridData.partConfigs[rowIndex, columnIndex];

                if (partConfig == null)
                {
                    continue;
                }

                GameplayPart gameplayPart = Instantiate(partConfig.GameplayPrefab, vehicle.transform);
                gameplayPart.transform.localPosition = new Vector3(columnIndex, rowIndex, 0f);
            }
        }
    }
}