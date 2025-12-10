using UnityEngine;

namespace BridgeSystem
{
    public class BridgeSection : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Joint _joint;

        public Rigidbody Rigidbody => _rigidbody;
        public Joint Joint => _joint;
    }
}