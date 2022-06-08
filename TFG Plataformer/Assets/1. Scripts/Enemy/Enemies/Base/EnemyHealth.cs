using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public GameObject dieEffect;
    public bool useDieEffect = true;
    [HideInInspector] public bool gotHit = false;
    [HideInInspector] public bool isDead = false;


    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            if(GetComponent<SinEnemyProjectile>() != null) //Espec�fico para el spawner de MedusaHeads
            {
                Deactivate();
            }
            else
            {
                if (useDieEffect)
                {
                    Die(); 
                }
                else
                {
                    animator.SetBool("IsDead", true);
                }
            }
            
        }

        void Die()
        {
            isDead = true;
            if (useDieEffect)
            {
                animator.SetBool("IsDead", true);
            }
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            /*En caso de que haya que desactivar alg�n componente en espec�fico que haga funcionar a un enemigo al destruirlo, se realizar� desde aqu� */
            
            if (GetComponentInParent<EnemyPatrol>() != null)
            {
                GetComponentInParent<EnemyPatrol>().enabled = false;
            }
            if (GetComponent<Enemy>() != null)
            {
                GetComponent<Enemy>().enabled = false;
            }

            if (GetComponent<BoxScript>() != null)
            {
                GetComponent<BoxScript>().instantiate();
            }

            if (GetComponentInChildren<EnemyFireballHolder>() != null)
            {
                GetComponentInChildren<EnemyFireballHolder>().enabled = false;
            }
            
            if (GetComponent<RangeEnemy>() != null)
            {
                GetComponent<RangeEnemy>().enabled = false;

            }
            if (useDieEffect)
            {
                Instantiate(dieEffect, transform.position, transform.rotation);
            }
            GameObject.Destroy(gameObject);
        }

        //Para MedusaHeads
        void Deactivate()
        {
            if (useDieEffect)
            {
                Instantiate(dieEffect, transform.position, transform.rotation);
            }
            gameObject.SetActive(false);
        }   
    }
    public void Die()
    {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (GetComponentInParent<EnemyPatrol>() != null)
        {
            GetComponentInParent<EnemyPatrol>().enabled = false;
        }
        if (GetComponent<Enemy>() != null)
        {
            GetComponent<Enemy>().enabled = false;
        }

        if (GetComponent<BoxScript>() != null)
        {
            GetComponent<BoxScript>().instantiate();
        }

        if (GetComponentInChildren<EnemyFireballHolder>() != null)
        {
            GetComponentInChildren<EnemyFireballHolder>().enabled = false;
        }

        if (GetComponent<RangeEnemy>() != null)
        {
            GetComponent<RangeEnemy>().enabled = false;

        }
        GameObject.Destroy(gameObject);
    }

    public void BossPDie()
    {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (GetComponentInParent<EnemyPatrol>() != null)
        {
            GetComponentInParent<EnemyPatrol>().enabled = false;
        }
        if (GetComponent<Enemy>() != null)
        {
            GetComponent<Enemy>().enabled = false;
        }

        if (GetComponent<BoxScript>() != null)
        {
            GetComponent<BoxScript>().instantiate();
        }

        if (GetComponentInChildren<EnemyFireballHolder>() != null)
        {
            GetComponentInChildren<EnemyFireballHolder>().enabled = false;
        }

        if (GetComponent<RangeEnemy>() != null)
        {
            GetComponent<RangeEnemy>().enabled = false;

        }

        GetComponent<Animator>().enabled = false;
        GetComponent<boss>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        //GameObject.Destroy(gameObject);
    }
}
