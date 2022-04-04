using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 15.5f;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float extraJumpsForce = 12;
    [SerializeField] private LayerMask groundLayer;
    [Header("Dash")]
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashLength = .22f;
    [SerializeField] private float dashCooldown = .5f;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;
    private int facingRight = 1;
    [HideInInspector] public float horizontalSpeed = 0;
    [HideInInspector] public bool canFlip = true;

    private bool jump = false;
    private bool airJump = false;
    private bool cancelJump = false;
    private float coyoteTime = 0.07f;
    private float coyoteTimeCounter = 0;
    private float jumpBufferTime = 0.07f;
    private float jumpBufferCounter = 0;
    private int extraJumpsCounter = 0;

    private bool dash = false;
    [HideInInspector] public bool isDashing = false;
    private float dashCdCounter = 0;
    
    [HideInInspector] public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        
        gravity = rigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(!isDashing)
        {
            //Movimiento horizontal
            rigidBody.velocity = new Vector2(horizontalSpeed, rigidBody.velocity.y);

            //Salto
            //Si la tecla se mantiene presionada se salta mas alto que si se suelta
            if (jump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jump = false;
            }
            else if (airJump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, extraJumpsForce);
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
            if(extraJumpsCounter > 0 & Input.GetKeyDown(KeyCode.Space) & coyoteTimeCounter <= 0)
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
        //Si la tecla se mantiene presionada salta mas alto que si se suelta
        if ((jumpBufferCounter > 0 & coyoteTimeCounter > 0))
        {
            jump = true;
            extraJumpsCounter = extraJumps;
            jumpBufferCounter = 0;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            cancelJump = true;
            //Impide que se pueda dar saltar m�s veces si pulsas muy r�pido la tecla de nuevo
            coyoteTimeCounter = 0;
        }

        //Dash
        if ((Input.GetKeyDown(KeyCode.LeftShift) | Input.GetKeyDown(KeyCode.RightShift)) & dashCdCounter <= 0)
        {
            dash = true;
        }
        else if(dashCdCounter > 0)
        {
            dashCdCounter -= Time.deltaTime;
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
        RaycastHit2D rayCastHit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, Vector2.down, extraHeight, groundLayer);

        //Debug.Log(rayCastHit.collider);

        return rayCastHit.collider != null;
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
}