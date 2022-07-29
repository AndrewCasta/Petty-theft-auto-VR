using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads the rotation of a wheel object and provides a -1 to 1 Value to control objects
/// Assumes the wheel is orientation 0 on the local y axis.
/// </summary>
public class WheelInput : MonoBehaviour
{
    public float Value { get; set; }

    [Tooltip("Value of local Y when turned to the right")]
    [SerializeField] float maxClockwiseRotation;
    [Tooltip("Value of local Y when turned to the left")]
    [SerializeField] float maxCounterClockwiseRotation;


    void Update()
    {
        Value = Mathf.Clamp(InputRotation(), -1, 1);
        Debug.Log(Value);
    }

    private float InputRotation()
    {
        if (transform.localEulerAngles.y > 0 && transform.localEulerAngles.y < 180)
        {
            return ConvertRange(transform.localEulerAngles.y, 0, maxClockwiseRotation, 0, 1);
        }
        if (transform.localEulerAngles.y > 180 && transform.localEulerAngles.y < 360)
        {
            return ConvertRange(transform.localEulerAngles.y, 360, maxCounterClockwiseRotation, 0, -1);
        }
        return 0;
    }
    private float ConvertRange(float inputValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        // (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin
        return (((inputValue - oldMin) * (newMax - newMin)) / (oldMax - oldMin)) + newMin;
    }
}
