using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Unity.XRContent.Interaction
{
    /// <summary>
    /// Two hand grabbable wheel that is rotated in place
    /// Uses a hingle joint to precent movement when grabbing
    /// </summary>
    public class XRSteeringWheelV2 : XROffsetGrabInteractable
    {
        HingeJoint hinge;

        [SerializeField] bool rotateBackToZero;
        [SerializeField] float rotationSpeed;
        Quaternion inititalRotation;

        Transform originalParent;


        protected override void Awake()
        {
            base.Awake();
            originalParent = transform.parent;
            hinge = GetComponent<HingeJoint>();
            selectMode = InteractableSelectMode.Multiple;
            inititalRotation = transform.localRotation;
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (isSelected) hinge.useMotor = false;
            base.OnSelectEntered(args);
            transform.SetParent(originalParent); // Lock object to parent (for movement in car later)
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if (!isSelected) hinge.useMotor = true;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            if (interactorsSelecting.Count == 1)
                firstInteractorSelecting.transform.LookAt(transform.position);
            if (interactorsSelecting.Count == 2)
                ProcessTwoHandInteraction();
            if (interactorsSelecting.Count == 0 && rotateBackToZero)
                RotateWheelToZero();
        }

        private void ProcessTwoHandInteraction()
        {
            Vector3 direction = interactorsSelecting[1].transform.position - firstInteractorSelecting.transform.position;
            firstInteractorSelecting.transform.rotation = Quaternion.LookRotation(direction);
        }

        void RotateWheelToZero()
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, inititalRotation, rotationSpeed);
        }

    }
}
