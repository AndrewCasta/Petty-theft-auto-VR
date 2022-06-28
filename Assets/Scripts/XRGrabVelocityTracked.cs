using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabVelocityTracked : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        SetParentToXRRig();
        base.OnSelectEntered(interactor);
    }

    protected override void OnSelectExited(SelectExitEventArgs interactor)
    {
        SetParentToWorld();
        base.OnSelectExited(interactor);
    }

    public void SetParentToXRRig()
    {
        transform.SetParent(interactorsSelecting[0].transform);
    }

    public void SetParentToWorld()
    {
        transform.SetParent(null);
    }
}