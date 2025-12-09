using UnityEngine;

namespace RoadSystem
{
    public class FinishFlag : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Finish flag triggered");
        }
    }
}