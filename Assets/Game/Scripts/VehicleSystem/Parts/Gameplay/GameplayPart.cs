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
                Destroy(gameObject);

                Destroyed?.Invoke(this);
            }
        }
    }
}