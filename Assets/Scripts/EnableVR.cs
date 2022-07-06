using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVR : MonoBehaviour
{
    [SerializeField] GameObject flatPlyer;
    [SerializeField] GameObject xrOrigin;

    [SerializeField] bool enableVR;


    // Start is called before the first frame update
    void Awake()
    {
        if (enableVR)
        {
            flatPlyer.SetActive(false);
            xrOrigin.SetActive(true);
        }
        if (!enableVR)
        {
            flatPlyer.SetActive(true);
            xrOrigin.SetActive(false);
        }
    }
}
