using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Uses the basic GrabInteractable
/// Changes between instantaneous (smoother tracking) & velocity tracked (physics collisions) when trigger collider hits an object.
/// Nests nest the interacable on the interactor during a grab, to reduce jitter during velocity tracked movement.
/// </summary>
/// 

[RequireComponent(typeof(Collider))]
public class XRGrabVelocityTrackedInteractable : XRGrabInteractable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<XRBaseControllerInteractor>() == null)
        {
            movementType = MovementType.VelocityTracking;
            SetSmoothing(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<XRBaseControllerInteractor>() == null)
        {
            movementType = MovementType.Instantaneous;
            SetSmoothing(false);
        }

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        transform.SetParent(interactorsSelecting[0].transform);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        transform.SetParent(null);
        base.OnSelectExited(args);
    }

    private void SetSmoothing(bool state)
    {
        smoothPosition = state;
        smoothRotation = state;
    }
}