using System.Collections;
using System.Collections.Generic;
using Unity.XRContent.Interaction;
using UnityEngine;

public class CarXR : MonoBehaviour
{
    [SerializeField] XRKnob wheel;
    [SerializeField] XRLever throttle;

    [SerializeField] List<AxleInfo> axleInfos;
    [SerializeField] float maxMotorTorque;
    [SerializeField] float maxSteeringAngle;
    [SerializeField] float brakeTorque;
    bool isBraking;
    [SerializeField] Transform centerOfMass;

    private void Start()
    {
        // GetComponent<Rigidbody>().centerOfMass = centerOfMass.position;
    }


    private void FixedUpdate()
    {
        //if (throttle.Value) carController.InputForward();
        //if (!throttle.Value) carController.InputReverse();
        //if (wheel.InputValue > 0f) carController.TurnRight();
        //if (wheel.InputValue < 0f) carController.TurnLeft();


        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(KeyCode.Space);
        float braking = isBraking ? brakeTorque : 0f;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;

            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            if (axleInfo.braking)
            {
                axleInfo.leftWheel.brakeTorque = braking;
                axleInfo.rightWheel.brakeTorque = braking;
            }

        }
    }
}