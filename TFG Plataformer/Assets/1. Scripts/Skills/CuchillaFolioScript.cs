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

            if (collision.GetComponent<ColumnaLogic>() != null)
            {
                collision.GetComponent<ColumnaLogic>().TakeDamage(damage);
            }

            if (collision.GetComponent<BossHealth>() != null)
            {
                collision.GetComponent<BossHealth>().TakeDamage(damage);
            }

            if (collision.GetComponent<GolemBossHealth>() != null)
            {
                collision.GetComponent<GolemBossHealth>().TakeDamage(damage);
            }

            if (collision.GetComponent<StatueHealth>() != null)
            {
                collision.GetComponent<StatueHealth>().TakeDamage(damage);
            }

            if (collision.GetComponent<bossHealthR>() != null)
            {
                collision.GetComponent<bossHealthR>().TakeDamage(damage);
            }

            if (collision.GetComponent<MinionHealth>() != null)
            {
                collision.GetComponent<MinionHealth>().TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
