namespace VehicleSystem.Parts.Gameplay
{
    public abstract class GameplayAttachablePart : GameplayPart
    {
        private GameplayBasePart _basePart;

        public void SetBasePart(GameplayBasePart basePart)
        {
            if (basePart == null)
            {
                return;
            }

            _basePart = basePart;
            transform.SetParent(_basePart.transform);
        }
    }
}