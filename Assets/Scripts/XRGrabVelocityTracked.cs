using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabVelocityTracked : XRGrabInteractable
{
    Transform originalParent;

    protected override void Awake()
    {
        base.Awake();
        originalParent = transform.parent;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        SetParentToInteractor();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(SelectExitEventArgs interactor)
    {
        SetParentToOriginal();
        base.OnSelectExited(interactor);
    }

    public void SetParentToInteractor()
    {
        transform.SetParent(interactorsSelecting[0].transform);
    }

    public void SetParentToOriginal()
    {
        transform.SetParent(originalParent);
    }
}