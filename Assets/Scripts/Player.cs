using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Get reference to rigid body
    private Rigidbody2D _rigid;
    // Start is called before the first frame update
    void Start()
    {
        // Assign handle to rigid body
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal input for left/right
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // If jump key && grounded

        // current velocity = new velocity (x = horizontal input, current velocity.y)
        _rigid.velocity = new Vector2(horizontalInput, _rigid.velocity.y);
    }
}
