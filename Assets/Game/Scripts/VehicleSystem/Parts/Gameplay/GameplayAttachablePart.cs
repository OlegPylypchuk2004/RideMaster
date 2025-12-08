using UnityEngine;

namespace VehicleSystem.Parts.Gameplay
{
    public abstract class GameplayAttachablePart : GameplayPart
    {
        private GameplayBasePart _basePart;

        private void OnDestroy()
        {
            if (_basePart != null)
            {
                _basePart.Destroyed -= OnBasePartDestroyed;
            }
        }

        public void SetBasePart(GameplayBasePart basePart, Vector2 direction)
        {
            if (basePart == null)
            {
                return;
            }

            _basePart = basePart;
            _basePart.Destroyed += OnBasePartDestroyed;

            transform.SetParent(_basePart.transform);

            if (this is GameplayPartSuspension)
            {
                if (direction == Vector2.up)
                {
                    transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else if (direction == Vector2.down)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, 0f));
                }
                else if (direction == Vector2.right)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                }
                else if (direction == Vector2.left)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
                }
            }
            else if (this is GameplayPartBooster)
            {
                if (direction == Vector2.right)
                {
                    transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else if (direction == Vector2.left)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(180f, 0f, 0f));
                }
                else if (direction == Vector2.up)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
                }
                else if (direction == Vector2.down)
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
                }
            }
        }

        private void OnBasePartDestroyed(GameplayPart gameplayPart)
        {
            if (gameplayPart == null || _basePart != gameplayPart)
            {
                return;
            }

            _basePart.Destroyed -= OnBasePartDestroyed;

            TakeDamage(Strength);
        }
    }
}