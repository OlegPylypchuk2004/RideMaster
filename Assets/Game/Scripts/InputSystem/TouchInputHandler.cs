using UnityEngine;

namespace InputSystem
{
    public class TouchInputHandler : IInputHandler
    {
        public bool IsActive { get; set; }
        public bool IsAccelerating { get; private set; }
        public bool IsBraking { get; private set; }

        public void Update()
        {
            if (IsActive)
            {
                IsAccelerating = Input.GetMouseButton(0);
                IsBraking = Input.GetMouseButton(1);
            }
            else
            {
                IsAccelerating = false;
                IsBraking = false;
            }
        }
    }
}