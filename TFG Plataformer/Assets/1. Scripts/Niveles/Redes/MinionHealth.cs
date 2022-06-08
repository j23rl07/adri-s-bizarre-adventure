using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealth : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public GameObject dieEffect;
    public bool useDieEffect = true;
    [HideInInspector] public bool gotHit = false;

    private Vector3 posicionInicial = new Vector3();


    public void Awake()
    {
        posicionInicial = gameObject.transform.position;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
         Die();
        }

        void Die()
        {
            if (useDieEffect)
            {
                animator.SetBool("IsDead", true);
            }
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            /*En caso de que haya que desactivar alg�n componente en espec�fico que haga funcionar a un enemigo al destruirlo, se realizar� desde aqu� */

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
            //GameObject.Destroy(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.position = posicionInicial;
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
