using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Diamond trigger1: " + other.gameObject.tag);
        Debug.Log("Diamond trigger2: " + GameObject.FindWithTag("Player").tag);

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (other.gameObject.tag == player.tag)
        {
            player.gemCount++;
            Destroy(this.gameObject);
            Debug.Log("Diamond trigger3, player gem count: " + player.gemCount);
        }
    }
}
