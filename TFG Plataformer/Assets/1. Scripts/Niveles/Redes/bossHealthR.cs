using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealthR : MonoBehaviour
{

    [Header("boss")]
    public bool trasnlada = false;
    public int posicion = 0;
    public HealthBar healthBar;

    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public GameObject dieEffect;
    public bool useDieEffect = true;
    [HideInInspector] public bool gotHit = false;

    public GameObject bossO;


    /*En el primer frame obtenemos los componentes y variables necesarias para instanciar al enemigo*/
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
    }

    /*Determina cuando el enemigo ataca dependiendo del cooldown establecido al ser el jugador detectado*/
    private void Update()
    {
        GetComponent<boss>().LookAtPlayer();
        if (trasnlada)
        {
            nuevaPosicion();
        }
    }


    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= 1;
        actualizaBarraBoss();

        if (currentHealth <= 0)
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
        else
        {
            trasnlada = true;
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
            if (GetComponent<boss>() != null)
            {
                GetComponent<boss>().enabled = false;
            }
            if (useDieEffect)
            {
                Instantiate(dieEffect, transform.position, transform.rotation);
            }
            GetComponent<boss>().boss_muerto();
            GameObject.Destroy(gameObject);
        }
    }

    public void nuevaPosicion()
    {
        trasnlada = false;
        posicion++;

        if (posicion == 1)
        {
            bossO.transform.position = new Vector3(194f, -43f, 0f);
        }
        else if (posicion == 2)
        {
            bossO.transform.position = new Vector3(218f, -12f, 0f);
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

    public void actualizaBarraBoss()
    {
        healthBar.SetHealth(currentHealth);
    }
}
