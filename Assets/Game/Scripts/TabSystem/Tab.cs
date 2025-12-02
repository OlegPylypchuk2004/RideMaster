using UnityEngine;

namespace TabSystem
{
    public class Tab : MonoBehaviour
    {
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