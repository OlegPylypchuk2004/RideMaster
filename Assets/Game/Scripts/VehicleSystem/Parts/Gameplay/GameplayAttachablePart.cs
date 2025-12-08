using UnityEngine;

namespace VehicleSystem.Parts.Gameplay
{
    public abstract class GameplayAttachablePart : GameplayPart
    {
        [SerializeField] private AttachablePartDefaultDirection _defaultDirection;

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

            switch (_defaultDirection)
            {
                case AttachablePartDefaultDirection.Horizontal:
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
                    break;

                case AttachablePartDefaultDirection.Vertical:
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
                    break;
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