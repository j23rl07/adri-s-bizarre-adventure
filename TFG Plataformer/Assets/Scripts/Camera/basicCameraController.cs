using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    float cameraWidth;
    float cameraHeight;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, offset.y, offset.z);
    }
}
