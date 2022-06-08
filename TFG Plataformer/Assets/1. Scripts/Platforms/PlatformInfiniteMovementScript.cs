using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInfiniteMovementScript : MonoBehaviour
{
    public float speed; //Velocidad de la plataforma
    public int startingPoint; //Posicion de la plataforma (al comienzo)
    public Transform[] points; //Posiciones de la plataforma

    private int i; //indice


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[1].position) < 0.02f)
        {
            transform.position = points[0].position;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[1].position, speed * Time.deltaTime); //posicion actual, posicion siguiente, velocidad
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
