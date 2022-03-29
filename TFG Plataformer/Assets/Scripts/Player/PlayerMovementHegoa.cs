using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHegoa : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float movetSmoothing = .05f;
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private CircleCollider2D circleCollider;
    private bool facingRight = true;
    private float direction = 0;
    private bool jumpKeyDown = false;
    private bool jumpKeyUp = false;

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
        if (jumpKeyDown)
        {
            jumpKeyDown = false;
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if(jumpKeyUp)
        {
            jumpKeyUp = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y*0.3f);
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
        //Si la tecla se mantiene presionada se salta mas alto que si se suelta
        if (Input.GetKeyDown(KeyCode.Space) & IsGrounded())
        {
            jumpKeyDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) & rigidBody.velocity.y > 0f)
        {
            jumpKeyUp = true;
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

        Debug.Log(rayCastHit.collider);

        return rayCastHit.collider != null;
    }
}
