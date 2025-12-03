using LevelSystem;
using SessionSystem;
using System;
using UnityEngine;
using VehicleSystem.Parts;
using Zenject;

namespace VehicleSystem.Building
{
    public class PartButtonsPanel : MonoBehaviour
    {
        [SerializeField] private PartButton[] _partButtons;

        private LevelConfig _levelConfig;

        public event Action<PartConfig> PartConfigSelected;

        [Inject]
        private void Construct(SessionData sessionData)
        {
            _levelConfig = sessionData.levelConfig;
        }

        private void Start()
        {
            PartData[] partDatas = CreatePartDatasCopy();

            for (int i = 0; i < _partButtons.Length; i++)
            {
                if (i < partDatas.Length)
                {
                    _partButtons[i].SetPartData(partDatas[i]);
                    _partButtons[i].Selected += OnPartButtonSelected;
                }
                else
                {
                    _partButtons[i].SetPartData(null);
                }
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _partButtons.Length; i++)
            {
                _partButtons[i].Selected += OnPartButtonSelected;
            }
        }

        public void ReturnPart(PartConfig partConfig)
        {
            foreach (PartButton partButton in _partButtons)
            {
                if (string.Equals(partButton.PartData.config.ID, partConfig.ID))
                {
                    partButton.IncreasePartsCount();

                    return;
                }
            }
        }

        private PartData[] CreatePartDatasCopy()
        {
            PartData[] sourcePartDatas = _levelConfig.PartDatas;
            PartData[] copyPartDatas = new PartData[sourcePartDatas.Length];

            for (int i = 0; i < sourcePartDatas.Length; i++)
            {
                copyPartDatas[i] = new PartData
                {
                    config = sourcePartDatas[i].config,
                    count = sourcePartDatas[i].count
                };
            }

            return copyPartDatas;
        }

        private void OnPartButtonSelected(PartButton partButton)
        {
            if (partButton.PartData.config == null || partButton.PartData.count <= 0)
            {
                return;
            }

            partButton.ReducePartsCount();

            PartConfigSelected?.Invoke(partButton.PartData.config);
        }
    }
}