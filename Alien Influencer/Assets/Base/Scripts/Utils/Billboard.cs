using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform camTransform;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
