using UnityEngine;

namespace InputSystem
{
    public class TouchInputHandler : IInputHandler
    {
        public bool IsPerforming { get; private set; }

        public void Update()
        {
            IsPerforming = Input.GetMouseButton(0);
        }
    }
}