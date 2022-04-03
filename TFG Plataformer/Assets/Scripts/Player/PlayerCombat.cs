using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private PlayerMovement playerMovement;
    private Rigidbody2D rigidBody;
    private float gravity;
    [HideInInspector] public bool attack = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidBody = GetComponent<Rigidbody2D>();

        gravity = rigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //Play an attack animation
        attack = true;
        //Stop movement
        StartCoroutine(stopMovement());
        //Detect enemis in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }

        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator stopMovement()
    {
        playerMovement.enabled = false;
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.gravityScale = 0;
        while (attack)
        {
            yield return null;
        }
        playerMovement.enabled = true;
        rigidBody.gravityScale = gravity;
    }
}
