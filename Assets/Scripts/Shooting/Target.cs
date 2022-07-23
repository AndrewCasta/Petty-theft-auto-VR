using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject impactEffect;

    public void Damage (RaycastHit hit, float damage)
    {
        GameObject impactParticle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactParticle, 1f);
    }
}
