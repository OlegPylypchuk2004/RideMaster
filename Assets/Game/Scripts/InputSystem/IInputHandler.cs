namespace InputSystem
{
    public interface IInputHandler
    {
        public bool IsPerforming { get; }

        public void Update();
    }
}