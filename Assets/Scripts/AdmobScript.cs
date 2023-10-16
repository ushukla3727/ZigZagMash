using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdmobScript : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        LoadRewardedInterstitialAd();
        LoadRewardedAd();
    }

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-6805333443609748/3268221540";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-6805333443609748/3268221540";
#else
  private string _adUnitId = "unused";
#endif

  private RewardedInterstitialAd rewardedInterstitialAd;

  /// <summary>
  /// Loads the rewarded interstitial ad.
  /// </summary>
  public void LoadRewardedInterstitialAd()
  {
      // Clean up the old ad before loading a new one.
      if (rewardedInterstitialAd != null)
      {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
      }

      Debug.Log("Loading the rewarded interstitial ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest.Builder().Build();

      // send the request to load the ad.
      RewardedInterstitialAd.Load(_adUnitId, adRequest,
          (RewardedInterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              rewardedInterstitialAd = ad;
          });
     }

     public void ShowRewardedInterstitialAd()
    {
        const string rewardMsg =
            "Rewarded interstitial ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Rewarded interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open full screen content " +
                        "with error : " + error);
        };
    }



    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
  private string _adUnitIdReward = "ca-app-pub-6805333443609748/7286507546";
#elif UNITY_IPHONE
  private string _adUnitIdReward = "ca-app-pub-6805333443609748/7286507546";
#else
  private string _adUnitIdReward = "unused";
#endif

  private RewardedAd rewardedAd;

  /// <summary>
  /// Loads the rewarded ad.
  /// </summary>
  public void LoadRewardedAd()
  {
      // Clean up the old ad before loading a new one.
      if (rewardedAd != null)
      {
            rewardedAd.Destroy();
            rewardedAd = null;
      }

      Debug.Log("Loading the rewarded ad.");

      // create our request used to load the ad.
      var adRequest = new AdRequest.Builder().Build();

      // send the request to load the ad.
      RewardedAd.Load(_adUnitIdReward, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              rewardedAd = ad;
          });
  }

public void ShowRewardedAd()
{
    const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (rewardedAd != null && rewardedAd.CanShowAd())
    {
        rewardedAd.Show((Reward reward) =>
        {
            // TODO: Reward the user.
            Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            PlayerPrefs.SetInt("Currency",PlayerPrefs.GetInt("Currency")+ UnityEngine.Random.Range(200,300));
        });
    }
    LoadRewardedAd();
}
private void RegisterEventHandlers(RewardedAd ad)
{
    // Raised when the ad is estimated to have earned money.
    ad.OnAdPaid += (AdValue adValue) =>
    {
        Debug.Log(string.Format("Rewarded ad paid {0} {1}.",
            adValue.Value,
            adValue.CurrencyCode));
    };
    // Raised when an impression is recorded for an ad.
    ad.OnAdImpressionRecorded += () =>
    {
        Debug.Log("Rewarded ad recorded an impression.");
        
    };
    // Raised when a click is recorded for an ad.
    ad.OnAdClicked += () =>
    {
        Debug.Log("Rewarded ad was clicked.");
    };
    // Raised when an ad opened full screen content.
    ad.OnAdFullScreenContentOpened += () =>
    {
        Debug.Log("Rewarded ad full screen content opened.");
    };
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Rewarded ad full screen content closed.");
    };
    // Raised when the ad failed to open full screen content.
    ad.OnAdFullScreenContentFailed += (AdError error) =>
    {
        Debug.LogError("Rewarded ad failed to open full screen content " +
                       "with error : " + error);
    };
}

public void DeletePLayerPrefs()
{
    PlayerPrefs.DeleteAll();
}
}
