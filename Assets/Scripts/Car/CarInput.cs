using System.Collections;
using System.Collections.Generic;
using Unity.XRContent.Interaction;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CarInput : MonoBehaviour
{
    [SerializeField] ICar carController;

    [Tooltip("Object to read Steering input rotation value from")]
    [SerializeField] GameObject steeringWheel;
    [Tooltip("Object to Forward/Reverse input value from")]
    [SerializeField] GameObject gearBox;
    [Tooltip("Object to read Brake input value from")]
    [SerializeField] GameObject handBrake;
}
