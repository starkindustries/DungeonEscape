using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Diamond triggered by: " + other.gameObject.name);
        Debug.Log("Diamond position: " + this.gameObject.transform.position.ToString());

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (other.gameObject.tag == player.tag)
        {
            player.PickupDiamond();
            Destroy(this.gameObject);
            Debug.Log("Diamond trigger3, player gem count: " + player.GetGemCount());
        }
    }
}
