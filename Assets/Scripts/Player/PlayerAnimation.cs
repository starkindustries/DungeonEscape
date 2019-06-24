using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Animator swordAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        animator.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
        animator.SetBool("Jump", jump);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        // Sword arc:
        // swordAnimator.SetTrigger("SwordAnimation");
    }
}
