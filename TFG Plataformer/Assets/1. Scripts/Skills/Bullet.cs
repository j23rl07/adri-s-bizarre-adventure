using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 40;
    public GameObject impactEffect;
    public List<int> allowedLayerCollisions;


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 3);
    }

    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("MyCoin");
        GameObject[] silverCoin = GameObject.FindGameObjectsWithTag("SilverCoin");
        GameObject[] water = GameObject.FindGameObjectsWithTag("Water");
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (GameObject obj in projectiles)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (GameObject obj in water)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (GameObject obj in silverCoin)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowedLayerCollisions.Contains(collision.gameObject.layer))
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
