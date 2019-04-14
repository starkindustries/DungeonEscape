using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    bool canDamage = true;

    private void OnEnable()
    {
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && canDamage)
        {
            Debug.Log("HIT WITH DAMAGE!");
            hit.Damage();
            canDamage = false;
        }
    }
}
