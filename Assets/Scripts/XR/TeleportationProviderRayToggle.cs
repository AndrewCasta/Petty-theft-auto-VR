using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationProviderRayToggle : TeleportationProvider
{
    [SerializeField] bool canToggleLeftRay;
    [SerializeField] bool canToggleRightRay;
    [SerializeField] InputActionReference leftToggle;
    [SerializeField] InputActionReference rightToggle;
    [SerializeField] GameObject leftRay;
    [SerializeField] GameObject rightRay;


    void Start()
    {
        leftToggle.action.started += LeftToggle;
        leftToggle.action.canceled += LeftToggle;

        rightToggle.action.started += RightToggle;
        rightToggle.action.canceled += RightToggle;
    }

    void LeftToggle(InputAction.CallbackContext context)
    {
        if (canToggleLeftRay) leftRay.SetActive(!leftRay.activeSelf);
    }
    void RightToggle(InputAction.CallbackContext context)
    {
        if (canToggleRightRay) rightRay.SetActive(!rightRay.activeSelf);
    }
}
