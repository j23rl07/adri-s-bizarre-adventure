using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plattformRecorrido : MonoBehaviour
{
    public float speed; //Velocidad de la plataforma
    public int startingPoint; //Posicion de la plataforma (al comienzo)
    public Transform[] points; //Posiciones de la plataforma

    private int i; //indice

    private GameObject player;

    [SerializeField] private BoxCollider2D boxCollider;


    void Start()
    {
        transform.position = points[startingPoint].position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().isDead)
        {
            transform.position = points[0].position;
            this.gameObject.GetComponent<plattformRecorrido>().enabled = false;
            boxCollider.enabled = true;
            i = 0;
        }
        else if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                transform.position = points[0].position;
                this.gameObject.GetComponent<plattformRecorrido>().enabled = false;
                boxCollider.enabled = true;
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime); //posicion actual, posicion siguiente, velocidad
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<plattformRecorrido>().enabled = true;
            boxCollider.enabled = false;
        }
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
