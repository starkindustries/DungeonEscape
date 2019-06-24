using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    // UI Elements
    [SerializeField]
    private Text playerGemCountText;
    [SerializeField]
    private Image selectionImage;

    // Data Variables
    private int playerGemCount;
    private int[] yPos;
    private int[] prices;
    private string[] itemDescriptions;
    private int? selectedItem = null;

    public void Start()
    {
        yPos = new int[] { 105, 0, -103 };
        prices = new int[] { 400, 500, 100 };
        itemDescriptions = new string[] { "Flame Sword", "Flight Boots", "Castle Keys" };
        selectionImage.enabled = false;
    }

    public void SetPlayerGemCount(int gemCount)
    {
        playerGemCount = gemCount;
        playerGemCountText.text = gemCount.ToString() + "G";
    }   

    public void DidSelectItem(int item)
    {                        
        Debug.Log("ITEM SELECTED: " + item.ToString());
        selectedItem = item;

        selectionImage.enabled = true;
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos[item]);        
    }

    public void DidPressBuyButton()
    {
        if (selectedItem == null)
        {
            Debug.Log("Buy button: No item selected.");
            return;
        }
        /* stopped here
        if(selectedItem == 2) // castle key
        {

        }
        else if (selectedItem >= 0 && selectedItem < 3) // 0 is Flame sword, 1 is Flight boots, 2 is Castle keys
        {
            // Check if player has enough gems
            if (prices[selectedItem.Value] <= playerGemCount)
            {
                // Player has enough gems!
                Debug.Log("You bought: " + itemDescriptions[selectedItem.Value]);
            }
            else
            {
                // Player does not have enough gems
                Debug.Log("Buy button: not enough gems for: " + itemDescriptions[selectedItem.Value] + " " + prices[selectedItem.Value] + "G");
            }
        }*/
    }
}
