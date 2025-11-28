using System;
using UnityEngine;
using Vehicle.Part.Configs;

namespace Vehicle.Part.UI
{
    public class PartButton : MonoBehaviour
    {
        [SerializeField] private PartConfig _partConfig;

        public event Action<PartConfig> Selected;

        private void OnMouseDown()
        {
            Selected?.Invoke(_partConfig);
        }
    }
}