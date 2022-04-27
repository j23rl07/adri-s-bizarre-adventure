using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMovement : MonoBehaviour
{
    public float speed; //Velocidad de la plataforma
    public int startingPoint; //Posicion de la plataforma (al comienzo)
    public Transform[] points; //Posiciones de la plataforma

    private int i; //indice
    private SpriteRenderer sr;


    void Start()
    {

        transform.position = points[startingPoint].position;
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            sr.flipY = false;
            i++;
            if (i == points.Length)
            {
                i = 0;
                sr.flipY = true;

            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime); //posicion actual, posicion siguiente, velocidad

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
