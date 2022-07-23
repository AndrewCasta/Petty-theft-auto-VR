using UnityEngine;


public interface IDamageable
{
    void Damage(RaycastHit hit, float damage);
}