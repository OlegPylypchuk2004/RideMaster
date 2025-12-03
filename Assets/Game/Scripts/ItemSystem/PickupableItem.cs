using System;
using UnityEngine;

namespace ItemSystem
{
    public class PickupableItem : MonoBehaviour
    {
        public event Action<PickupableItem> Triggered;

        protected virtual void OnTriggerEnter(Collider other)
        {
            Triggered?.Invoke(this);
        }
    }
}