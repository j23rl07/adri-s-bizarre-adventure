using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealth : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("Hit Effect")]
    public GameObject dieEffect;
    public GameObject hitEffect;
    [HideInInspector] public bool gotHit = false;

    [Header("Boss Logic")]
    public BattleZoneBBDD bz;
    public GameObject gb;
    [SerializeField] private BoxCollider2D boxCollider;

    public void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    void OnEnable()
    {
        boxCollider.enabled = true;
        this.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Instantiate(hitEffect, transform.position, transform.rotation);

        if (currentHealth <= 0)
        {

            Die();

        }

        void Die()
        {
            GetComponent<Collider2D>().enabled = false;
            Instantiate(dieEffect, transform.position, transform.rotation);
            if (gb.name == "NucleoLobo")
            {
                bz.isWolfDestroyed = true;
            }
            else if (gb.name == "NucleoBuho")
            {
                bz.isOwlDestroyed = true;
            }
            else if (gb.name == "NucleoHeroe")
            {
                bz.isHeroDestroyed = true;
            }
            else if (gb.name == "NucleoMain")
            {
                bz.isMainDestroyed = true;
            }
            gameObject.active = false;
        }
    }

    public void setMaxHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
}
