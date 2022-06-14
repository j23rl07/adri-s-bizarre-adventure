using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    //Scripts
    private PlayerMovement playerMovementScript;
    private PlayerCombat playerCombatScript;
    private Player playerScript;
    private Weapon weaponScript;

    //Animaciones
    //  Movimiento
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_FALL = "Player_fall";
    const string PLAYER_DASH = "Player_dash";

    //  Combate
    const string PLAYER_ATTACK = "Player_attack";
    const string PLAYER_HURT = "Player_hurt";
    const string PLAYER_CAST = "Player_cast";
    const string PLAYER_DEATH = "Player_death";

    private Animator animator;
    private Rigidbody2D rigidBody;
    private PauseMenu pauseMenu;
    private string currentState;
    [HideInInspector] public bool canMove = true;
    private bool firstTime = true;
    private bool overrideAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerCombatScript = GetComponent<PlayerCombat>();
        playerScript = GetComponent<Player>();
        weaponScript = GetComponent<Weapon>();

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.IsGamePaused())
        {
            /*JERARQUÍA (Quién puede interrumpir a quien):
             * 1. Muerte/daño
             * 2. Dash/Ataque/habilidad 
             * 3. Movimiento/salto
             */
            if (playerScript.isDead)
            {
                playerScript.isHurt = false;
                ChangeAnimationState(PLAYER_DEATH);
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f & animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_DEATH))
                {
                    if (GetComponent<SpriteRenderer>().enabled)
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                        playerScript.canRespawn = true;
                    }
                }
            }
            else
            {
                if (playerScript.isHurt)
                {
                    ChangeAnimationState(PLAYER_HURT);
                    StopMovement(new Vector2(0, 0), false);
                    overrideAnimation = true;
                    playerCombatScript.enabled = false;
                    weaponScript.enabled = false;
                    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2.5f & animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_HURT))
                    {
                        playerScript.isHurt = false;
                        canMove = true;
                        overrideAnimation = false;
                        playerCombatScript.enabled = true;
                        weaponScript.enabled = true;
                    }
                }
                else if (playerCombatScript.isAttacking)
                {
                    if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f & animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_ATTACK)) | overrideAnimation)
                    {
                        playerCombatScript.isAttacking = false;
                        weaponScript.enabled = true;
                        canMove = true;
                        overrideAnimation = false;
                    }
                    else
                    {
                        ChangeAnimationState(PLAYER_ATTACK);
                        weaponScript.enabled = false;
                        StopMovement(new Vector2(0, 0), true);
                    }

                }
                else if (weaponScript.isCasting)
                {
                    if ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f & animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_CAST)) | overrideAnimation)
                    {
                        weaponScript.isCasting = false;
                        playerCombatScript.enabled = true;
                        overrideAnimation = false;
                    }
                    else
                    {
                        ChangeAnimationState(PLAYER_CAST);
                        playerCombatScript.enabled = false;
                    }
                }
                else if (playerMovementScript.isDashing)
                {
                    ChangeAnimationState(PLAYER_DASH);
                }
                else
                {
                    if (playerMovementScript.IsGrounded())
                    {
                        //Correr
                        if (Mathf.Abs(playerMovementScript.horizontalSpeed) > 0)
                        {
                            ChangeAnimationState(PLAYER_RUN);
                        }
                        else
                        {
                            ChangeAnimationState(PLAYER_IDLE);
                        }
                    }
                    else
                    {
                        //Salto
                        if (rigidBody.velocity.y >= 0)
                        {
                            ChangeAnimationState(PLAYER_JUMP);
                        }
                        else
                        {
                            ChangeAnimationState(PLAYER_FALL);
                        }
                    }
                }
            }
        }
    }

    private void ChangeAnimationState(string newState)
    {
        //evitar que una animacion se interrumpa a si misma
        if (currentState == newState) return;

        //iniciar la animacion
        animator.Play(newState);

        //cambiar el estado actual al de la nueva animacion
        currentState = newState;
    }

    private IEnumerator StopMovementRoutine(Vector2 movement, bool stopGravity)
    {
        canMove = false;
        playerMovementScript.enabled = false;
        rigidBody.velocity = movement;
        if (stopGravity)
        {
            rigidBody.gravityScale = 0;
        }
        while (!canMove)
        {
            yield return null;
        }
        rigidBody.gravityScale = playerMovementScript.gravity;
        playerMovementScript.enabled = true;
        firstTime = true;
    }

    private void StopMovement(Vector2 movement, bool stopGravity)
    {
        if (firstTime)
        {
            StartCoroutine(StopMovementRoutine(movement, stopGravity));
            firstTime = false;
        }
    }
}
