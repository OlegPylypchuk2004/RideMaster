using UnityEngine;

namespace InputSystem
{
    public class TouchInputHandler : IInputHandler
    {
        public bool IsActive { get; set; }
        public bool IsAccelerating => IsActive && Input.GetMouseButton(0);
        public bool IsBraking => !IsActive || !IsAccelerating;
    }
}