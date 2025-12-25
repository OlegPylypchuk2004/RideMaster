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
        [SerializeField] private Rigidbody _rigidbody;

        private HashSet<GameplayPart> _parts;
        private int _destroyedPartsCount;

        public IReadOnlyList<GameplayPart> Parts => _parts.ToArray();
        public int PartsCount => _parts.Count;
        public int DestroyedPartsCount => _destroyedPartsCount;

        [Inject]
        private void Construct(Road road)
        {
            transform.position = road.VehicleStartPoint;
        }

        private void Awake()
        {
            _parts = new HashSet<GameplayPart>();
        }

        private void OnDestroy()
        {
            foreach (GameplayPart part in _parts)
            {
                part.Destroyed -= OnPartDestroyed;
            }
        }

        public void AddPart(GameplayPart part)
        {
            _parts.Add(part);
            part.Destroyed += OnPartDestroyed;

            _rigidbody.mass += part.PartConfig.Mass;
        }

        private void OnPartDestroyed(GameplayPart part)
        {
            _destroyedPartsCount++;

            _parts.Remove(part);
            part.Destroyed -= OnPartDestroyed;

            _rigidbody.mass -= part.PartConfig.Mass;
        }
    }
}