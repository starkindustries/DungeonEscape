using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    // IDamageable
    public int Health { get; set; } 

    [SerializeField]
    protected int health;    
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    
    protected Vector3 target;
    protected Animator animator;
    protected bool facingRight;
    protected bool shouldIdle;
    protected bool isDead = false;

    protected Transform playerTransform;

    // Abstract Methods
    public abstract void Attack();

    // Start is called before the first frame update
    void Start()
    {
        Init();
        Health = health;
        target = pointB.position;
        shouldIdle = true;
        facingRight = true;
        animator = GetComponentInChildren<Animator>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public virtual void Init() {}

    public virtual void Update()
    {
        // If enemy is dead, do nothing.
        if (isDead)
        {
            return;
        }

        float distanceFromPlayer = Vector2.Distance(transform.position, playerTransform.position);
        // 0 is the layer, which is 0 in this case.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // do not move while in idle
            // do not move if player is near
            if (distanceFromPlayer > 2.0f)
            {
                animator.SetBool("InCombat", false);
            }                       
        }
        else if (animator.GetBool("InCombat") == true)
        {
            FacePlayer();
        }
        else 
        {
            Patrol();            
        }
    }

    public virtual void FacePlayer()
    {
        // face the player
        Vector2 direction = playerTransform.position - transform.position;        
        if (direction.x > 0 && !facingRight)
        {
            // face right
            Debug.Log("FLIP TO RIGHT");
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            // face left
            Debug.Log("FLIP TO LEFT");
            Flip();
        }
        

    }

    public void Flip()
    {
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Patrol()
    {
        // Debug.Log("Dist PointA-Pos: " + (pointA.position.x - transform.position.x));
        // Debug.Log("Dist Pos-PointB: " + (transform.position.x - pointB.position.x));

        // Check if enemy has reached pointA. If yes, change target to pointB.
        if (pointA.position.x - transform.position.x >= 0 && target == pointA.position)
        {
            if (shouldIdle)
            {
                animator.SetTrigger("Idle");
                shouldIdle = false;
            }
            else
            {
                // Debug.Log("move to point B");
                target = pointB.position;                
                shouldIdle = true;
            }            
        }
        // Check if enemy has reached pointB. If yes, change target to pointA.
        if (transform.position.x - pointB.position.x >= 0 && target == pointB.position)
        {
            if (shouldIdle)
            {
                animator.SetTrigger("Idle");
                shouldIdle = false;
            }
            else
            {
                // Debug.Log("move to point A");
                target = pointA.position;
                shouldIdle = true;
            }
        }

        // If enemy is moving towards pointA (moving left) and facing right then flip it.
        // Or if enemy is moving towards pointB (moving right) and facing left then flip it.
        if ((target == pointA.position && facingRight) || (target == pointB.position && !facingRight))
        {
            Flip();
        }

        float step = speed * Time.deltaTime;
        // set the move target to have the same y value as the current object
        Vector2 moveTarget = new Vector2(target.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);       
    }

    public virtual void Damage()
    {        
        if (isDead)
        {
            Debug.Log("Dead enemy damaged.");
            return;
        }

        Debug.Log("Damage!");
        Health -= 1;

        if (Health < 1)
        {
            Debug.Log("DESTROY!");                        
            animator.SetTrigger("Death");
            isDead = true;
            // Destroy(this.gameObject);
        }
        else
        {
            animator.SetTrigger("Hit");            
            animator.SetBool("InCombat", true);
        }
    }    
}
