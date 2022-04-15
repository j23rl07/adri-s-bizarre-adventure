using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //herencia de la clase para dañar al jugador
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float duration;

    public void triggerProjectile()
    {
        duration = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        //mover el projectil en el eje x
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        //resetear projectil
        duration += Time.deltaTime;
        if(duration > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.TakeDamage(damage);
            StartCoroutine(player.playerKnockback(0.01f, 50, player.transform.position));
        }
        base.OnTriggerEnter2D(collision); //llamada a parent script 
        gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
