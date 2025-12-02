using RoadSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VehicleSystem.Parts;
using Zenject;

namespace VehicleSystem
{
    public class Vehicle : MonoBehaviour
    {
        private HashSet<GameplayPart> _parts;

        public IReadOnlyList<GameplayPart> Parts => _parts.ToArray();

        [Inject]
        private void Construct(Road road)
        {
            transform.position = road.VehicleStartPoint;
        }

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