using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ShopPanel : MonoBehaviour
{
    // UI Elements
    [SerializeField]
    private Text playerGemCountText;
    [SerializeField]
    private Image selectionImage;

    // Data Variables    
    private int[] yPos;
    private int[] prices;
    private string[] itemDescriptions;
    private int? selectedItem = null;
    private Player buyer;

    public void Start()
    {
        yPos = new int[] { 105, 0, -103 };
        prices = new int[] { 400, 500, 100 };
        itemDescriptions = new string[] { "Flame Sword", "Flight Boots", "Castle Keys" };
        selectionImage.enabled = false;
    }

    public void SetBuyer(Player currentPlayer)
    {
        buyer = currentPlayer;
        playerGemCountText.text = buyer.GetGemCount() + "G";
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
        // Check if an item was selected
        if (selectedItem == null)
        {
            Debug.Log("Buy button: No item selected.");
            return;
        }

        // Check if player has enough gems
        if (prices[selectedItem.Value] > buyer.GetGemCount())
        {
            // Player does not have enough gems
            Debug.Log("ShopKeeper: Not enough gems for " + itemDescriptions[selectedItem.Value] + " " + prices[selectedItem.Value] + "G");
            return;
        }

        // else: Player has enough gems! 
        // Subtract gems in order to make the purchase!
        buyer.Purchase(item: itemDescriptions[selectedItem.Value], price: prices[selectedItem.Value]);        
        playerGemCountText.text = buyer.GetGemCount() + "G";

        if (selectedItem == 2) // castle key
        {
            Debug.Log("ShopKeeper: here is the castle key!");
            GameManager.Instance.hasKeyToCastle = true;
        }
    }

    public void DidPressAdsButton()
    {
        var showOptions = new ShowOptions
        {
            resultCallback = HandleAdResult
        };
        AdsManager.ShowRewardedAd(showOptions: showOptions);
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("WOOT 100G!!");
                // Add the gems to the player's inventory
                buyer.AddGems(amount: 100);
                // Update the shop panel's player gem count UI
                playerGemCountText.text = buyer.GetGemCount() + "G";                                
                break;
            case ShowResult.Failed:
                Debug.Log("Ad failed. Please check your data connection and try again.");
                break;
            case ShowResult.Skipped:
                Debug.Log("You skipped the ad. Complete the ad to get gems.");
                break;
        }
    }
}
