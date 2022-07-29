using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (firstInteractorSelecting is XRDirectInteractor)
        {
            attachTransform.position = firstInteractorSelecting.transform.position;
            attachTransform.rotation = firstInteractorSelecting.transform.rotation;

        }
        base.OnSelectEntered(args);
    }
}
