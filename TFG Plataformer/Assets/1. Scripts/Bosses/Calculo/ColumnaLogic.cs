using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnaLogic : MonoBehaviour
{
    [Header("Healing Parameters")]
    public int maxHealth;
    public int currentHealth;
    public Animator animator;
    public GameObject dieEffect;
    [HideInInspector] public bool gotHit = false;
    public CalculoBossLogic cbl;
    public GameObject gb;

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
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Instantiate(dieEffect, transform.position, transform.rotation);
        if(gb.name == "Columna1")
        {
            cbl.st1IsDestroyed = true;
        }else if (gb.name == "Columna2") {
            cbl.st2IsDestroyed = true;
        }else if (gb.name == "Columna3") {
            cbl.st3IsDestroyed = true;
        }else if (gb.name == "Columna4") {
            cbl.st4IsDestroyed = true;
        }
        GameObject.Destroy(gameObject);
    }

}
