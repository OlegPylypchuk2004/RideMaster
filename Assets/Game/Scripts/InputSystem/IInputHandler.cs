namespace InputSystem
{
    public interface IInputHandler
    {
        public bool IsActive { get; }
        public bool IsPerforming { get; }

        public void SetActive(bool isActive);
        public void Update();
    }
}