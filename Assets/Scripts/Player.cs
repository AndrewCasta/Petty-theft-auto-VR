using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public void Damage (RaycastHit hit, float damage)
    {
        Debug.Log("Player hit");
    }
}
