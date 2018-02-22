using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class LaserGoogleAds : MonoBehaviour {

	private BannerView bannerView;

	public void Start () {
		#if UNITY_ANDROID
			string appId = "ca-app-pub=3940256099942544~3347511713";
		#else
			string appId = "unexpected_platform"
		#endif

		MobileAds.Initialize(appId);
	}

	void Update () {
		
	}
}
