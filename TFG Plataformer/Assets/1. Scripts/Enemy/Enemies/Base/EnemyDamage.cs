using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Player player;

    [SerializeField] protected private int damage;

    /*Script gen�rico para poder ocasionar da�os al jugador, se inicia la corutina que aplica un empuje al jugador.*/
    protected private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.TakeDamage(damage);
            
        }
    }
}
