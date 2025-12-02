using UnityEngine;

namespace TabSystem
{
    public class Tab : MonoBehaviour
    {
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}