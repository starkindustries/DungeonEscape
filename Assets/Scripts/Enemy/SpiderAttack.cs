using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    private Spider spider; 

    private void Start()
    {
        spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        // tell spider to fire
        Debug.Log("Spider should FIRE");
        spider.Attack();
    }
}
