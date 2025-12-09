using System;
using UnityEngine;
using VehicleSystem.Parts.Gameplay;

namespace RoadSystem
{
    public class FinishFlag : MonoBehaviour
    {
        public event Action<GameplayBasePart> VehicleBasePartTriggered;

        private void OnTriggerEnter(Collider other)
        {
            GameplayBasePart gameplayBasePart = other.gameObject.GetComponent<GameplayBasePart>();

            if (gameplayBasePart != null)
            {
                VehicleBasePartTriggered?.Invoke(gameplayBasePart);
            }
        }
    }
}