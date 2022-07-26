using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.XRContent.Interaction
{

    public class XRSteeringWheelV1 : XRGrabInteractable
    {
        public float Value { get; set; }
        Transform originalParent;
        XRBaseController controller;

        [SerializeField] CarControllerPrometeo carController;


        protected override void Awake()
        {
            base.Awake();
            originalParent = transform.parent;

        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            transform.SetParent(originalParent);
            var controllerInteractor = args.interactorObject as XRBaseControllerInteractor;
            controller = controllerInteractor.xrController;

        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Value = 0;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            Value = InputRotation();

            // Simple steering input
            if (isSelected && Value > 0.2) carController.TurnRight();
            if (isSelected && Value < -0.2) carController.TurnLeft();


            // Basic forward/reverse method
            if (isSelected && controller != null)
            {
                if (controller.name == "RightHand Direct") carController.GoForward();
                if (controller.name == "LeftHand Direct") carController.GoReverse();
            }

            // Rotate wheel back to 0
            if (!isSelected)
            {
                // To implement
            }
        }
        private float InputRotation()
        {
            // (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin
            if (transform.localEulerAngles.y > 0 && transform.localEulerAngles.y < 180)
            {
                return (((transform.localEulerAngles.y - 0) * (1 - 0)) / (180 - 0)) + 0;
            }
            if (transform.localEulerAngles.y > 180 && transform.localEulerAngles.y < 360)
            {
                return (((transform.localEulerAngles.y - 360) * (-1 - 0)) / (180 - 360)) + 0;
            }
            return 0;
        }

    }
}
