using UnityEngine;

public class DestroyedPart : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _radius;
    [SerializeField] private Rigidbody[] _rigidbodies;

    public void Explode()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.useGravity = true;
            rigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }
}