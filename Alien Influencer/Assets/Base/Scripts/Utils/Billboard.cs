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
        //For world space UI that always faces camera, like building destruction progress bar
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
