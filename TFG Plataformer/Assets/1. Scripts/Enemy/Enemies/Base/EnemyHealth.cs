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
    
    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            if(GetComponent<SinEnemyProjectile>() != null)
            {
                Deactivate();
            }
            else
            {
                Die();
            }
            
        }

        void Die()
        {
            animator.SetBool("IsDead", true);
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
            Instantiate(dieEffect, transform.position, transform.rotation);
            GameObject.Destroy(gameObject);
        }

        void Deactivate()
        {
            Instantiate(dieEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }   
    }
}
