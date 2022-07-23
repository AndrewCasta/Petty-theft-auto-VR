using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    int Health { get; set; }
    bool IsAlive { get; set; }

    void Die(RaycastHit hit = new RaycastHit());
}
