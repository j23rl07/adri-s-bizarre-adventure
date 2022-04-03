using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    //Scripts
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private Animator animator;
    private string currentState;
    private Rigidbody2D rigidBody;

    //Movimiento
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_FALL = "Player_fall";
    const string PLAYER_DASH = "Player_dash";

    //Combate
    const string PLAYER_ATTACK = "Player_attack";

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*JERARQUÍA:
         * 1. Muerte
         * 2. Daño
         * 3. Dash
         * 4. Ataque/habilidad
         * 5. Movimiento/salto
         */

        //if()

        if (playerMovement.isDashing)
        {
            ChangeAnimationState(PLAYER_DASH);
        }
        else if (playerCombat.attack)
        {
            ChangeAnimationState(PLAYER_ATTACK);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f & animator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_ATTACK))
            {
                playerCombat.attack = false;
            }

        }
        else
        {
            if (playerMovement.IsGrounded())
            {
                //Correr
                if (Mathf.Abs(playerMovement.horizontalSpeed) > 0)
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

    private void ChangeAnimationState(string newState)
    {
        //evitar que una animacion se interrumpa a si misma
        if (currentState == newState) return;

        //iniciar la animacion
        animator.Play(newState);

        //cambiar el estado actual al de la nueva animacion
        currentState = newState;
    }
}
