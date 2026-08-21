using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void ExplodeInRadius(Vector3 explosionPosition, float explosionRadius, float explosionForce)
    {
        foreach (Rigidbody Rigidbody in GetRigidbodiesInRadius(explosionPosition, explosionRadius))
        {
            Rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }

    private List<Rigidbody> GetRigidbodiesInRadius(Vector3 explosionPosition, float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(explosionPosition, explosionRadius);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                rigidbodies.Add(hit.attachedRigidbody);
            }
        }

        return rigidbodies;
    }
}