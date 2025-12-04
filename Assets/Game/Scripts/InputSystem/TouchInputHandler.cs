using UnityEngine;

namespace InputSystem
{
    public class TouchInputHandler : IInputHandler
    {
        public bool IsActive { get; private set; }
        public bool IsPerforming { get; private set; }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void Update()
        {
            if (IsActive)
            {
                IsPerforming = Input.GetMouseButton(0);
            }
            else
            {
                IsPerforming = false;
            }
        }
    }
}