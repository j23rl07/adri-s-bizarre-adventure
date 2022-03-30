using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHegoa : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 15.5f;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float extraJumpsForce = 12;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;
    private bool facingRight = true;
    private float direction = 0;
    private bool jump = false;
    private bool extraJump = false;
    private bool cancelJump = false;
    private bool isJumping = false;
    private float coyoteTime = 0.07f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.07f;
    private float jumpBufferCounter;
    private int extraJumpsCounter;

    Vector2 targetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
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
        //Movimiento horizontal
        rigidBody.velocity = new Vector2(direction*moveSpeed, rigidBody.velocity.y);

        //Salto
        //Si la tecla se mantiene presionada se salta mas alto que si se suelta
        if (jump)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jump = false;
            isJumping = true;
        }
        if (extraJump)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(new Vector2(0, extraJumpsForce), ForceMode2D.Impulse);
            extraJump = false;
        }
        if(cancelJump & rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y*0.5f);
            cancelJump = false;
        }

        //Girar el modelo si cambia de direccion
        if (direction > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction < 0 && facingRight)
        {
            Flip();
        }
    }

    private void GetMovementInputs()
    {

        //Movimiento horizontal
        direction = Input.GetAxisRaw("Horizontal");

        //Salto
        //CoyoteTime: un margen desde que el jugador deja de tocar el suelo para que pueda saltar mejor desde bordes
        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
            extraJumpsCounter = extraJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;

            if(extraJumpsCounter > 0 & Input.GetKeyDown(KeyCode.Space))
            {
                extraJump = true;
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
            jumpBufferCounter = 0;
        }
        if (Input.GetKeyUp(KeyCode.Space) & isJumping)
        {
            cancelJump = true;
            //Impide que se pueda dar saltar m�s veces si pulsas muy r�pido la tecla de nuevo
            coyoteTimeCounter = 0;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D rayCastHit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, Vector2.down, extraHeight, groundLayer);

        //Debug.Log(rayCastHit.collider);

        return rayCastHit.collider != null;
    }
}