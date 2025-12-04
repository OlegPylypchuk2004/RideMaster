using UnityEngine;
using VehicleSystem.Parts.Preview;

namespace VehicleSystem.Building
{
    public class PreviewPartHolder : MonoBehaviour
    {
        [SerializeField] private Vector3 _partLocalPosition;

        public PreviewPart Part { get; private set; }

        public void SetPart(PreviewPart part)
        {
            if (part == null)
            {
                return;
            }

            Part = part;
            Part.transform.SetParent(transform);
            Part.transform.localPosition = _partLocalPosition;
        }

        public void ResetPart()
        {
            Part = null;
        }
    }
}