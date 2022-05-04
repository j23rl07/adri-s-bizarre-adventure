using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spikes")
        {
            player.TakeDamage(100);    
        }
        if (collision.tag == "Lava")
        {
            player.TakeDamage(300);
        }
    }
}
