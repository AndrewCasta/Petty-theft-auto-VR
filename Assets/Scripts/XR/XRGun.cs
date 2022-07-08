using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Basic Gun interactable
/// </summary>
/// 

[RequireComponent(typeof(IGun))]

public class XRGun : XRGrabVelocityTrackedInteractable
{
    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        var gun = GetComponent<IGun>();
        gun.Shoot();
    }
}
