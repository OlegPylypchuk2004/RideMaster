using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VehicleSystem.Parts;

namespace VehicleSystem
{
    public class Vehicle : MonoBehaviour
    {
        private HashSet<GameplayPart> _parts;

        public IReadOnlyList<GameplayPart> Parts => _parts.ToArray();

        private void Awake()
        {
            _parts = new HashSet<GameplayPart>();
        }

        public void AddPart(GameplayPart part)
        {
            _parts.Add(part);
        }
    }
}