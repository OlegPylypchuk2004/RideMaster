namespace InputSystem
{
    public interface IInputHandler
    {
        public bool IsActive { get; set; }
        public bool IsAccelerating { get; }
        public bool IsBraking { get; }

        public void Update();
    }
}