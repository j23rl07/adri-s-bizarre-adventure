using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float delay;
    [SerializeField] private LayerMask playerLayer;
    
    private Vector3 finalPos;
    private Vector3[] directions = new Vector3[4];

    private float timer;
    private bool attacking;

    private void OnEnable()
    {
        GameObject[] goldenCoin = GameObject.FindGameObjectsWithTag("MyCoin");
        GameObject[] silverCoin = GameObject.FindGameObjectsWithTag("SilverCoin");


        foreach (GameObject obj in goldenCoin)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        foreach (GameObject obj in silverCoin)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        Stop();
    }

    private void Update()
    {
        //Move object to final position if activated
        if (attacking) { 
            transform.Translate(finalPos * Time.deltaTime * speed);
        }
        else
        {
            timer += Time.deltaTime;
            if(timer > delay)
            {
                checkForPlayer();
            }
        }
    }

    private void checkForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                finalPos = directions[i];
                timer = 0;
            }
        }
        
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    
    }

    private void Stop()
    {
        finalPos = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
