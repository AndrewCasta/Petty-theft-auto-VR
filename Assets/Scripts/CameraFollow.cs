using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followObject;
    [SerializeField] Vector3 offset;


    void Update()
    {
        transform.position = followObject.transform.position + offset;
        transform.rotation = followObject.transform.rotation;
    }
}
