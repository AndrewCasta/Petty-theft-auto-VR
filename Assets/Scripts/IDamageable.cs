using UnityEngine;


public interface IDamageable
{
    void Damage(RaycastHit hit, int damange);
}