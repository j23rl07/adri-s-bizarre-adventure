using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCameraController : MonoBehaviour
{
    [Header("Attached")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [Header("Background")]
    [SerializeField] private GameObject background = null;


    private void Start()
    {
        GetComponent<Camera>().orthographicSize = 7;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, offset.y, offset.z);

        if(background != null)
        {
            background.transform.position = transform.position + new Vector3(0, 0, 1);
        }
    }
}
