using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Vehicle.Part.Configs;

namespace Vehicle.Part.UI
{
    public class PartButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PartConfig _partConfig;

        public event Action<PartConfig> Selected;

        public void OnPointerDown(PointerEventData eventData)
        {
            Selected?.Invoke(_partConfig);
        }
    }
}