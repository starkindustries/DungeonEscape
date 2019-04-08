using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    protected bool facingRight;

    protected Vector3 target;
    protected Animator animator;
    protected bool shouldIdle;

    // Start is called before the first frame update
    void Start()
    {
        target = pointB.position;
        shouldIdle = true;
        animator = GetComponentInChildren<Animator>();
    }

    public virtual void Update()
    {
        // 0 is the layer, which is 0 in this case.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // do nothing
        }
        else
        {
            Move();
        }
    }

    public virtual void Attack()
    {
        Debug.Log("Name: " + this.gameObject.name);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Move()
    {
        Debug.Log("Dist PointA-Pos: " + (pointA.position.x - transform.position.x));
        Debug.Log("Dist Pos-PointB: " + (transform.position.x - pointB.position.x));
        if (pointA.position.x - transform.position.x >= 0 && target == pointA.position)
        {
            if (shouldIdle)
            {
                animator.SetTrigger("Idle");
                shouldIdle = false;
            }
            else
            {
                Debug.Log("move to point B");
                target = pointB.position;
                Flip();
                shouldIdle = true;
            }
        }
        if (transform.position.x - pointB.position.x >= 0 && target == pointB.position)
        {
            if (shouldIdle)
            {
                animator.SetTrigger("Idle");
                shouldIdle = false;
            }
            else
            {
                Debug.Log("move to point A");
                target = pointA.position;
                Flip();
                shouldIdle = true;
            }
        }
        float step = speed * Time.deltaTime;
        // set the move target to have the same y value as the current object
        Vector2 moveTarget = new Vector2(target.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, step);
    }
}
