using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Basic Gun interactable
/// </summary>
[RequireComponent(typeof(IGun))]
public class XRGunTwoHandOLD : XRGrabVelocityTrackedInteractable
{

    enum FireMode { Single, Auto }
    [SerializeField] FireMode fireMode;
    [SerializeField] float rateOfFire;

    GunController gun;
    IEnumerator shootAutoRoutine;

    [SerializeField] Transform secondAttachTransform;
    MovementType currentMovementType;

    // Two Handed Grip variables
    //Transform firstAttach;
    //Transform firstHand;
    //Transform secondAttach;
    //Transform secondHand;

    protected override void Awake()
    {
        base.Awake();
        gun = GetComponent<IGun>() as GunController;
        selectMode = InteractableSelectMode.Multiple;
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

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (interactorsSelecting.Count == 1)
        {
            base.ProcessInteractable(updatePhase);
        }
        else if (interactorsSelecting.Count == 2)
        {
            ProcessTwoHandedInteractable(updatePhase);
        }
    }

    private void ProcessTwoHandedInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        // Currently, this just uses transform.SetPositionAndRotation on a two handed grab, which ignores physics.
        // Looking at the XRGrabInteractable, they use transform.position/rotation for PerformInstantaneousUpdate, and calculate the velocity for PerformVelocityTrackingUpdate, which I can replicate here.

        // https://circuitstream.com/blog/two-handed-interactions-with-unity-xr-interaction-toolkit/

        // These could be pulled out to OnSelected for optimisation?
        Transform firstAttach = GetAttachTransform(null);
        Transform firstHand = interactorsSelecting[0].transform;
        Transform secondAttach = secondAttachTransform;
        Transform secondHand = interactorsSelecting[1].transform;

        // Get rotation between attach points
        Vector3 directionBetweenAttaches = secondAttach.position - firstAttach.position;
        Quaternion rotationFromAttachToForward = Quaternion.FromToRotation(directionBetweenAttaches, transform.forward);

        // Get the rotation between the hands
        Vector3 directionBetweenHands = secondHand.position - firstHand.position;
        Quaternion rotationHandAttachToUp = Quaternion.LookRotation(directionBetweenHands, firstHand.up);

        Quaternion targetRotation = rotationFromAttachToForward * rotationHandAttachToUp;

        // Get the position of the object, relative to position of the first hand (recreating attach point logic)
        Vector3 worldDirectionFromHandleToBase = transform.position - firstAttach.position;
        Vector3 localDirectionFromHandleToBase = transform.InverseTransformDirection(worldDirectionFromHandleToBase);
        Vector3 targetPosition = firstHand.position + targetRotation * localDirectionFromHandleToBase;

        transform.SetPositionAndRotation(targetPosition, targetRotation);

        // This is heavily cut down from the XRGrabInteractable ProcessInteractable, I may need to add more steps here
        switch (updatePhase)
        {
            case XRInteractionUpdateOrder.UpdatePhase.Fixed:
                if (isSelected)
                {
                    if (movementType == MovementType.VelocityTracking)
                        PerformTwoHandVelocityTrackingUpdate();
                }
                break;

            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
                if (isSelected)
                {
                    if (movementType == MovementType.Instantaneous)
                        PerformTwoHandInstantaneousUpdate();
                }

                break;
        }
    }

    void PerformTwoHandInstantaneousUpdate()
    {
        // To be implemented
        Debug.Log("InstantaneousUpdate");
    }
    void PerformTwoHandVelocityTrackingUpdate()
    {
        // To be implemented
        Debug.Log("VelocityTrackingUpdate");
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
