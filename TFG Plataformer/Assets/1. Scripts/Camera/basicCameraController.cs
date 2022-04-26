using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraController : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [Range(1, 15)]
    [SerializeField] private float smoothFactor = 8;

    [Header("Camera Sections")]
    [SerializeField] private Vector3 levelMinBounds;
    [SerializeField] private Vector3 levelMaxBounds;
    [SerializeField] private float transitionDuration = .5f;
    [Range(1, 15)]
    [SerializeField] private float transitionFactor = 8;
    private Vector3 minValues, maxValues;
    private bool isChanged = false;
    private Collider2D pendingCollision = null;

    [Serializable]
    public class CameraChange
    {
        public GameObject start;
        public GameObject end;
        public Vector3 minValues;
        public Vector3 maxValues;
    }
    public List<CameraChange> cameraChanges = new();

    [Header("Background")]
    [SerializeField] private GameObject background = null;


    private void Start()
    {
        GetComponent<Camera>().orthographicSize = 7;
        levelMinBounds = new Vector3(levelMinBounds.x, levelMinBounds.y, -10);
        levelMaxBounds = new Vector3(levelMaxBounds.x, levelMaxBounds.y, -10);
        minValues = levelMinBounds;
        maxValues = levelMaxBounds;

        target.GetComponent<CameraChangeScript>().basicCameraController = this;
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
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void ChangeCameraBounds(Collider2D collision)
    {
        //Cambiar los limites si se encuentra dentro de algún tramo de cambio de cámara
        foreach (CameraChange cameraChange in cameraChanges)
        {
            Collider2D startCollider = cameraChange.start.GetComponent<Collider2D>();
            //Para entrar en un nuevo tramo no puedes estar ya dentro de uno anterior
            if (collision.Equals(startCollider) & !isChanged)
            {
                StartCoroutine(SmoothTransition());
                minValues = new Vector3(cameraChange.minValues.x, cameraChange.minValues.y, -10);
                maxValues = new Vector3(cameraChange.maxValues.x, cameraChange.maxValues.y, -10);
                isChanged = true;
                Collider2D endCollider = cameraChange.end.GetComponent<Collider2D>();
                pendingCollision = endCollider;
                break;
            }
            else if (collision.Equals(pendingCollision) & isChanged)
            {
                minValues = new Vector3(levelMinBounds.x, levelMinBounds.y, -10);
                maxValues = new Vector3(levelMaxBounds.x, levelMaxBounds.y, -10);
                isChanged = false;
                pendingCollision = null;
                break;
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
