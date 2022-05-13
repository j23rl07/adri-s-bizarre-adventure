using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculoBossLogic : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCd;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Column management")]
    public bool st1IsDestroyed;
    public bool st2IsDestroyed;
    public bool st3IsDestroyed;
    public bool st4IsDestroyed;
    public BoxCollider2D healthCollider;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;

    [Header("Other")]
    public Player player;
    public Animator animator;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        st1IsDestroyed = false;
        st2IsDestroyed = false;
        st3IsDestroyed = false;
        st4IsDestroyed = false;
        InvokeRepeating("EnemySpawn", 5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(st1IsDestroyed && st2IsDestroyed && st3IsDestroyed && st4IsDestroyed)
        {
            GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
        }

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
            player.TakeDamage(damage);
    }

    private void EnemySpawn()
    {
        animator.SetTrigger("summon");
        GameObject enemyToSpawn = Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        enemyToSpawn.transform.position = this.transform.position;
        enemyToSpawn.SetActive(true);
    }
}
