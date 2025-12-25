using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace VehicleSystem.Parts
{
    [Serializable]
    public class PartData
    {
        [SerializeField] private PartConfig _config;
        [SerializeField, MinValue(0)] private int _count;

        public event Action<int> CountChanged;

        public PartData(PartConfig config, int count)
        {
            _config = config;
            _count = count;
        }

        public PartConfig Config => _config;

        public int Count
        {
            get => _count;
            set
            {
                if (value < 0)
                {
                    return;
                }

                _count = value;

                CountChanged?.Invoke(_count);
            }
        }
    }
}