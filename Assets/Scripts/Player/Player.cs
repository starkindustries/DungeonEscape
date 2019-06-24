using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    // IDamageable
    public int Health { get; set; }
    public int gemCount = 0;

    private Rigidbody2D rb2d;

    [SerializeField]
    private int health;
    [SerializeField]
    private float jumpForce = 0f;
    private float jumpRaycastDistance = 0.9f;
    [SerializeField]
    private float speed = 0f;
    [SerializeField] 
    private LayerMask groundLayer; // Note that layerMask is just a 32 bit integer. There are a max of 32 layer slots.
    private PlayerAnimation animator;
    // private SpriteRenderer playerSprite;

    private bool facingRight;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimation>();
        Health = health;
        facingRight = true;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((CrossPlatformInputManager.GetButtonDown("AButton")) && IsGrounded())
        {
            animator.Attack();
        }
        // Debug: Draw the jump detector raycast
        Debug.DrawRay(transform.position, Vector2.down * jumpRaycastDistance, Color.green);        
    }

    void PlayerMovement()
    {        
        // horizontal input for left/right
        float temp1 = CrossPlatformInputManager.GetAxis("Horizontal");
        float temp2 = Input.GetAxisRaw("Horizontal");
        float horizontalInput = (Mathf.Abs(temp1) > Mathf.Abs(temp2)) ? temp1 : temp2;
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

        // Check if player landed
        bool grounded = IsGrounded(); 
        if (CrossPlatformInputManager.GetButtonDown("BButton") && grounded)
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
        if (hitInfo.collider != null)
        {
            // Debug.Log("Grounded on: " + hitInfo.collider.name);
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

    public void Damage()
    {
        Debug.Log("Player damaged!");
        Health--;
        
        // Check if player is dead
        if(isDead)
        {
            return;
        }

        // Player is not dead. Check health
        if(Health > 0)
        {
            animator.Hit();
            return;
        }

        // Player is not dead and health is 0!
        isDead = true;
        animator.Die();        
    }
}
