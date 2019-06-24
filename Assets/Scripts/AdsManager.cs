using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
        Debug.Log("Showing rewarded ad");
        const string placementId = "rewardedVideo";
        
        if(Advertisement.IsReady(placementId: placementId))
        {
            var showOptions = new ShowOptions
            {
                resultCallback = HandleShowResult
            };
            Advertisement.Show(placementId: placementId, showOptions: showOptions);
        }
    }

    void HandleShowResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                // Award 100 gems
                Debug.Log("WOOT 100G!!");
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
