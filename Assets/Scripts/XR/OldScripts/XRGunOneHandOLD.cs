using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Basic Gun interactable
/// </summary>
[RequireComponent(typeof(IGun))]
public class XRGunOneHand : XRGrabVelocityTrackedInteractable
{
    enum FireMode { Single, Auto }
    [SerializeField] FireMode fireMode;
    [SerializeField] float rateOfFire;

    GunController gun;
    IEnumerator shootAutoRoutine;

    protected override void Awake()
    {
        base.Awake();
        gun = GetComponent<IGun>() as GunController;
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        if (fireMode == FireMode.Single) gun.Shoot();
        if (fireMode == FireMode.Auto)
        {
            shootAutoRoutine = ShootAuto();
            StartCoroutine(shootAutoRoutine);
        }
    }
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        if (fireMode == FireMode.Auto) StopCoroutine(shootAutoRoutine);
    }

    IEnumerator ShootAuto()
    {
        while (true)
        {
            gun.Shoot();
            yield return new WaitForSeconds(1 / rateOfFire);
        }

    }
}
