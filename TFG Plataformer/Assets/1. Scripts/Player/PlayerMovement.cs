using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [Header("Jump")]
    [SerializeField] public float jumpForce = 15.5f;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float extraJumpsForce = 12;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public Vector2 groundCheckDirection;
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashLength = .22f;
    [SerializeField] private float dashCooldown = .5f;
    [Header("WallJump")]
    [SerializeField] private float wallJumpForceX = 20f;
    [SerializeField] private float wallJumpForceY = 13f;
    [SerializeField] private float wallJumpTime = 0.05f;
    [SerializeField] private float wallSlideSpeed = 0.7f;
    [SerializeField] private float wallDistance = 0.5f;
    private bool isWallJumping = false;
    private bool isWallSliding = false;
    [SerializeField] private float wallJumpAirTime = 0.3f;
    RaycastHit2D WallCheckHit;
    private float jumpTime;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private int facingRight = 1;
    [HideInInspector] public float horizontalSpeed = 0;
    [HideInInspector] public bool canFlip = true;

    private bool isGrounded = false;
    private bool jump = false;
    private bool airJump = false;
    private bool cancelJump = false;
    private bool wallJump = false;
    private float coyoteTime = 0.07f;
    private float coyoteTimeCounter = 0;
    private float jumpBufferTime = 0.07f;
    private float jumpBufferCounter = 0;
    private int extraJumpsCounter = 0;

    private bool dash = false;
    [HideInInspector] public bool isDashing = false;
    private float dashCdCounter = 0;

    private PauseMenu pauseMenu;
    
    [HideInInspector] public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        gravity = rigidBody.gravityScale;
        groundCheckDirection = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseMenu.IsGamePaused())
            GetMovementInputs();
    }

    private void FixedUpdate()
    {
            SetMovement();
    }


    private void SetMovement()
    {
        if (dash){
            StartCoroutine(Dash());
        }
        if(!isDashing & !isWallJumping)
        {
            //Movimiento horizontal
            rigidBody.velocity = new Vector2(horizontalSpeed, rigidBody.velocity.y);

            //Salto
            //Si la tecla se mantiene presionada se salta mas alto que si se suelta
            if (jump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce*(-1*groundCheckDirection.y));
                jump = false;
            }
            else if (airJump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, extraJumpsForce*(-1*groundCheckDirection.y));
                airJump = false;
            }
            if(cancelJump & rigidBody.velocity.y > 0f)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y*0.5f);
                cancelJump = false;
            }

            //Girar el modelo si cambia de direccion
            if (horizontalSpeed > 0 && facingRight == -1)
            {
                Flip();
            }
            else if (horizontalSpeed < 0 && facingRight == 1)
            {
                Flip();
            }

            //wallJump
            if (wallJump)
            {

                StartCoroutine(WallJump());
            }
            if (facingRight == 1)
            {
                WallCheckHit = Physics2D.Raycast(boxCollider.bounds.center, new Vector2(wallDistance, 0), wallDistance, groundLayer);
            } 
            else
            {
                WallCheckHit = Physics2D.Raycast(boxCollider.bounds.center, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
            }

            if (WallCheckHit && !IsGrounded() && horizontalSpeed != 0 )
            {
                isWallSliding = true;
                jumpTime = Time.time + wallJumpTime;
            }
            else if (jumpTime < Time.time)
            {
                isWallSliding = false;
            }

            if (isWallSliding & rigidBody.velocity.y < 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * wallSlideSpeed);
            }
        }

    }

    private void GetMovementInputs()
    {

        //Movimiento horizontal
        horizontalSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //Salto
        //CoyoteTime: un margen desde que el jugador deja de tocar el suelo para que pueda saltar mejor desde bordes
        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            extraJumpsCounter = extraJumps;
}
        else
        {
            coyoteTimeCounter -= Time.deltaTime;

            //Salto doble/multiple
            if(extraJumpsCounter > 0 & Input.GetKeyDown(KeyCode.Space) & coyoteTimeCounter <= 0 & !isWallJumping & !isWallSliding)
            {
                airJump = true;
                extraJumpsCounter -= 1;
            }
        }
        //JumpBuffer: si se presiona la tecla de salto un poco antes de tocar el suelo se registra y cuando se toque el suelo se vuelve a saltar (evita tener que ser super preciso)
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
            cancelJump = false;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        //Permite saltar
        if ((jumpBufferCounter > 0 & coyoteTimeCounter > 0))
        {
            jump = true;
            extraJumpsCounter = extraJumps;
            jumpBufferCounter = 0;
        }
        //Si la tecla se mantiene presionada salta mas alto que si se suelta
        if (Input.GetKeyUp(KeyCode.Space))
        {
            cancelJump = true;
            //Impide que se pueda dar saltar más veces si pulsas muy rápido la tecla de nuevo
            coyoteTimeCounter = 0;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) & dashCdCounter <= 0)
        {
            dash = true;
        }
        else if(dashCdCounter > 0)
        {
            dashCdCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) & isWallSliding)
        {
            wallJump = true;
        }
    }

    private void Flip()
    {
        if (canFlip)
        {
            facingRight = facingRight * -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, groundCheckDirection, extraHeight, groundLayer);

        //Debug.Log(rayCastHit.collider);
        isGrounded = rayCastHit.collider != null;

        return isGrounded;
    }

    private IEnumerator Dash()
    {
        //Desactivar ataques y habilidades hasta que se complete el dash
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Weapon>().enabled = false;

        dash = false;
        isDashing = true;
        rigidBody.velocity = new Vector2(dashSpeed * facingRight, 0);
        rigidBody.gravityScale = 0;
        yield return new WaitForSeconds(dashLength);
        rigidBody.gravityScale = gravity;
        isDashing = false;
        dashCdCounter = dashCooldown;

        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<Weapon>().enabled = true;
    }

    private IEnumerator WallJump()
    {
        isWallJumping = true;
        wallJump = false;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x,0);
        if (facingRight == 1)
        {
            rigidBody.AddForce(new Vector2(-wallJumpForceX, wallJumpForceY * (-1 * groundCheckDirection.y)), ForceMode2D.Impulse);
        }
        else
        {
            rigidBody.AddForce(new Vector2(wallJumpForceX, wallJumpForceY * (-1 * groundCheckDirection.y)), ForceMode2D.Impulse);
        }
        Flip();
        yield return new WaitForSeconds(wallJumpAirTime);
        isWallJumping = false;
    }

}
