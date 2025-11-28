using UnityEngine;
using Vehicle.Building;
using Vehicle.Part;
using Vehicle.Part.Configs;
using Zenject;

public class VehicleAssembler : MonoBehaviour
{
    private PartsGridData _partsGridData;

    [Inject]
    private void Consytuct(PartsGridData partsGridData)
    {
        _partsGridData = partsGridData;
    }

    private void Start()
    {
        for (int rowIndex = 0; rowIndex < _partsGridData.partConfigs.GetLength(0); rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < _partsGridData.partConfigs.GetLength(1); columnIndex++)
            {
                PartConfig partConfig = _partsGridData.partConfigs[rowIndex, columnIndex];

                if (partConfig == null)
                {
                    continue;
                }

                GameplayPart gameplayPart = Instantiate(partConfig.GameplayPrefab);
                gameplayPart.transform.position = new Vector3(columnIndex, rowIndex, 0f);
            }
        }
    }
}