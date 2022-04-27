using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    [HideInInspector] public List<Collider2D> allowedCollisions;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 3);
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
