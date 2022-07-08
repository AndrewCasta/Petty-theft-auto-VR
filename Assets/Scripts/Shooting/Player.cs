using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public void Damage (RaycastHit hit, int damage)
    {
        Debug.Log("Player hit");
    }
}
