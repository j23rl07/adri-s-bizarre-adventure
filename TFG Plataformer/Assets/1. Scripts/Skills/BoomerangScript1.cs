using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript1 : MonoBehaviour
{
    public GameObject itemToAdd;
    public int amountToAdd;
    Inventory inventory;

    [SerializeField] private float force = 20f;
    [SerializeField] private float pullForce = 0.1f;
    [SerializeField] private int damage = 40;

    private Rigidbody2D rb;
    private bool returning = false;
    private bool canTakeDamage = true;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance.GetComponent<Inventory>();

        player = FindObjectOfType<Player>();
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
        if (target.CompareTag("Player"))
        {
            inventory.CheckTSlotsAvailability(itemToAdd, itemToAdd.name, amountToAdd);
        }

        if (target.GetComponent<EnemyHealth>() != null & canTakeDamage)
        {
            target.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        if(target.name == "Tilemap ground" & !returning)
        {
            rb.velocity = new Vector2(-pullForce, 0);
        }
        if(target.name == "Player" & returning)
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
