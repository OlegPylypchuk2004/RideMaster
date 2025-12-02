using System.Collections.Generic;
using UnityEngine;
using VehicleSystem.Parts;

namespace VehicleSystem
{
    public class Vehicle : MonoBehaviour
    {
        private HashSet<GameplayPart> _gameplayParts;

        private void Awake()
        {
            _gameplayParts = new HashSet<GameplayPart>();
        }

        public void AddPart(GameplayPart gameplayPart)
        {
            _gameplayParts.Add(gameplayPart);
        }
    }
}