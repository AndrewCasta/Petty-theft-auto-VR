using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject desktopCharacter;
    public bool launchVR = true;

    void Start()
    {
        if (launchVR)
        {
            xrOrigin.SetActive(true);
            desktopCharacter.SetActive(false);
        } else
        {
            xrOrigin.SetActive(false);
            desktopCharacter.SetActive(true);
        }
    }


}
