using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinEnemyProjectile : EnemyDamage
{
    [Header("Sin")]
    public float bulletSpeed = 5.0f;
    public float frecuencia = 20.0f;  // Velocidad a lo largo del seno
    public float magnitud = 0.5f;   // Tamaño del movimiento en el seno
    private Vector3 eje;
    private Vector3 pos;

    [Header("Enemy bullet")]
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float duration;


    public void triggerProjectile()
    {
        pos = transform.position;
        eje = transform.right;
        duration = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        //mover el projectil en el eje x
        pos += transform.up * Time.deltaTime * bulletSpeed;
        transform.position = pos + eje * Mathf.Sin(Time.time * frecuencia) * magnitud;

        //resetear projectil
        duration += Time.deltaTime;
        if (duration > resetTime)
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

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
