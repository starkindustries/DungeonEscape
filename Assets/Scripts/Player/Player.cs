using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float jumpForce = 0f;
    private float jumpRaycastDistance = 0.8f;

    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    // Note that layerMask is just a 32 bit integer. There are a max of 32 layer slots.
    private LayerMask groundLayer;
    private PlayerAnimation animator;
    // private SpriteRenderer playerSprite;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // Assign handle to rigid body
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimation>();
        // playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((CrossPlatformInputManager.GetButtonDown("AButton")) && IsGrounded())
        {
            animator.Attack();
        }
        // Draw the jump detector raycast
        Debug.DrawRay(transform.position, Vector2.down * jumpRaycastDistance, Color.green);
    }

    void PlayerMovement()
    {
        IsGrounded();

        // horizontal input for left/right
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");
        // Debug.Log("horizontal: " + horizontalInput);

        // Face correct direction
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // If jump key && grounded:
        // current velocity = new velocity (current x, jumpForce);        
        if ((CrossPlatformInputManager.GetButtonDown("BButton")) && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            animator.Jump(true);
        }
        
        // current velocity = new velocity (x = horizontal input, current velocity.y)
        rb2d.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, rb2d.velocity.y);
        animator.Move(horizontalInput);
    }

    // Use FixedUpdate for physics stuff
    void FixedUpdate()
    {
        PlayerMovement();
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, jumpRaycastDistance, groundLayer.value);
        // Debug.DrawRay(transform.position, Vector2.down * jumpRaycastDistance, Color.green);
        if (hitInfo.collider != null)
        {
            Debug.Log("hit: " + hitInfo.collider.name);
            animator.Jump(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
