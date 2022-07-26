using UnityEngine;


public interface IDamageable
{
    public void Damage(float damage, RaycastHit hit = new RaycastHit());
}