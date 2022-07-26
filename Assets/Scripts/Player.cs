using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public void Damage(float damage, RaycastHit hit = new RaycastHit())
    {
        Debug.Log("Player hit");
    }
}
