using System;
using UnityEngine;

namespace VehicleSystem.Parts
{
    public abstract class GameplayPart : MonoBehaviour
    {
        [SerializeField] private PartConfig _partConfig;

        private int _strength;

        public event Action<int> Damaged;
        public event Action<GameplayPart> Destroyed;

        public PartConfig PartConfig => _partConfig;

        public int Strength
        {
            get => _strength;
            private set
            {
                _strength = Mathf.Max(0, value);
            }
        }

        private void Awake()
        {
            Strength = _partConfig.Strength;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Mathf.Min(damage, Strength);
            Strength -= actualDamage;

            Damaged?.Invoke(actualDamage);

            if (Strength <= 0)
            {
                SpawnDestroyPart();
                Destroy(gameObject);

                Destroyed?.Invoke(this);
            }
        }

        private void SpawnDestroyPart()
        {
            if (_partConfig.DestroyPrefab == null)
            {
                return;
            }

            DestroyedPart destroyedPart = Instantiate(_partConfig.DestroyPrefab, transform.position, transform.rotation);
            destroyedPart.Explode();
        }
    }
}