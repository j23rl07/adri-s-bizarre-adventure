using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSectionScript : MonoBehaviour
{
    [SerializeField] private BasicCameraController basicCameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            basicCameraController.ChangeCameraBounds(gameObject.transform.parent.gameObject);
    }
}
