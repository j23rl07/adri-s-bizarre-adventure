using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraController : MonoBehaviour
{
    [SerializeField] private int tamanyoCamara = 7;
    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 levelMinBounds;
    [SerializeField] private Vector3 levelMaxBounds;
    [SerializeField] private Vector3 initialOffset;
    [HideInInspector] public Vector3 offset, minValues, maxValues;
    [Range(1, 15)]
    [SerializeField] private float smoothFactor = 8;
    private int zCoord = -10;

    [Header("Camera Sections")]
    [SerializeField] private float transitionDuration = .5f;
    [Range(1, 15)]
    [SerializeField] private float transitionFactor = 8;

    [SerializeField] private GameObject currentSection;
    [Serializable]
    public class CameraSection
    {
        public GameObject section;
        public Vector3 minValues;
        public Vector3 maxValues;
        public Vector3 offset;
    }
    public List<CameraSection> cameraSections;

    [Header("Background")]
    [SerializeField] private GameObject background = null;



    void Start()
    {
        GetComponent<Camera>().orthographicSize = tamanyoCamara;
        levelMinBounds = new Vector3(levelMinBounds.x, levelMinBounds.y, zCoord);
        levelMaxBounds = new Vector3(levelMaxBounds.x, levelMaxBounds.y, zCoord);
        minValues = levelMinBounds;
        maxValues = levelMaxBounds;
        offset = initialOffset;
    }

    void FixedUpdate()
    {
        Follow();

        if (background != null)
        {
            background.transform.position = transform.position + new Vector3(0, 0, 1);
        }
    }


    ///////////////////////FUNCIONES AUXILIARES////////////////////////////
    void Follow()
    {
        //Añadir el offset
        Vector3 targetPosition = target.position + offset;
        //Limitar la posicion x,y,z de la camara relativa a los confines del nivel
        Vector3 boundPosition = new Vector3(
                Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
                Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
                Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z)
            );
        //Suavizar el movimiento de la camara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    public void ChangeCameraBounds(GameObject section)
    {
        foreach (CameraSection cameraSection in cameraSections)
        {
            if (cameraSection.section.Equals(section) && !section.Equals(currentSection))
            {
                currentSection = section;
                StartCoroutine(SmoothTransition());
                minValues = new Vector3(cameraSection.minValues.x, cameraSection.minValues.y, zCoord);
                maxValues = new Vector3(cameraSection.maxValues.x, cameraSection.maxValues.y, zCoord);
                offset = cameraSection.offset;
            }
        }
    }

    private IEnumerator SmoothTransition()
    {
        //Make the camera movement slower for the transition
        float storage = smoothFactor;
        smoothFactor = transitionFactor;
        yield return new WaitForSeconds(transitionDuration);
        smoothFactor = storage;
    }
}

