using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Basic Gun interactable
/// </summary>
[RequireComponent(typeof(IGun))]
public class XRGun : XRGrabVelocityTrackedInteractable
{
    enum RotatingInteractor { First, Second }
    [SerializeField] RotatingInteractor rotatingInteractor = RotatingInteractor.First;
    [SerializeField] bool isTwoHanded;

    GunController gunController;
    IEnumerator shootAutoRoutine;

    protected override void Awake()
    {
        base.Awake();
        gunController = GetComponent<IGun>() as GunController;
        selectMode = InteractableSelectMode.Multiple;
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
        if (gunController.fireMode == GunController.FireMode.Single) gunController.Shoot();
        if (gunController.fireMode == GunController.FireMode.Auto)
        {
            shootAutoRoutine = ShootAuto();
            StartCoroutine(shootAutoRoutine);
        }
    }
    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        base.OnDeactivated(args);
        if (gunController.fireMode == GunController.FireMode.Auto) StopCoroutine(shootAutoRoutine);
    }

    IEnumerator ShootAuto()
    {
        while (true)
        {
            gunController.Shoot();
            yield return new WaitForSeconds(1 / gunController.rateOfFire);
        }

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (interactorsSelecting.Count == 2 && isTwoHanded)
            ProcessTwoHandInteraction();
        base.ProcessInteractable(updatePhase);
    }
    void ProcessTwoHandInteraction()
    {
        Vector3 direction = interactorsSelecting[1].transform.position - firstInteractorSelecting.transform.position;
        Vector3 upDirection = interactorsSelecting[(int)rotatingInteractor].transform.up;
        firstInteractorSelecting.transform.rotation = Quaternion.LookRotation(direction, upDirection);
    }

    protected override void Grab()
    {
        if (interactorsSelecting.Count == 1) base.Grab();
    }

    protected override void Drop()
    {
        if (!isSelected) base.Drop();
    }
}
