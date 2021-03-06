using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PauseMenu pauseMenu;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public GameObject Trinket;

    [HideInInspector] public bool isAttacking = false;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    void Attack()
    {
        //Play an attack animation
        isAttacking = true;
        //haz sonido
        AudioManager.instance.PlayAudio(AudioManager.instance.MissSword);
        //Detect enemis in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyHealth>() != null)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }

            if(enemy.GetComponent<ColumnaLogic>() != null)
            {
                enemy.GetComponent<ColumnaLogic>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<BossHealth>() != null)
            {
                enemy.GetComponent<BossHealth>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<GolemBossHealth>() != null)
            {
                enemy.GetComponent<GolemBossHealth>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<StatueHealth>() != null)
            {
                enemy.GetComponent<StatueHealth>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<bossHealthR>() != null)
            {
                enemy.GetComponent<bossHealthR>().TakeDamage(attackDamage);
            }

            if (enemy.GetComponent<MinionHealth>() != null)
            {
                enemy.GetComponent<MinionHealth>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
}
