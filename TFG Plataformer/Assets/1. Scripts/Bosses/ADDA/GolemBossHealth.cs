using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBossHealth : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    [HideInInspector] public bool gotHit = false;

    [Header("References")]
    public HealthBar healthBar;
    public AddaBattleZone battleZone;
    public GameObject dieEffect;

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

        if (currentHealth <= 0)
        {
            Die();
        }

        void Die()
        {
            animator.SetBool("IsDead", true);
            this.enabled = false;
            Instantiate(dieEffect, transform.position, transform.rotation);
            GetComponent<Collider2D>().enabled = false;
            this.transform.parent.gameObject.active = false;
            if (gameObject.name == "GolemRange")
            {
                battleZone.isGolem2Destroyed = true;
            }
            if (gameObject.name == "GolemMelee")
            {
                battleZone.isGolem1Destroyed = true;
            }
        }
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
}
