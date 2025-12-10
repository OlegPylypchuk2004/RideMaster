using UnityEngine;

namespace BridgeSystem
{
    public class Bridge : MonoBehaviour
    {
        [SerializeField] private Joint _startPoint;
        [SerializeField] private Rigidbody _endPoint;

        private BridgeSection[] _sections;

        private void Awake()
        {
            _sections = GetComponentsInChildren<BridgeSection>();

            for (int i = 0; i < _sections.Length; i++)
            {
                BridgeSection section = _sections[i];

                if (i == 0)
                {
                    _startPoint.connectedBody = section.Rigidbody;
                }

                if (section.TryGetComponent(out Joint joint))
                {
                    if (i < _sections.Length - 1)
                    {
                        section.Joint.connectedBody = _sections[i + 1].Rigidbody;
                    }
                    else
                    {
                        section.Joint.connectedBody = _endPoint;
                    }
                }
            }
        }
    }
}