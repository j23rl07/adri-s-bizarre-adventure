using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHegoa : MonoBehaviour
{
    Rigidbody2D playerRb;
    public float moveSpeed;

    private bool facingRight = true;

    Vector2 targetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        setMovement();
    }

    private void FixedUpdate()
    {
        playerRb.velocity = targetVelocity;
    }


    private void setMovement()
    {
        targetVelocity = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A))
        {
            targetVelocity = new Vector2(moveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
        {

            targetVelocity = new Vector2(-moveSpeed, 0);
        }

        if (targetVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (targetVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
