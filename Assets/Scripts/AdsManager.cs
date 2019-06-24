using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager
{
    public static void ShowRewardedAd(ShowOptions showOptions)
    {
        Debug.Log("Showing rewarded ad");
        const string placementId = "rewardedVideo";
        
        if(Advertisement.IsReady(placementId: placementId))
        {
            Advertisement.Show(placementId: placementId, showOptions: showOptions);
        }
    }    
}
