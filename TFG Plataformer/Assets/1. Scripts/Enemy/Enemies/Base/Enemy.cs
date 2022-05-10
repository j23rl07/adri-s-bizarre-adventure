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

    
    /*En el primer frame obtenemos los componentes y variables necesarias para instanciar al enemigo*/
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    /*Determina cuando el enemigo ataca dependiendo del cooldown establecido al ser el jugador detectado*/
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
        
        /*El enemigo patrullará mientras no haya detectado al jugador*/
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }
    
    /*Generar la zona que detecta al jugador. Las variables range y colliderDistance permiten ajustar el tamaño
     de dicha caja. Vector3 nos permite resituar la caja. Transform.localScale.x nos permite rotar al enemigo y collider adecuadamente.
    Se retorna si el jugador se ha detectado o no.*/
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    /*Usamos Gizmos para dibujar la zona anterior y poder visualizarla*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }

    /*Se llama a la función correspondiente del jugador para que reciba daño al ser detectado*/
    private void damagePlayer()
    {
        if (PlayerInSight())
            player.TakeDamage(damage);
    }

    
}
