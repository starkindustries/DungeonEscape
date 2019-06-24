using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(true);
            ShopPanel panel = shopPanel.GetComponent<ShopPanel>();
            panel.SetBuyer(currentPlayer: other.gameObject.GetComponent<Player>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }
}
