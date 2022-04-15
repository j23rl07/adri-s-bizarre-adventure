using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyHealth
{

    [Header("Attack Parameters")]
    [SerializeField] private float attackCd;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;

    [Header("Other")]
    public Player player;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cdTimer >= attackCd)
            {
                cdTimer = 0;
                animator.SetTrigger("rangedAttack");
            }
        }

    }

    private void rangedAttack()
    {
        cdTimer = 0;
        projectiles[findProjectile()].transform.position = firePoint.position;
        projectiles[findProjectile()].GetComponent<EnemyProjectile>().triggerProjectile();
    }

    private int findProjectile()
    {
        //Iterar entre proyectiles y resetear
        for (int i=0;i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
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

}
