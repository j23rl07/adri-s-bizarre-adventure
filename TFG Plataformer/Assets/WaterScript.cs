using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    public PlayerMovement pm;

    void Start()
    {
        spr = player.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spr.color = Color.blue;
            rb.mass = 0.8f;
            rb.gravityScale = 1.2f;
            pm.jumpForce = 10;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            spr.color = Color.white;
            rb.mass = 1;
            rb.gravityScale = 3;
            pm.jumpForce = 15.5f;
        }
    }
}
