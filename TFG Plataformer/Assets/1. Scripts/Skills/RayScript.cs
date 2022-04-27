using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    [Header("Sin")]
    public float bulletSpeed = 5.0f;
    public float frecuencia = 20.0f;  // Velocidad a lo largo del seno
    public float magnitud = 0.5f;   // Tamaño del movimiento en el seno
    private Vector3 eje;
    private Vector3 pos;

    [Header("Bullet")]
    public int damage = 40;
    public GameObject impactEffect;
    [HideInInspector] public List<Collider2D> allowedCollisions;

    void Start()
    {
        pos = transform.position;
        DestroyObject(gameObject, 5);
        eje = transform.right;  

    }

    //Aplicamos el seno a la bala
    void Update()
    {
        pos += transform.up * Time.deltaTime * bulletSpeed;
        transform.position = pos + eje * Mathf.Sin(Time.time * frecuencia) * magnitud;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedCollisions.Contains(collision))
        {
            if (collision.GetComponent<EnemyHealth>() != null)
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
