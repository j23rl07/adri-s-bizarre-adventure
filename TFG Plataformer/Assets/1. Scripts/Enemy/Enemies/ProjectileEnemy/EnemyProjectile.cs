using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //herencia de la clase para da?ar al jugador
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float duration;
    public GameObject impactEffect;

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

    void OnEnable()
    {
        GameObject[] goldCoins = GameObject.FindGameObjectsWithTag("MyCoin");
        GameObject[] silverCoins = GameObject.FindGameObjectsWithTag("SilverCoin");
        GameObject[] bz = GameObject.FindGameObjectsWithTag("BattleZone");

        foreach (GameObject obj in goldCoins)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        foreach (GameObject obj in silverCoins)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        foreach (GameObject obj in bz)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.TakeDamage(damage);
        }
       
        base.OnTriggerEnter2D(collision); //llamada a parent script 
        gameObject.SetActive(false);
        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
