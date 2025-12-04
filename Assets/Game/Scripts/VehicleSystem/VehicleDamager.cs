using System.Linq;
using UnityEngine;
using VehicleSystem.Parts;

namespace VehicleSystem
{
    public class VehicleDamager : MonoBehaviour
    {
        [SerializeField] private Vehicle _vehicle;

        private void OnCollisionEnter(Collision collision)
        {
            foreach (ContactPoint contactPoint in collision.contacts)
            {
                if (contactPoint.thisCollider.TryGetComponent(out GameplayPart gameplayPart))
                {
                    if (_vehicle.Parts.Contains(gameplayPart))
                    {
                        float hitStrength = contactPoint.impulse.magnitude;
                        int damage = Mathf.RoundToInt(hitStrength);

                        gameplayPart.TakeDamage(damage);

                        Debug.Log($"{gameplayPart.name} damaged; damage: {damage}");
                    }
                }
            }
        }
    }
}