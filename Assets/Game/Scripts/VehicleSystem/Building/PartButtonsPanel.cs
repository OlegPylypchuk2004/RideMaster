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
            PartData[] copy = CreatePartDatasCopy();

            for (int i = 0; i < _partButtons.Length; i++)
            {
                if (i < copy.Length)
                {
                    _partButtons[i].SetPartData(copy[i]);
                    _partButtons[i].Selected += OnPartButtonPressed;
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
                _partButtons[i].Selected -= OnPartButtonPressed;
            }
        }

        public bool IsAllButtonsAreEmpty()
        {
            foreach (PartButton partButton in _partButtons)
            {
                if (partButton == null || partButton.PartData == null)
                {
                    continue;
                }

                if (partButton.PartData.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void ReturnPart(PartConfig partConfig)
        {
            if (partConfig == null)
            {
                return;
            }

            foreach (PartButton button in _partButtons)
            {
                PartData partData = button.PartData;

                if (partData == null)
                {
                    continue;
                }

                if (string.Equals(partData.Config.ID, partConfig.ID))
                {
                    partData.Count++;
                    return;
                }
            }
        }

        public void ResetButtons()
        {
            foreach (PartData sourcePart in _levelConfig.PartDatas)
            {
                foreach (PartButton button in _partButtons)
                {
                    PartData targetPart = button.PartData;

                    if (targetPart == null)
                    {
                        continue;
                    }

                    if (targetPart.Config == null)
                    {
                        continue;
                    }

                    if (string.Equals(sourcePart.Config.ID, targetPart.Config.ID))
                    {
                        targetPart.Count = sourcePart.Count;
                        break;
                    }
                }
            }
        }

        private PartData[] CreatePartDatasCopy()
        {
            PartData[] source = _levelConfig.PartDatas;
            PartData[] copy = new PartData[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                copy[i] = new PartData(source[i].Config, source[i].Count);
            }

            return copy;
        }

        private void OnPartButtonPressed(PartButton partButton)
        {
            if (partButton == null)
            {
                return;
            }

            PartData data = partButton.PartData;

            if (data == null)
            {
                return;
            }

            if (data.Config == null)
            {
                return;
            }

            if (data.Count <= 0)
            {
                return;
            }

            data.Count--;

            PartConfigSelected?.Invoke(data.Config);
        }
    }
}