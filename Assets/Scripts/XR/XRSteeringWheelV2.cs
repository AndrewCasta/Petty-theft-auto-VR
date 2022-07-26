using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.XRContent.Interaction
{

    public class XRSteeringWheelV2 : XRGrabInteractable
    {
        public float Value { get; set; }

        HingeJoint hingeJoint;

        Transform originalParent;
        XRBaseController controller;

        [SerializeField] CarControllerPrometeo carController;


        protected override void Awake()
        {
            base.Awake();
            originalParent = transform.parent;
            hingeJoint = GetComponent<HingeJoint>();
            selectMode = InteractableSelectMode.Multiple;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (isSelected) hingeJoint.useMotor = false;
            if (firstInteractorSelecting is XRDirectInteractor)
            {
                attachTransform.position = firstInteractorSelecting.transform.position;
                attachTransform.rotation = firstInteractorSelecting.transform.rotation;

            }
            base.OnSelectEntered(args);


            transform.SetParent(originalParent); // Lock object to parent (for movement in car later)
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Value = 0;
            if (!isSelected) hingeJoint.useMotor = true;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (interactorsSelecting.Count == 2)
                ProcessTwoHandInteraction();
            base.ProcessInteractable(updatePhase);
        }

        private void ProcessTwoHandInteraction()
        {
            Vector3 directionBetweenHands = interactorsSelecting[1].transform.position - firstInteractorSelecting.transform.position;
            // This creates a horizontal line between the hands
            // Next I want to find a rotation from this & apply it to the wheel
            // Maybe rotate the firstinteractor, like the Gun?

            // When releasing, if first is released first, need to make the second now the first and swap the attachpoint?
        }

    }
}
