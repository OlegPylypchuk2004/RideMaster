using InputSystem;
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
        private IInputHandler _inputHandler;
        private HashSet<GameplayPart> _parts;

        public IReadOnlyList<GameplayPart> Parts => _parts.ToArray();

        [Inject]
        private void Construct(Road road, IInputHandler inputHandler)
        {
            transform.position = road.VehicleStartPoint;
            _inputHandler = inputHandler;
        }

        private void Awake()
        {
            _parts = new HashSet<GameplayPart>();
        }

        private void Update()
        {
            if (_inputHandler.IsBraking)
            {
                _rigidbody.linearVelocity = Vector3.zero;
            }
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
            _parts.Remove(part);
            part.Destroyed -= OnPartDestroyed;

            _rigidbody.mass -= part.PartConfig.Mass;
        }
    }
}