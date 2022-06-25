using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPositioning : MonoBehaviour
{
    [SerializeField] Transform XRCameraTransform;

    [SerializeField] GameObject hips;
    [SerializeField] float hipsOffsetHeight;

    [SerializeField] float[] lookDownPauseRotationRange = new float[2] { 45, 90 };

    // Update is called once per frame
    void Update()
    {
        if (!CanSeeHips()) UpdateHipsLocation();
    }

    bool CanSeeHips()
    {
        if (XRCameraTransform.eulerAngles.x > lookDownPauseRotationRange[0] && XRCameraTransform.eulerAngles.x < lookDownPauseRotationRange[1])
        {
            return true;
        }
        else return false;
    }

    void UpdateHipsLocation ()
    {
        // Translate position to match camera X, Y - offset, Z
        float newYPosition = Mathf.Clamp(XRCameraTransform.position.y - hipsOffsetHeight, 0.1f, XRCameraTransform.position.y);
        hips.transform.position = new Vector3(XRCameraTransform.position.x, newYPosition, XRCameraTransform.position.z);
        // Rotate to match camera
        hips.transform.eulerAngles = new Vector3(hips.transform.eulerAngles.x, XRCameraTransform.eulerAngles.y, hips.transform.eulerAngles.z);
    }
}
