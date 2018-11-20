using UnityEngine;
using UnityEngine.UI;
using Vdopia;

public class VdopiaUnitySample : MonoBehaviour
{
    VdopiaPlugin plugin;

    void Start()                        //Called by Unity
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            plugin = VdopiaPlugin.GetInstance();       //Initialize Plugin Instance

            if (plugin != null)
            {
                //Set Delegate Receiver For Vdopia SDK Ad Event (Not compulsory)
                VdopiaListener.GetInstance().VdopiaAdDelegateEventHandler += onVdopiaEventReceiver;

                //Set USER parameter used for better ad targeting and higher yield (Not mandatory)
                //Developer can pass empty string for any Param like ""
                //Param 1 : Age
                //Param 2 : BirthDate (dd/MM/yyyy)
                //Param 3 : Gender (m/f/u)
                //Param 4 : Marital Status (single/married/unknown)
                //Param 5 : Ethinicty (example : Africans/Asian/Russians)
                //Param 6 : DMA Code (in String format)
                //Param 7 : Postal Code (in String format)
                //Param 8 : Current Postal Code (in String format)
                //Param 9 : Location latitude in string format
                //Param 10 : Location longitude in string format

                plugin.SetAdRequestUserData("", "", "", "", "", "", "", "", "", "");

                //Set APP parameter used better Ad targeting and higher yield
                //Developer can pass empty string for any Param like ""
                //Param 1 : App Name
                //Param 2 : Publisher Name
                //Param 3 : App Domain
                //Param 4 : Publisher Domain
                //Param 5 : PlayStore URL of the App
                //Param 6 : App Category (IAB category)

                plugin.SetAdRequestAppData("Music Rush", "Canvas Mobile Games", "unity-demo.com", "www.facebook.com/canvasmobilegames/", "play.google.com/store/apps/details?id=com.CanvasMobileGames.SuperCasual", "IAB9");

                //Set Test Mode parameter used for Getting Test AD (Not mandatory)
                //Param 1 : boolean : true if test mode enabled else false
                //Param 2 : Hash ID (If you are testing Facebook/Google Partner Test Ad you can get from ADB Logcat)
                plugin.SetAdRequestTestMode(true, "XXXXXXXXXXXXXXXX");

                //Initialize Chocolate Platform Ads Sdk!
                plugin.ChocolateInit("XqjhRR");   //feel free to use our test app key 'XqjhRR'


            }
            else
            {
                Debug.Log("Vdopia Plugin Initialize Error.");
            }

        }

    }

    void Update()  //Called by Unity
    {
        //Empty
    }

    //This is your defined ad event receiver; invoked after you
    //call loadInterstitialAd() or loadRewardAd() which are defined below.
    void onVdopiaEventReceiver(string adType, string eventName)     //Ad Event Receiver
    {
        Debug.Log("Ad Event Received : " + eventName + " : For Ad Type : " + adType);

        if (eventName == "INTERSTITIAL_LOADED")
        {

            showInterstitialAd();

        }
        else if (eventName == "INTERSTITIAL_FAILED")
        {

        }
        else if (eventName == "INTERSTITIAL_SHOWN")
        {

        }
        else if (eventName == "INTERSTITIAL_DISMISSED")
        {

            //New!  Optional.  Silently pre-fetch the next interstitial ad without making
            //any callbacks.  The pre-fetched ad will remain in cache until you call
            //the next LoadInterstitialAd.
            //feel free to use our test app key 'XqjhRR'
            plugin.PrefetchInterstitialAd("XqjhRR");

        }
        else if (eventName == "INTERSTITIAL_CLICKED")
        {

        }
        else if (eventName == "REWARD_AD_LOADED")
        {

            showRewardAd();

        }
        else if (eventName == "REWARD_AD_FAILED")
        {

        }
        else if (eventName == "REWARD_AD_SHOWN")
        {

        }
        else if (eventName == "REWARD_AD_SHOWN_ERROR")
        {

        }
        else if (eventName == "REWARD_AD_DISMISSED")
        {

            //Pre-fetch: Silently pre-fetch the next reward ad without making 
            //any callbacks. The pre-fetched ad will remain in cache until you call
            //the next LoadRewardAd. 
            //feel free to use our test app key 'XqjhRR'
            plugin.PrefetchRewardAd("XqjhRR");

        }
        else if (eventName == "REWARD_AD_COMPLETED")
        {

            //If you setup server-to-server (S2S) rewarded callbacks you can
            //assume your server url will get hit at this time.
            //Or you may choose to reward your user from the client here.

        }
    }


    //===============Interstitial Ad Methods===============

    public void loadInterstitialAd()     //called when btnLoadInterstitial Clicked
    {
        Debug.Log("Load Interstitial...");
        if (Application.platform == RuntimePlatform.Android && plugin != null)
        {
            //Param 1: AdUnit Id (This is your SSP App ID you received from your account manager or obtained from the portal)
            plugin.LoadInterstitialAd("XqjhRR");  //feel free to use our test app key 'XqjhRR'
        }
    }

    public void showInterstitialAd()     //called when btnShowInterstitial Clicked
    {
        Debug.Log("Show Interstitial...");
        if (Application.platform == RuntimePlatform.Android && plugin != null)
        {
            //Make sure Interstitial Ad is loaded before call this method
            plugin.ShowInterstitialAd();
        }
    }

    //===============Rewarded Video Ad Methods===============

    public void requestRewardAd()       //called when btnRequestReward Clicked
    {
        Debug.Log("Request Reward...");
        if (Application.platform == RuntimePlatform.Android && plugin != null)
        {
            //Param 1: AdUnit Id (This is your SSP App ID you received from your account manager or obtained from the portal)
            plugin.RequestRewardAd("XqjhRR");  //feel free to use our test app key 'XqjhRR'
        }
    }

    public void showRewardAd()           //called when btnShowReward Clicked
    {
        Debug.Log("Show Reward...");

        //Make sure Ad is loaded before call this method
        if (Application.platform == RuntimePlatform.Android && plugin != null)
        {
            //Parma 1: Secret Key (Get it from Vdopia Portal: Required if server-to-server callback enabled)
            //Parma 2: User name – this is the user ID of your user account system
            //Param 3: Reward Currency Name or Measure
            //Param 4: Reward Amount
            plugin.ShowRewardAd("qj5ebyZ0F0vzW6yg", "Chocolate1", "coin", "30");

        }

    }

    public void checkRewardAdAvailable()
    {
        Debug.Log("Check Reward Ad Available...");
        if (Application.platform == RuntimePlatform.Android && plugin != null)
        {
            bool isReady = plugin.IsRewardAdAvailableToShow();
            Debug.Log("Check Reward Ad Available..." + isReady);
        }
    }

}

