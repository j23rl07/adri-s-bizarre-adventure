using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGolemBehaviour : MonoBehaviour
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

    [Header("Other")]
    public Player player;
    public Animator animator;

    [Header("Attack")]
    public GameObject efectoAtaque;
    public Transform posicionEfecto;
    // Update is called once per frame
   
    void OnEnable()
    {
        boxCollider.enabled = true;
    }
    
    void Update()
    {
        cdTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cdTimer >= attackCd)
            {
                cdTimer = 0;
                animator.SetTrigger("meleeAttack");
                
            }
        }
    }


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

    /*Se llama a la funci�n correspondiente del jugador para que reciba da�o al ser detectado*/
    private void damagePlayer()
    {
        if (PlayerInSight())
            Instantiate(efectoAtaque, posicionEfecto.position, posicionEfecto.rotation);
            player.TakeDamage(damage);
    }
}
