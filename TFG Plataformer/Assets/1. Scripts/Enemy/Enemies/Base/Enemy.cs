using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyHealth
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCd;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;
    private EnemyPatrol enemyPatrol;

    [Header("Other")]
    public Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;
        if (PlayerInSight()) {
            if (cdTimer >= attackCd)
            {
                cdTimer = 0;
                animator.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight() && currentHealth > 0;

    }
    
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }

    private void damagePlayer()
    {
        if (PlayerInSight())
            player.TakeDamage(damage);
    }


}
