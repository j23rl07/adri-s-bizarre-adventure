using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGolemBehaviour : MonoBehaviour
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
    public Animator animator;

    void OnEnable()
    {
        boxCollider.enabled = true;
    }
    /*En el primer frame obtenemos los componentes y variables necesarias para instanciar al enemigo*/
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /*Determina cuando el enemigo ataca dependiendo del cooldown establecido al ser el jugador detectado*/
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

    /*El enemigo dispara a través del firePoint. Llama al componente del proyectil correspondiente del array que posee la función que lo activa. 
     Se resetea el timer de disparo*/
    private void rangedAttack()
    {
        cdTimer = 0;
        projectiles[findProjectile()].transform.position = firePoint.position;
        projectiles[findProjectile()].GetComponent<GolemProjectile>().triggerProjectile();
    }

    private int findProjectile()
    {
        //Iterar entre proyectiles y resetear
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }
}
