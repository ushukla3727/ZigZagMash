using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSScript : MonoBehaviour
{
    public AdmobScript ads;
    private void OnEnable() {
        ads.ShowRewardedInterstitialAd();
    }
}
