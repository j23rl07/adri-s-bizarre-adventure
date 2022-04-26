using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeScript : MonoBehaviour
{
    [HideInInspector] public BasicCameraController basicCameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        basicCameraController.ChangeCameraBounds(collision);
    }
}
