using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MovementDirection;
    public float Speed = 7;
    public float JumpStrength = 12f;
    private bool doubleJumped = false;
    public float jumpForce = 10f;
    public float gravity = -19.62f;
    public float fallMultiplier = 2.5f;

    private Rigidbody2D Rigidbody;
    private bool facingRight = true;

    private bool triggerJump = false;
    private bool triggerDoubleJump = false;

    public Transform GroundCheck;
    public LayerMask Ground;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.gravityScale = 0f;
    }

    void Update()
    {
        MovementDirection = Keyboard.current.dKey.isPressed ? 1f : (Keyboard.current.aKey.isPressed ? -1f : 0f);
        if(MovementDirection > 0 && !facingRight)
        {
            Flip();
        }
        else if(MovementDirection < 0 && facingRight)
        {
            Flip();
        }

        bool isGrounded = CheckGround();

        if (isGrounded)
        {
            doubleJumped = false;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            if (isGrounded)
            {
                triggerJump = true;
            }
            else if (!doubleJumped)
            {
                triggerDoubleJump = true;
                doubleJumped = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            doubleJumped = true;
        }
    }

    private void FixedUpdate()
    {
        float targetVelocityX = MovementDirection * Speed;
        float currentYVelocity = Rigidbody.linearVelocity.y;

        if(currentYVelocity < 0)
        {
            currentYVelocity += gravity * fallMultiplier * Time.fixedDeltaTime;
        }
        else
        {
            currentYVelocity += gravity * Time.fixedDeltaTime;
        }

        if(triggerJump)
        {
            currentYVelocity = JumpStrength;
            triggerJump = false;
        }
        else if(triggerDoubleJump)
        {
            currentYVelocity = JumpStrength;
            triggerDoubleJump = false;
        }
        
        Rigidbody.linearVelocity = new Vector2(targetVelocityX, currentYVelocity);

    }

    public bool CheckGround()
    {
        if(GroundCheck == null) return false;

        return Physics2D.OverlapCircle(GroundCheck.position, 0.25f, Ground);
    }

    private int Direction()
    {
        if(MovementDirection > 0)
        {
            return 1;
        }
        if(MovementDirection < 0)
        {
            return -1;
        }
        return 0;
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
