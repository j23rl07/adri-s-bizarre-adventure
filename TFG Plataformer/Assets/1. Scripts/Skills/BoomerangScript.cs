using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    [SerializeField] private float force = 20f;
    [SerializeField] private float pullForce = 0.1f;
    [SerializeField] private int damage = 40;

    private Rigidbody2D rb;
    private bool returning = false;
    private bool canTakeDamage = true;
    private GameObject player;
    [HideInInspector] public List<int> allowedLayerCollisions;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * force, ForceMode2D.Impulse);
        if(rb.velocity.x < 0)
        {
            pullForce = -pullForce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) <= force)
        {
            rb.velocity = new Vector2(rb.velocity.x - pullForce,0);
        }
        if(rb.velocity.x < 1 & rb.velocity.x > -1)
        {
            returning = true;
        }
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (allowedLayerCollisions.Contains(target.gameObject.layer))
        {
            if (target.GetComponent<EnemyHealth>() != null & canTakeDamage)
            {
                target.GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            if (target.GetComponent<ColumnaLogic>() != null)
            {
                target.GetComponent<ColumnaLogic>().TakeDamage(damage);
            }

            if (target.GetComponent<BossHealth>() != null)
            {
                target.GetComponent<BossHealth>().TakeDamage(damage);
            }

            if (target.GetComponent<GolemBossHealth>() != null)
            {
                target.GetComponent<GolemBossHealth>().TakeDamage(damage);
            }

            if (target.GetComponent<StatueHealth>() != null)
            {
                target.GetComponent<StatueHealth>().TakeDamage(damage);
            }

            if (target.GetComponent<bossHealthR>() != null)
            {
                target.GetComponent<bossHealthR>().TakeDamage(damage);
            }

            if (target.GetComponent<MinionHealth>() != null)
            {
                target.GetComponent<MinionHealth>().TakeDamage(damage);
            }

            if (target.gameObject.layer == 6 & !returning)
            {
                rb.velocity = new Vector2(-pullForce, 0);
            }
        }

        if (target.CompareTag("Player") & returning)
        {
            Destroy(gameObject);
        }
    }


    private IEnumerator TakeDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
    }
}
