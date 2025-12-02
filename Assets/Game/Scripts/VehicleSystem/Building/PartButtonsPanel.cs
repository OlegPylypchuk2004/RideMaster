using LevelSystem;
using SessionSystem;
using System;
using UnityEngine;
using VehicleSystem.Part;
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

        private void OnPartButtonSelected(PartData partData)
        {
            if (partData.config == null || partData.count <= 0)
            {
                return;
            }

            PartConfigSelected?.Invoke(partData.config);
        }
    }
}