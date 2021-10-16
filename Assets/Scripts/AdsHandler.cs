using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements; // only compile Ads code on supported platforms
#endif

public class AdsHandler : MonoBehaviour {
	private string gameId = "2605705";
	private float chanceToAds = 0.6f;

	public void ShowDefaultAd()
	{
		// Choose if gonna show a ads.
		float result = Random.value;
		if (result <= chanceToAds) {
			StartCoroutine ( ShowAds() );
		}

	}

	IEnumerator ShowAds()
	{
		#if UNITY_ADS
		//Debug.Log("Started advertisement...");
		if (Advertisement.isSupported) { 
			// If runtime platform is supported...
			Advertisement.Initialize (gameId, true); // ...initialize.
		}

		// Wait until Unity Ads is initialized,
		//  and the default ad placement is ready.
		while (!Advertisement.isInitialized || !Advertisement.IsReady("video"))
		{
			yield return new WaitForSeconds(0.5f);
		}
		// Show the default ad placement.
		Advertisement.Show("video");
		#endif

	}
}
