using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitHandler : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnHit;

    public void CallHitMethod()
    {
        OnHit.Invoke();
    }
}
