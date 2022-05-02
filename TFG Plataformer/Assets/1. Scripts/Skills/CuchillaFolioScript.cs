using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuchillaFolioScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    [HideInInspector] public List<int> allowedLayerCollisions;

    private readonly int groundLayer = 6;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 3);
    }




    void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedLayerCollisions.Contains(collision.gameObject.layer) && collision.gameObject.layer != groundLayer)
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