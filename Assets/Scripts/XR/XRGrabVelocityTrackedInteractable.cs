using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Uses the basic GrabInteractable
/// Changes between instantaneous (smoother tracking) & velocity tracked (physics collisions) when trigger collider
/// hits an object.
/// Nests nest the interacable on the interactor during a grab, to reduce jitter during velocity tracked movement.
/// </summary>
/// 

[RequireComponent(typeof(Collider))]
public class XRGrabVelocityTrackedInteractable : XRGrabInteractable
{
    [SerializeField] bool setBackToParent;
    Transform originalParent;

    protected override void Awake()
    {
        base.Awake();
        originalParent = transform.parent;
    }

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

    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        base.OnSelectEntered(interactor);
        transform.SetParent(interactorsSelecting[0].transform);
    }

    protected override void OnSelectExited(SelectExitEventArgs interactor)
    {
        if (setBackToParent) 
        {
            transform.SetParent(originalParent);
        } else
        {
            transform.SetParent(null);
        }
        base.OnSelectExited(interactor);
    }

    private void SetSmoothing(bool state)
    {
        smoothPosition = state;
        smoothRotation = state;
    }
}