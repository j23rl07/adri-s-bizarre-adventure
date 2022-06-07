using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    public List<int> allowedLayerCollisions;


    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedLayerCollisions.Contains(collision.gameObject.layer))
        {
            if (collision.GetComponent<Player>() != null)
            {
                collision.GetComponent<Player>().TakeDamage(damage);
            }
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}

