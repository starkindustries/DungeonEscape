using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;

    [SerializeField]
    private float jumpForce = 0f;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    // Note that layerMask is just a 32 bit integer. There are a max of 32 layer slots.
    private LayerMask _groundLayer; 

    // Start is called before the first frame update
    void Start()
    {
        // Assign handle to rigid body
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerMovement()
    {
        // horizontal input for left/right
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Debug.Log("horizontal: " + horizontalInput);

        // If jump key && grounded:
        // current velocity = new velocity (current x, jumpForce);        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);
        }

        // current velocity = new velocity (x = horizontal input, current velocity.y)
        _rigid.velocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, _rigid.velocity.y);
    }

    // Use FixedUpdate for physics stuff
    void FixedUpdate()
    {
        PlayerMovement();
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);
        if (hitInfo.collider != null)
        {
            Debug.Log("hit: " + hitInfo.collider.name);
            return true;
        }
        else
        {
            return false;
        }
    }
}
