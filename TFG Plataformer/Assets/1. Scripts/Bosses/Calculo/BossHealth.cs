using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public GameObject dieEffect;
    [HideInInspector] public bool gotHit = false;

    [Header("References")]
    public HealthBar healthBar;
    public GameObject columns;
    public GameObject battleZone;
    public GameObject texto;
    public GameObject columnaBloqueo;
    public GameObject seeker;
    


    public void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0) {
            Die();
        }

        void Die()
        {
            animator.SetBool("IsDead", true);
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Instantiate(dieEffect, transform.position, transform.rotation);
            gameObject.active = false;
        }
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void OnDisable()
    {
        columns.SetActive(true);
        battleZone.SetActive(false);
        texto.SetActive(true);
        columnaBloqueo.SetActive(false);
        seeker.SetActive(false);
    }
}
