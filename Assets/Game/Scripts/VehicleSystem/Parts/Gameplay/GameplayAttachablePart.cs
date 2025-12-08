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

        public void SetBasePart(GameplayBasePart basePart)
        {
            if (basePart == null)
            {
                return;
            }

            _basePart = basePart;
            _basePart.Destroyed += OnBasePartDestroyed;

            transform.SetParent(_basePart.transform);
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