using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class store_controller : MonoBehaviour {

	#region === Vars ===
    public static store_controller s;

	[Header ("Gems")]
	public GameObject mainCategory;
	public GameObject[] promoCategories;
	public Categories curCategory = Categories.Main;

	[Header ("Gems")]
	[SerializeField] Text myGemsPrice;

	[Header ("Bottom Coins")]

	public Animator myCoinsFullAnimator;
	[SerializeField] Text myCoinsQuantity;
	[SerializeField]GameObject[] myCoins;
	[SerializeField]GameObject myCoinsFalling, myCoinsPile;
	[SerializeField]Animator CoinsFallingAnimator;
	bool coinsAreFalling;

	[SerializeField]Button myBackBt;

	[SerializeField]GameObject myU, mYTitle, myBgLights, myLockedBg;
	[Space (10)] 

	public RewardScreen myRewardScreen;

	public int jukeboxCurrentPrice;

	[Header ("Chars")]
	[SerializeField] GameObject genericChar;
	[SerializeField] GameObject[] myChars;
	[SerializeField] GameObject[] myPromos;
	GameObject lastChar;

	public GameObject[] lightLeftLines, lighttRightLines;
	public ScrollSnapRect ScrollSnap;

	public Text title;
	public Button jukeboxBt;
	public Text jukeboxBtNotesLow, jukeboxBtNotesHigh;

    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
	[HideInInspector]public int rockAlreadyBuyed;
	[HideInInspector]public int popGagaAlreadyBuyed;
	[HideInInspector]public int reggaeAlreadyBuyed;
	[HideInInspector]public int rapAlreadyBuyed;

	int[] alreadyBuyed;
	public int nCharsBuyed = 0;

//    public int popPrice = 70;
//    public int rockPrice = 50;
//	public int popGagaPrice = 70;
//	public int reggaePrice = 50;
//    [HideInInspector]
//    public int eletronicPrice = 30;
//
    public Text actualCoins;


	[Header ("Button buy")]
	public Text buyGemsPrice;
	public Text buyRealMoney;
    public GameObject buyButton;

    public GameObject equipButton;

	[Header ("SPECIAL OFFER")]
	public Text specialOfferTime;
	public GameObject specialOfferButton;

	public GameObject frameVIP;
    public GameObject GemsStore;

	public Text nextSaturdayTimer;

    int actualCharInScreen;
	MusicStyle actualStyle, lastSortedStyle = MusicStyle.Eletro;
	int lastSortedSkin = -2;

	float yStartCoinsPile = -1068, yEndCoinsPile = -575, yIncCoinsPile;

	#endregion
    // Use this for initialization

	#region === Init ===
    void Start () {
//		USER.s.NOTES = 98;
//		USER.s.NOTES = 101;
//		globals.s.NOTES_COLLECTED_JUKEBOX = 8 ;


//		StartCoroutine (InitCoinFallingAnimation (globals.s.NOTES_COLLECTED_JUKEBOX));

        s = this;
//		USER.s.AddNotes (70);

		PlayerPrefs.SetInt(GD.s.skins[0].skinName+"AlreadyBuyed", 1);
//		PlayerPrefs.SetInt(MusicStyle.Rap.ToString()+"AlreadyBuyed", 1);

//        popAlreadyBuyed = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
//		rockAlreadyBuyed = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);
//		popGagaAlreadyBuyed = PlayerPrefs.GetInt("popGagaAlreadyBuyed", 0);
//		reggaeAlreadyBuyed = PlayerPrefs.GetInt("reggaeAlreadyBuyed", 0);
//		eletronicAlreadyBuyed = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);
//		rapAlreadyBuyed = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);

		alreadyBuyed = new int[GD.s.skins.Length];

//		alreadyBuyed[(int)MusicStyle.Pop] = PlayerPrefs.GetInt("PopAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Rock] = PlayerPrefs.GetInt("RockAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.PopGaga] = PlayerPrefs.GetInt("PopGagaAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Reggae] = PlayerPrefs.GetInt("ReggaeAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Eletro] = PlayerPrefs.GetInt("EletronicAlreadyBuyed", 1);
//		alreadyBuyed[(int)MusicStyle.Rap] = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);
//		alreadyBuyed[(int)MusicStyle.Classic] = PlayerPrefs.GetInt("classicAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Latina] = PlayerPrefs.GetInt("latinacAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Samba] = PlayerPrefs.GetInt("sambaAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.DingoBells] = PlayerPrefs.GetInt("dingoBellscAlreadyBuyed", 0);

		if(FTUController.s.firstSongPurchased == 1){
			for (int i = 0; i < alreadyBuyed.Length; i++) {
//			PlayerPrefs.SetInt(((MusicStyle)i).ToString()+"AlreadyBuyed", 0);
				int unlocked = 0;
				if (QA.s.ALL_UNLOCKED)
					unlocked = 1;
				alreadyBuyed [i] = PlayerPrefs.GetInt (GD.s.skins [i].skinName + "AlreadyBuyed", unlocked);
//			Debug.Log("ALREADY BUYED I: "+ i + " TRUE: " + alreadyBuyed[i]);
				if (alreadyBuyed [i] == 1)
					nCharsBuyed++;
			}
		}

//		changeAnimationEquipButton("eletronic");
//		changeAnimationEquipButtonNew(MusicStyle.Eletro);

//		DefineActualCharOnTheScreen ();
		DefineActualCharOnTheScreenNew ();
//		OnCharacterChanged(actualCharInScreen);
		OnCharacterChangedNew(actualCharInScreen, true);

//        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

		if (globals.s.FIRST_GAME) {
			actualCharInScreen = 0;
//			equipCharacter ();

			equipCharacterNew ();
		}

//		StartCoroutine (LightAnimations ());
    }
	bool genericCharPosAlreadyDefined = false;
	public void DefineGenericCharPosition(){
		if (genericCharPosAlreadyDefined == false) {
			Debug.Log ("[JUKE] DEFINE GENERIC CHAR POS!!!! : " + GD.s.N_SKINS); 
			genericChar.transform.SetSiblingIndex (GD.s.N_SKINS);
			genericCharPosAlreadyDefined = true;
		}
	}

	void DefineActualCharOnTheScreenNew(){
//		actualCharInScreen = (int)globals.s.ACTUAL_STYLE;
		actualCharInScreen = globals.s.ACTUAL_SKIN.id;
		actualStyle = globals.s.ACTUAL_STYLE;
	}

	bool coinsPositionAlreadySetted = false;
	public void OpenStore(){
		Invoke ("OpenStore2", 0.1f);
		if (coinsPositionAlreadySetted == false) {
			coinsPositionAlreadySetted = true;
			SetPileOfCoinsInitalPosition ();
		}

		if (globals.s.NOTES_COLLECTED_JUKEBOX > 0) {
			Debug.Log (" OPEN NOTES COLLECTED : " + globals.s.NOTES_COLLECTED_JUKEBOX);
			StartCoroutine (InitCoinFallingAnimation (globals.s.NOTES_COLLECTED_JUKEBOX));
		} else {
//			myCoinsFalling.GetComponent<Animator> ().enabled = false;
			myCoinsFalling.SetActive(false);
			myCoinsQuantity.text = USER.s.NOTES + "/"+globals.s.JUKEBOX_CURRENT_PRICE;
		}
//		DisplayNotes ();
//		changeAnimationEquipButtonNew(MusicStyle.Eletro);
//		OnCharacterChangedNew((MusicStyle)actualCharInScreen);
//		OnCharacterChangedNew((MusicStyle)actualCharInScreen);
	}

	void OpenStore2(){
		lastChar = null;

		if (CohortMaster.s.newCharacterForOpenAtStore == 0 || USER.s.NEWBIE_PLAYER == 1 || FTUController.s.firstSongPurchased == 0) {
			changeAnimationEquipButtonNew (globals.s.ACTUAL_SKIN.id);
//		ScrollSnap.SetCurrentPage((MusicStyle)globals.s.ACTUAL_STYLE);
//		myChars[globals.s.ACTUAL_SKIN.id].SetActive(true);
			ScrollSnap.SetCurrentPage (globals.s.ACTUAL_SKIN.id);
			OnCharacterChangedNew (globals.s.ACTUAL_SKIN.id, true);
			title.text = globals.s.ACTUAL_SKIN.skinName;
			sound_controller.s.curJukeboxMusic = globals.s.ACTUAL_STYLE;
		} else {
			changeAnimationEquipButtonNew (CohortMaster.s.newCharacterForOpenAtStore);

			ScrollSnap.SetCurrentPage (CohortMaster.s.newCharacterForOpenAtStore);
			OnCharacterChangedNew (CohortMaster.s.newCharacterForOpenAtStore);
			title.text = GD.s.skins[CohortMaster.s.newCharacterForOpenAtStore].skinName;
			CohortMaster.s.newCharacterForOpenAtStore = 0;
		}

		DisplayGemsValues ();

		if (FTUController.s.firstSongPurchased == 0) {
			myBackBt.gameObject.SetActive (false);
			equipButton.gameObject.SetActive (false);
			specialOfferButton.SetActive (false);
		}

		if (CountHowManyStylesPlayerHas () >= GD.s.N_STYLES) {
			specialOfferButton.SetActive (false);
		}
		else{
			USER.s.CheckIfSpecialOfferTimeHasElapsedAndUpdate ();
			dateNextPromo = Convert.ToDateTime (USER.s.SPECIAL_OFFER_END_DATE);
		}

		//next saturday logic

		dateNextSaturday = Convert.ToDateTime (USER.s.NEXT_SATURDAY);

	}

	public void CloseStore(bool fromBackBt){
//		if(fromBackBt == true) sound_controller.s.change_music((MusicStyle)globals.s.ACTUAL_STYLE);
//				if(fromBackBt == true) sound_controller.s.SoltaOSomAeDJAndreMarques(globals.s.ACTUAL_STYLE);
		if(fromBackBt == true) {
			if (curCategory == Categories.Main) {
				sound_controller.s.SoltaOSomAeDJAndreMarques (globals.s.ACTUAL_STYLE);
			} else {
				ChangeCategoryTo (Categories.Main);
			}
		}
		else equipCharacterNew ();
	}

	IEnumerator LightAnimations(){
		int curLine = 0;
		int max = 0;
		for (int i = 0; ;i++) {
			if (1==1 || globals.s.curGameScreen == GameScreen.Store) {
				lightLeftLines [i].SetActive (false);
				lighttRightLines [i].SetActive (false);
				yield return new WaitForSeconds (0.15f);

				lightLeftLines [i].SetActive (true);
				lighttRightLines [i].SetActive (true);

				if (i == lightLeftLines.Length-1) {
					i = -1;
				}

			} else
				break;

			max++;
			if (max > 100)
				break;
		}
	}

	#endregion

	#region === Update ===
	public void UpdateUserNotes(){
		actualCoins.text = USER.s.NOTES.ToString("00");
		DisplayNotes ();
	}


	#endregion

    #region === CHAR ===
	public bool CheckIfCharacterIsAlreadyPurchasedNew(int skinId){
		if (alreadyBuyed [skinId] == 1)
			return true;
		else
			return false;
	}

	public void GiveCharacterForFreeNew(int skinId){
		actualCharInScreen = skinId;
		actualStyle = GD.s.skins [skinId].musicStyle;

//		equipCharacter();
		equipCharacterNew();

		PlayerPrefs.SetInt (GD.s.skins[skinId].skinName + "AlreadyBuyed", 1);
		alreadyBuyed [skinId] = 1;
	}

//	public void GiveCharacterForFree(int musicStyle = -1)
//	{
//		actualCharInScreen = musicStyle;
//		equipCharacter();
//
//		if(musicStyle == 0)
//		{
//			PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
//			eletronicAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 1)
//		{
//			PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
//			rockAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 2)
//		{
//			PlayerPrefs.SetInt("popAlreadyBuyed", 1);
//			popAlreadyBuyed = 1;
//		}
//
//		else if (musicStyle == 3)
//		{
//			PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
//			popGagaAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 4)
//		{
//			PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
//			reggaeAlreadyBuyed = 1;
//		}
//
//		else if (musicStyle == 5)
//		{
//			PlayerPrefs.SetInt("rapAlreadyBuyed", 1);
//			reggaeAlreadyBuyed = 1;
//		}
//	}
//

    void buyed()
    {
        buyButton.SetActive(false);
        equipButton.SetActive(true);
        equipCharacterNew();
    }
    
	public void equipCharacterNew(bool dontChangeMusic = false){
//		USER.s.SetCurrentSelectedMusic ((MusicStyle)actualCharInScreen,  1);
		USER.s.SetCurrentSelectedMusic (GD.s.skins[actualCharInScreen].musicStyle,  actualCharInScreen);
		changeActualCharSkin ();

//		changeAnimationEquipButton("eletronic");
		changeAnimationEquipButtonNew(actualCharInScreen);
		if(dontChangeMusic == false) sound_controller.s.change_music(globals.s.ACTUAL_STYLE);
	}

	public void OnCharacterChangedNew(int skinId, bool dontPlayMusic = false, bool dontDeactivateLast = false) {
		if (lastChar != null && dontDeactivateLast == false && globals.s.JUKEBOX_SORT_ANIMATION == false )
			StartCoroutine (DeactivateLastChar (lastChar, skinId));
		myChars [skinId].SetActive (true);
		lastChar = myChars [skinId];

		if (skinId < GD.s.N_SKINS ) {
			MusicStyle style = GD.s.skins [skinId].musicStyle;
//			if (lastChar != null && dontDeactivateLast == false)
//				StartCoroutine (DeactivateLastChar (lastChar));
//			myChars [skinId].SetActive (true);
//			lastChar = myChars [skinId];

//		Debug.Log ("[JUKEBOX] Character changed new: " + style.ToString());
//		Debug.Log ("[JUKEBOX] Character changed new: " + GD.s.skins[skinId].skinName);
			actualCharInScreen = skinId;
			actualStyle = style;

			if (GD.s.skins [skinId].isClothChanger || GD.s.skins [skinId].isBand)
				frameVIP.SetActive (true);
			else
				frameVIP.SetActive (false);

			nextSaturdayTimer.gameObject.SetActive (false);

//		title.text = GD.s.GetStyleName (style);
			title.text = GD.s.skins [skinId].skinName;

			if (globals.s.JUKEBOX_SORT_ANIMATION == false && dontPlayMusic == false) 
//			sound_controller.s.change_music(style);
				sound_controller.s.ChangeMusicForStore (style);

			if (alreadyBuyed [skinId] == 0) { // NOT BUYED CASE
//			equipButton.GetComponent<Animator> ().Play ("select off");
				equipButton.GetComponent<Button> ().interactable = false;
				myBgLights.SetActive (false);
				myLockedBg.SetActive (true);
				buyButton.SetActive (true);

				myGemsPrice.text = GetCurrentCharGemPrice (actualCharInScreen).ToString();

			} else { // ALREADY OWNED CASE
				equipButton.GetComponent<Button> ().interactable = true;
				myLockedBg.SetActive (false);
				myBgLights.SetActive (true);
				buyButton.SetActive (false);
			}
		} else { // GENERIC CHAR CASE 
			actualCharInScreen = skinId;
			if (RemoteMaster.s.maximumAllowedChars == GD.s.N_SKINS) {
				title.text = "More comming soon";
				nextSaturdayTimer.gameObject.SetActive (false);
			} else {
				title.text = "More next Saturday";
				nextSaturdayTimer.gameObject.SetActive (true);
			}

			equipButton.GetComponent<Button> ().interactable = false;

			myLockedBg.SetActive (false);
			myBgLights.SetActive (true);
			buyButton.SetActive (false);

			frameVIP.SetActive (false);
		}
//		changeAnimationEquipButtonNew(style);
	}

	IEnumerator DeactivateLastChar(GameObject character, int id = 0){
		yield return new WaitForSeconds (0.2f);
		if(character != null && id != actualCharInScreen) character.SetActive (false);
	}


	public void changeAnimationEquipButtonNew(int skinId) {
		if (CheckIfCharacterIsAlreadyPurchasedNew(skinId)) {
			equipButton.GetComponent<Button> ().interactable = true;
//			equipButton.GetComponent<Animator>().Play("selected");
		}
		else
			equipButton.GetComponent<Button> ().interactable = false;
//			equipButton.GetComponent<Animator>().Play("select");
	}

//
//    public void changeAnimationEquipButton(string inShopType)
//    {
//
//        if (globals.s.ACTUAL_CHAR == inShopType)
//        {
//			equipButton.GetComponent<Button> ().interactable = false;
//            equipButton.GetComponent<Animator>().Play("selected");
//        }
//        else
//        {
//            equipButton.GetComponent<Animator>().Play("select");
//        }
//    }

    void changeActualCharSkin()
    {
		BallMaster.s.UpdateBallsSkin ();
//        ball_hero[] balls = FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
//        for(int i=0;i<balls.Length;i++)
//        {
////			balls[i].changeSkinChar();
//			balls[i].UpdateMySkin();
//        }
    }
    #endregion

    #region === COINS ===

	public void DisplayNotes(){
		myCoinsQuantity.text = USER.s.NOTES + "/"+globals.s.JUKEBOX_CURRENT_PRICE;

		if (globals.s.JUKEBOX_CURRENT_PRICE == GD.s.JUKEBOX_FTU_PRICE) {
//			jukeboxBtNotesLow.gameObject.SetActive (true);
//			jukeboxBtNotesHigh.gameObject.SetActive (false);
//			jukeboxBtNotesLow.text = globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		} else {
//			jukeboxBtNotesLow.gameObject.SetActive (false);
//			jukeboxBtNotesHigh.gameObject.SetActive (true);
//			jukeboxBtNotesHigh.text = globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}


		// CHECK IF IS FULL
		if (nCharsBuyed >= GD.s.N_MUSIC) { //TBD ARRUMAR
//			jukeboxBtNotesLow.gameObject.SetActive (false);
//			jukeboxBtNotesHigh.gameObject.SetActive (true);
//			jukeboxBt.interactable = false;
//			jukeboxBtNotesHigh.text = "full";
		}
//		else if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
//			jukeboxBt.interactable = true;
////			jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
//		}
		else {
//			jukeboxBt.interactable = false;
			if (USER.s.NOTES < 10) {
//				jukeboxBtNotes.text = "0" + USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
			else{
//				jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
		}
	}

//	IEnumerator InitCoinsAnimationLogic(){
////		yield new WaitUntil
//	}


	void SetPileOfCoinsInitalPosition(){


		yIncCoinsPile = Mathf.Abs(yEndCoinsPile - yStartCoinsPile ) / globals.s.JUKEBOX_CURRENT_PRICE;


		if (globals.s.JUKEBOX_CURRENT_PRICE > USER.s.NOTES - globals.s.NOTES_COLLECTED_JUKEBOX) {
			myCoinsPile.transform.localPosition = 
				new Vector2 (myCoinsPile.transform.localPosition.x, yStartCoinsPile + yIncCoinsPile * (USER.s.NOTES - globals.s.NOTES_COLLECTED_JUKEBOX));
		} else {
			myCoinsPile.transform.localPosition = 
				new Vector2 (myCoinsPile.transform.localPosition.x, yStartCoinsPile + yIncCoinsPile * (globals.s.JUKEBOX_CURRENT_PRICE + 5));

			if ( USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE)
				StartCoroutine (InitCoinsFullAnimation ());
		}
		Debug.Log ("[JUKE] 'Y INITIAL POS: " +  myCoinsPile.transform.localPosition.y + " + Y INC: " + yIncCoinsPile);
	}

	public IEnumerator InitCoinFallingAnimation(int nCoins = 0){
		Debug.Log ("[JUKE] COIIIIIIIINS ANIMATION");
		yield return new WaitForSeconds (0.5f);
		int initialCoins = USER.s.NOTES - nCoins;
		myCoinsFalling.SetActive (true);
		myCoinsFalling.GetComponent<Animator> ().Play ("JukeboxCoinsFallingAnimation2");
		for (int i = 0; i <= nCoins && initialCoins + i <= globals.s.JUKEBOX_CURRENT_PRICE; i++) {
			myCoinsPile.transform.localPosition = new Vector2 (myCoinsPile.transform.localPosition.x, myCoinsPile.transform.localPosition.y + yIncCoinsPile);
			myCoinsQuantity.text = initialCoins + i + "/"+globals.s.JUKEBOX_CURRENT_PRICE;
//			Debug.Log ("CCCCCCCC COINS: " + (initialCoins + i));
			if(nCoins < 20) yield return new WaitForSeconds (0.14f);
			else yield return new WaitForSeconds (0.07f);
			if(i % 2 == 0)
				sound_controller.s.PlaySfxUIJukeboxCoinFalling ();
		}

		myCoinsFalling.SetActive (false);
		globals.s.NOTES_COLLECTED_JUKEBOX = 0;

		yield return new WaitForSeconds (0.2f);

		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE)
			StartCoroutine (InitCoinsFullAnimation ());
	}

	public IEnumerator InitCoinsFullAnimation(){
		jukeboxBt.gameObject.SetActive (true);
		myCoinsFullAnimator.enabled = true;
//		myCoinsFullAnimator.SetTrigger ("CoinsFull");

		yield return new WaitForSeconds (1f);
//		myCoinsFullAnimator.ResetTrigger ("CoinsFull");
//
		jukeboxBt.GetComponent<Button> ().interactable = true;

	}

   
    #endregion

	public void buyCharacterWithGems(bool dontChangeMusic = false){
		//		USER.s.SetCurrentSelectedMusic ((MusicStyle)actualCharInScreen,  1);
		USER.s.SetCurrentSelectedMusic (GD.s.skins[actualCharInScreen].musicStyle,  actualCharInScreen);
		changeActualCharSkin ();

		//		changeAnimationEquipButton("eletronic");
		changeAnimationEquipButtonNew(actualCharInScreen);
		if(dontChangeMusic == false) sound_controller.s.change_music(globals.s.ACTUAL_STYLE);
	}

	#region ==== Buying and Animation ====

	public void SetBuyButtonState(){
		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxBt.interactable = true;
		}
		else
			jukeboxBt.interactable = false;
	}


	public void BuyRandomCharacter(){
		if (nCharsBuyed < GD.s.N_SKINS) {
			jukeboxBt.GetComponent<Button> ().interactable = false;
			lastSortedSkin = -2;
			StartCoroutine (StartRoulleteAnimation ());

//			USER.s.NOTES -= globals.s.JUKEBOX_CURRENT_PRICE; 
			USER.s.SpendUserNotes (globals.s.JUKEBOX_CURRENT_PRICE, "BuyRandomSkin");
//			USER.s.SaveUserNotes ();

//			DisplayNotes ();
//			UpdateUserNotes();
			myCoinsQuantity.text = "0/"+globals.s.JUKEBOX_CURRENT_PRICE;
//			}
		} else {
			Debug.Log ("[JK] WOOOOOOOOOOOW! all chars purchaseD!! nCharsBuyed: " + nCharsBuyed + " GD N SKINS: "+ GD.s.N_SKINS);
		}
	}

	/// <summary>
	/// Logic Of the sort animation
	/// </summary>
	/// <returns>The roullete animation.</returns>
	/// 

	bool showSpecialOfferButtonFirstTime = false;
	public IEnumerator StartRoulleteAnimation(){

		//FALLING COINS ANIMATION
		myCoinsFullAnimator.SetTrigger("BuyButtonPressed");
//		myCoinsFullAnimator.ResetTrigger("BuyButtonPressed");
		yield return new WaitForSeconds (0.8f);

		globals.s.JUKEBOX_SORT_ANIMATION = true;
		int rand = UnityEngine.Random.Range (10, 13);

		myBackBt.interactable = false;
		jukeboxBt.interactable = false;

		//change animation speed
		myU.GetComponent<Animator>().speed = 3f;
		mYTitle.GetComponent<Animator>().speed = 3f;
		myBgLights.GetComponent<Animator>().speed = 3f;

		int skinToGive;
		if (FTUController.s.firstSongPurchased == 0) {
			skinToGive = 15; // mozart as first
			FTUController.s.SetFirstSongPurchased ();
			showSpecialOfferButtonFirstTime = true;
		}
		else skinToGive = SortCharToDrop (lastSortedSkin); 
		//
		Debug.Log ("[[[[ [JUKE] SORTED CHAR TO GIVE: " + GD.s.skins [skinToGive].skinName);
		sound_controller.s.PlaySfxUIJukeboxSortingCharacter();
		int k = 0;
		do { // FAZER UM MINIMO 
			for (int i = 0; i < rand; i++) { //logica de não repetir
				k++;
				ScrollSnap.NextScreen ();
				yield return new WaitForSeconds (0.15f);
//				if( k % 5 == 0 ) sound_controller.s.PlaySfxUIJukeboxSortingCharacter();
			}
			rand = 1;
//			rand = Random.Range (1, 3);
//			Debug.Log("STYLE FOUND: "+ (MusicStyle)actualStyle + " have " +CheckIfCharacterIsAlreadyPurchasedNew(skinId) );
		} while ( actualCharInScreen != skinToGive  || actualCharInScreen == lastSortedSkin); // TBD SE FOR O ULTIMO A DROPAR VAI DAR MERDA NO LAST SORTED SKIN
//		} while (alreadyBuyed [(int)actualStyle] == 0);
//		} while (CheckIfCharacterIsAlreadyPurchasedNew(actualCharInScreen) == true || actualCharInScreen == lastSortedSkin);

		globals.s.JUKEBOX_SORT_ANIMATION = false;
//		OnCharacterChangedNew (actualStyle);
		sound_controller.s.StopSfxEffect();

		yield return new WaitForSeconds(0.1f);

		Debug.Log("STYLE FOUND 2222: id: "+ actualCharInScreen + " name: " + GD.s.skins[actualCharInScreen].skinName + " have " +CheckIfCharacterIsAlreadyPurchasedNew(actualCharInScreen));

		lastSortedStyle = actualStyle;
		lastSortedSkin = actualCharInScreen;

		StartCoroutine (GiveReward ());

//		hud_controller.si.GiftButtonClicked (actualStyle);
	}

	int SortCharToDrop(int dontSortThisSkin = -1){
		int charToGive = -1;

		do {
			List<Skin> commonChars = new List<Skin> ();
			List<Skin> uncommonChars = new List<Skin> ();
			List<Skin> rareChars = new List<Skin> ();
//		List<Skin> epicChars = new List<Skin> ();

			//mount possible char list
			for (int i = 0; i < GD.s.N_SKINS; i++) {
				if (alreadyBuyed [i] == 0) {
					if (GD.s.skins [i].rarity == SkinRarity.common)
						commonChars.Add (GD.s.skins [i]);
					else if (GD.s.skins [i].rarity == SkinRarity.uncommon)
						uncommonChars.Add (GD.s.skins [i]);
					else if (GD.s.skins [i].rarity == SkinRarity.rare)
						rareChars.Add (GD.s.skins [i]);
//				else if (GD.s.skins [i].epicChars == SkinRarity.epic)
//					epicChars.Add (GD.s.skins [i]);
				}
			}

			//check available raririty and define chance sort values
			int maxRand = 0, commonChance = 0, uncommonChance = 0, rareChance = 0, epicChance = 0;
			if (commonChars.Count > 0) {
				maxRand += GD.s.GD_DROP_CHANCE_COMMON;
				commonChance = GD.s.GD_DROP_CHANCE_COMMON;
			}
			if (uncommonChars.Count > 0) {
				maxRand += GD.s.GD_DROP_CHANCE_UNCOMMON;
				uncommonChance = GD.s.GD_DROP_CHANCE_UNCOMMON;
			}
			if (rareChars.Count > 0) {
				maxRand += GD.s.GD_DROP_CHANCE_RARE;
				rareChance = GD.s.GD_DROP_CHANCE_RARE;
			}
			
			//start the sort
			int rand = UnityEngine.Random.Range (0, maxRand);

			if (commonChars.Count > 0 && rand < commonChance) {
				rand = UnityEngine.Random.Range (0, commonChars.Count);
				charToGive = commonChars.ToArray () [rand].id;
			} else if (uncommonChars.Count > 0 && rand < commonChance + uncommonChance) {
				rand = UnityEngine.Random.Range (0, uncommonChars.Count);
				charToGive = uncommonChars.ToArray () [rand].id;
			} else if (rareChars.Count > 0 && rand < commonChance + uncommonChance + rareChance) {
				rand = UnityEngine.Random.Range (0, rareChars.Count);
				charToGive = rareChars.ToArray () [rand].id;
			} else {
				charToGive = -1;
				Debug.LogError (" **** OUT OF CHARS ! THAT SHOULD NEVER HAPPEN *****");
			}

		} while (dontSortThisSkin == charToGive || charToGive == -1);

		return charToGive;
	}


	IEnumerator GiveReward() { //After the sort, show the Reward Screen
		purchaseFromGems = false;
		OnCharacterChangedNew (actualCharInScreen, false, true); // TBDCHAR
		globals.s.curGameScreen = GameScreen.RewardCharacter;
		if(nCharsBuyed >= GD.s.N_SKINS-1)
			myRewardScreen.showVideoButton = false;
		else
			myRewardScreen.showVideoButton = true;
			// tbd check if there is chars left

		GameObject curChar = null;
//		foreach (GameObject character in myChars) {
//			if (character.name == actualStyle.ToString ()) {
		for(int k=0; k < myChars.Length; k++) {
			if (k == actualCharInScreen) {
				curChar = myChars[k];
				break;
			}
		}

//		if (curChar.GetComponent<Animator> () != null)
//			myRewardScreen.SetMyRewardChar (curChar.GetComponent<Animator> ().runtimeAnimatorController, GD.s.skins [actualCharInScreen].skinName);
//		else {
//			Debug.Log ("LETS KIDNAP SOME CHILDERN");
//			myRewardScreen.SetMyRewardChar (curChar.GetComponentInChildren<Animator> ().runtimeAnimatorController, GD.s.skins [actualCharInScreen].skinName);
//		}
		myRewardScreen.SetMyRewardCharV2(curChar, GD.s.skins [actualCharInScreen].skinName);

//		myRewardScreen.SetMyRewardChar (curChar.GetComponent<Animator> ().runtimeAnimatorController, GD.s.GetStyleName(actualStyle));
//		myRewardScreen.myReward = curChar;
		yield return new WaitForSeconds(0.5f);

		myRewardScreen.gameObject.SetActive (true);

		//change animation speed back to normal
		myU.GetComponent<Animator>().speed = 1f;
		mYTitle.GetComponent<Animator>().speed = 1f;
		myBgLights.GetComponent<Animator>().speed = 1f;
		jukeboxBt.gameObject.SetActive (false);
	}

	IEnumerator GiveRewardFromGemsPurchase() { //Purchasing by Gems
		purchaseFromGems = true;
		OnCharacterChangedNew (actualCharInScreen, false, true); // TBDCHAR
		globals.s.curGameScreen = GameScreen.RewardCharacter;
		GameObject curChar = null;
		myRewardScreen.showVideoButton = false;

		for(int k=0; k < myChars.Length; k++) {
			if (k == actualCharInScreen) {
				curChar = myChars[k];
				break;
			}
		}
			
		myRewardScreen.SetMyRewardCharV2(curChar, GD.s.skins [actualCharInScreen].skinName);

		yield return new WaitForSeconds(0.5f);

		myRewardScreen.gameObject.SetActive (true);

		jukeboxBt.gameObject.SetActive (false);
	}

	IEnumerator GiveRewardSpecialOffer(GameObject curChar, string name) { //Purchasing by Gems

		globals.s.curGameScreen = GameScreen.RewardCharacter;

		myRewardScreen.SetMyRewardCharV2(curChar, name);
		myRewardScreen.showVideoButton = false;

		yield return new WaitForSeconds(0.5f);

		myRewardScreen.gameObject.SetActive (true);

		jukeboxBt.gameObject.SetActive (false);
	}

	void UnlockCharactersForSpecialPack(MusicStyle style){
		//		PlayerPrefs.SetInt (actualStyle.ToString () + "AlreadyBuyed", 1);
		for(int id =  0 ; id < alreadyBuyed.Length; id++)  {
			if (GD.s.skins [id].musicStyle == style && alreadyBuyed [id] == 0) {
				PlayerPrefs.SetInt (GD.s.skins [id].skinName + "AlreadyBuyed", 1);
				nCharsBuyed++;
				alreadyBuyed [id] = 1;
			}
		}
	}
		
	public void WatchedVideoForResort(){
		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.ResetMyRewardChar ();
		myRewardScreen.gameObject.SetActive (false);

		StartCoroutine (StartRoulleteAnimation ());
	}

	// === COLLECT BUTTON FROM THE REWARD SCREEN!! ====
	public void OnButtonRewardPressed(){
		myRewardScreen.ResetMyRewardChar ();
		ActivatePlayAndBackButtonsAgain ();
//		jukeboxBt.interactable = true; //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
//		UpdateUserNotes(); //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
		globals.s.MENU_OPEN = false;

		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.gameObject.SetActive (false);

		//give the reward for real
		if (curCategory == Categories.Main) {
			equipCharacterNew (true);
//		PlayerPrefs.SetInt (actualStyle.ToString () + "AlreadyBuyed", 1);
			PlayerPrefs.SetInt (GD.s.skins [actualCharInScreen].skinName + "AlreadyBuyed", 1);
			alreadyBuyed [actualCharInScreen] = 1;
			nCharsBuyed++;

			if (showSpecialOfferButtonFirstTime == true) {
				specialOfferButton.SetActive (true);
				USER.s.SetANewSpecialOffer ();
				DefineNextSpecialPack ();
				showSpecialOfferButtonFirstTime = false;
			}

			// buttons settings
			myBackBt.interactable = true;
			buyButton.SetActive (false);
			myLockedBg.SetActive (false);
			myBgLights.SetActive (true);
			equipButton.GetComponent<Button> ().interactable = true;

			if((MusicStyle) USER.s.CUR_SPECIAL_OFFER == GD.s.skins [actualCharInScreen].musicStyle ) {
				//&& CheckIfPlayerHasASkinOfThisStyle(GD.s.skins [actualCharInScreen].musicStyle)){
				USER.s.SetANewSpecialOffer(); // TBD = DO SOMETHING ELSE? 
				DefineNextSpecialPack();
			}

			// coins pile settings
			if (purchaseFromGems == false) {
				myCoinsFullAnimator.enabled = false;
				SetPileOfCoinsInitalPosition ();
				// FALL THE EXTRA COINS
				if (USER.s.NOTES > 0) {
					//			globals.s.NOTES_COLLECTED_JUKEBOX = USER.s.NOTES;
					StartCoroutine (InitCoinFallingAnimation (USER.s.NOTES));
				}
			}
		} else { // ****** SPECIAL PACK CASE *********
			UnlockCharactersForSpecialPack ((MusicStyle)USER.s.CUR_SPECIAL_OFFER);
			USER.s.SetANewSpecialOffer ();
			DefineNextSpecialPack();

			actualCharInScreen = USER.s.CUR_SPECIAL_OFFER * GD.s.SKINS_PER_MUSIC + 2;
			ChangeCategoryTo (Categories.Main);
		}
	}

	void ActivatePlayAndBackButtonsAgain(){
		if (FTUController.s.firstSongPurchased == 1) {
			myBackBt.gameObject.SetActive (true);
			equipButton.gameObject.SetActive (true);
		}
	}

	#endregion

	#region === IAP & GEMS === 
	int GetCurrentCharGemPrice(int id){
		int price = 10;
		if (GD.s.skins [id].rarity == SkinRarity.common)
			price = 10;
		else if(GD.s.skins[id].rarity == SkinRarity.uncommon)
			price = 30;
		else if(GD.s.skins[id].rarity == SkinRarity.rare)
			price = 50;
		return price;
	}

	public void OnButtonGreenPurchasePressed(){
		if (curCategory == Categories.Main) {
			if (USER.s.GEMS >= GetCurrentCharGemPrice (actualCharInScreen)) {
				USER.s.GEMS -= GetCurrentCharGemPrice (actualCharInScreen);
				USER.s.SaveUserGems ();
				OnGemsPurchaseComplete ();
				DisplayGemsValues ();
			}
			else
				OpenGemStore ();
		}

		else if (curCategory == Categories.Promo1) {
			//CompleteProject.Purchaser.instance.BuyPack ((MusicStyle)USER.s.CUR_SPECIAL_OFFER);
		}


//		if (curCategory == Categories.Main) { // LÓGICA DE GEMS
//			if ((USER.s.GEMS >= 30 &&  (GD.s.skins[actualCharInScreen].isBand ||GD.s.skins[actualCharInScreen].isClothChanger)) ||
//				USER.s.GEMS >= 10 ) // PREÇO AQUI
//			{
//
//			}
//			
//		} else if (curCategory == Categories.Promo1) { // COMPRAS DE $ 3.99
//                                                       //
//            CompleteProject.Purchaser.instance.BuyPack((MusicStyle)USER.s.CUR_SPECIAL_OFFER);
//        }
	}

    public void OpenGemStore()
    {
        GemsStore.SetActive(true);
		DisplayGemsValues ();
    }

    public void CloseGemStore()
    {
        GemsStore.SetActive(false);
    }

    bool purchaseFromGems = false;
	public void OnGemsPurchaseComplete(){
		ActivatePlayAndBackButtonsAgain (); // tutorial logic

		if(curCategory == Categories.Main)
			StartCoroutine (GiveRewardFromGemsPurchase ());
		else if( curCategory == Categories.Promo1)
			StartCoroutine (GiveRewardSpecialOffer(myPromos[USER.s.CUR_SPECIAL_OFFER], GD.s.GetStyleName((MusicStyle)USER.s.CUR_SPECIAL_OFFER)));

		//
		//		equipCharacterNew(true);
		//		PlayerPrefs.SetInt (GD.s.skins[actualCharInScreen].skinName + "AlreadyBuyed", 1);
		//		alreadyBuyed [actualCharInScreen] = 1;
		//		nCharsBuyed++;
		//
		//		myLockedBg.SetActive (false);
		//		myBgLights.SetActive (true);
		//		equipButton.GetComponent<Button> ().interactable = true;
		//		buyButton.SetActive (false);
	}

	public Text gemsTextJukebox, gemsTextIAPStore;
	public void DisplayGemsValues(){
		if(gemsTextJukebox.isActiveAndEnabled) gemsTextJukebox.text = USER.s.GEMS.ToString();
		if(gemsTextIAPStore.isActiveAndEnabled) gemsTextIAPStore.text = USER.s.GEMS.ToString();
	}
	#endregion

	#region === SPECIAL OFFER ===
	DateTime dateNextPromo, dateNextSaturday;
	void Update(){
		if (globals.s.curGameScreen == GameScreen.Store && specialOfferButton.activeInHierarchy) {
			TimeSpan diff = dateNextPromo.Subtract (System.DateTime.Now);

			if (diff.TotalSeconds <= 0) {
//				Debug.Log ("o tempo passou e eu sofri calado");
			}

			if (diff.TotalSeconds > 0) {
//				Debug.Log ("o tempo passou e eu sofri calado");

				specialOfferTime.text = diff.Hours.ToString ("00") + ":" + diff.Minutes.ToString ("00") + ":" + diff.Seconds.ToString ("00") + "";
			}
		}

		//NEXT SATURDAY TIMER
		if (globals.s.curGameScreen == GameScreen.Store && actualCharInScreen == GD.s.N_SKINS && nextSaturdayTimer.gameObject.activeInHierarchy) {
			TimeSpan diff = dateNextSaturday.Subtract (System.DateTime.Now);
			if (diff.TotalSeconds <= 0) {
				nextSaturdayTimer.text = "Re-open the game!";
			}
			if (diff.TotalSeconds > 0) {
				if(diff.Days > 0)
					nextSaturdayTimer.text = diff.Days.ToString("0")+"d "+diff.Hours.ToString ("00") + ":" + diff.Minutes.ToString ("00") + ":" + diff.Seconds.ToString ("00") + "";
				else
					nextSaturdayTimer.text = diff.Hours.ToString ("00") + ":" + diff.Minutes.ToString ("00") + ":" + diff.Seconds.ToString ("00") + "";
			}
		}
	}

	void DefineNextSpecialPack(){
		int maxRand = GD.s.N_STYLES; //be carefull
		if (CountHowManyStylesPlayerHas () < GD.s.N_STYLES) {
			int styleToOffer = -1;
			do {
				styleToOffer = UnityEngine.Random.Range (0, maxRand);
			} while(CheckIfPlayerHasASkinOfThisStyle ((MusicStyle)styleToOffer) == true);

			Debug.Log ("[JK] STYLE FOUND!! : " + (MusicStyle) styleToOffer);

			USER.s.SetCurrentSpecialOffer ((MusicStyle) styleToOffer);

			float xPos = specialOfferButton.transform.localPosition.x;
			specialOfferButton.transform.localPosition = new Vector2 (xPos - 250, specialOfferButton.transform.localPosition.y);
			specialOfferButton.transform.DOLocalMoveX (xPos, 0.8f).SetEase (Ease.OutBounce);

		} else {
			Debug.Log ("[JK] NOTHING TO DO HERE!! FLY AWAY");
			specialOfferButton.SetActive (false);
			//deactivate special offer button
		}
	}

	public bool CheckIfPlayerHasASkinOfThisStyle(MusicStyle style){
		for(int id =  0 ; id < alreadyBuyed.Length; id++)  {
			if (GD.s.skins[id].musicStyle == style && alreadyBuyed[id] == 1)
				return true;
		}
		return false;
	}

	public int CountHowManyStylesPlayerHas(){
		int count = 0;
		for(int id =  0 ; id <  GD.s.N_STYLES; id++)  {
			if(CheckIfPlayerHasASkinOfThisStyle((MusicStyle)id)){
				count++;
			}
		}
//		Debug.Log (" CCCCCCCCC TOTAL PURCHASED STLYES: " + count + "  TOTAL STYLES: " + GD.s.N_STYLES); 
		return count;
	}


	public void OnButtonSpecialOfferPressed(){
		ChangeCategoryTo (Categories.Promo1);
	}

	void ChangeCategoryTo(Categories categTochange){
		if (curCategory != categTochange) {
			if (categTochange == Categories.Main) {
				promoCategories [(int)curCategory].SetActive (false);
				mainCategory.SetActive (true);
				ScrollSnap = mainCategory.GetComponentInChildren<ScrollSnapRect> ();

				StartCoroutine( ChangeCategoryAnimation (actualCharInScreen, mainCategory));

			} else {
				mainCategory.SetActive (false);
				promoCategories [(int)categTochange].SetActive (true);

				// try to define the current offer
				if (USER.s.CUR_SPECIAL_OFFER == -1 || USER.s.CheckIfSpecialOfferTimeHasElapsedAndUpdate() ) //tbd
					DefineNextSpecialPack ();

				Debug.Log ("JUMPING TO SPECIAL OFFER :" + ((MusicStyle)USER.s.CUR_SPECIAL_OFFER).ToString() + " ID " + USER.s.CUR_SPECIAL_OFFER.ToString());

				myPromos [USER.s.CUR_SPECIAL_OFFER].SetActive (true);
				ScrollSnap = promoCategories [(int)categTochange].GetComponentInChildren<ScrollSnapRect>();
				title.text = GD.s.GetStyleName((MusicStyle)USER.s.CUR_SPECIAL_OFFER) + " Pack";

				nextSaturdayTimer.gameObject.SetActive (false);

				StartCoroutine( ChangeCategoryAnimation (USER.s.CUR_SPECIAL_OFFER, promoCategories [(int)categTochange]));
			}

			curCategory = categTochange;
			lastChar = null;
		}
	}

	IEnumerator ChangeCategoryAnimation(int page, GameObject categoryList){
		float lastY = categoryList.transform.localPosition.y;
		categoryList.transform.localPosition = new Vector2 (categoryList.transform.localPosition.x, categoryList.transform.localPosition.y + 500);
		yield return new WaitForSeconds(0.01f);
		ScrollSnap.SetCurrentPage(page);

		categoryList.transform.DOLocalMoveY (lastY, 0.3f).SetEase (Ease.OutBounce);

		yield return new WaitForSeconds(0.1f);

		if (curCategory == Categories.Main) {
			equipButton.SetActive (true);
			buyGemsPrice.gameObject.SetActive (true);
			buyRealMoney.gameObject.SetActive (false);

			OnCharacterChangedNew (actualCharInScreen);

		} else { // promo special case
			frameVIP.SetActive (true);

			equipButton.SetActive (false);
			buyButton.SetActive (true);
			buyGemsPrice.gameObject.SetActive (false);
			buyRealMoney.gameObject.SetActive (true);

			sound_controller.s.ChangeMusicForStore ((MusicStyle)page);

			if (Application.systemLanguage == SystemLanguage.Portuguese) {
				buyRealMoney.text = "R$ 9.99";
			} else if (Application.systemLanguage == SystemLanguage.English) {
				buyRealMoney.text = "$ 265";
			} else {
				buyRealMoney.text = "$ 4.99";
			}
		}
//		ScrollSnap.SetCurrentPage(0);
	}

	public enum Categories{Main = -1, Promo1 = 0, Promo2 = 1, Promo3 = 2, Promo4 = 3}
	#endregion

	#region == Old ==

	public void watchedVideo()
	{
		USER.s.NOTES += 10;
		PlayerPrefs.SetInt("notes", USER.s.NOTES);
		actualCoins.text = USER.s.NOTES.ToString();
	}


	//	public void OnCharacterChanged(int type)
	//	{
	//		actualCharInScreen = type;
	//		if (type == 0)
	//		{
	//			title.text = "Electro";
	//			sound_controller.s.change_music(MusicStyle.Eletro);
	//			if(eletronicAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("eletronic");
	//			}
	//		}
	//		else if (type == 1)
	//		{
	//			title.text = "Rock";
	//			sound_controller.s.change_music(MusicStyle.Rock);
	//
	//			if (rockAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("rock");
	//			}
	//		}
	//		else if (type == 2)
	//		{
	//			title.text = "Classic Pop";
	//			sound_controller.s.change_music(MusicStyle.Pop);
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("pop");
	//			}
	//		}
	//		else if (type == 3)
	//		{
	//			title.text = "Modern Pop";
	//			sound_controller.s.change_music(MusicStyle.PopGaga);
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("popGaga");
	//			}
	//		}
	//
	//		else if (type == 4)
	//		{
	//			title.text = "Reggae";
	//			sound_controller.s.change_music(MusicStyle.Reggae);
	//
	//			if (reggaeAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("reggae");
	//			}
	//		}
	//
	//
	//		else if (type == (int)MusicStyle.Rap)
	//		{
	//			title.text = "Rap";
	//			sound_controller.s.change_music(MusicStyle.Rap);
	//
	//			if (reggaeAlreadyBuyed == 0)
	//			{
	//				equipButton.GetComponent<Animator>().Play("select off");
	//				equipButton.GetComponent<Button> ().interactable = false;
	//			}
	//			else
	//			{
	//				equipButton.GetComponent<Button> ().interactable = true;
	//				changeAnimationEquipButton("rap");
	//			}
	//		}
	//	}

	//    public void equipCharacter()
	//    {
	//		Debug.Log ("ACTUAL CHAR IN THE SCREEN: " + actualCharInScreen);
	//        if (actualCharInScreen == 0)
	//        {
	////            Debug.Log(" igona flee gonna be gonna flow");
	//            PlayerPrefs.SetString("ACTUAL_CHAR", "eletronic");
	//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "eletronic");
	//            changeActualCharSkin();
	//
	//            changeAnimationEquipButton("eletronic");
	//
	//            sound_controller.s.change_music(MusicStyle.Eletro);
	//
	//        }
	//        else if (actualCharInScreen == 1)
	//        {
	//            PlayerPrefs.SetString("ACTUAL_CHAR", "rock");
	//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rock");
	//            changeActualCharSkin();
	//
	//            changeAnimationEquipButton("rock");
	//
	//            sound_controller.s.change_music(MusicStyle.Rock);
	//
	//        }
	//        else if (actualCharInScreen == 2)
	//        {
	//            PlayerPrefs.SetString("ACTUAL_CHAR", "pop");
	//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "pop");
	//            changeActualCharSkin();
	//
	//            changeAnimationEquipButton("pop");
	//
	//            sound_controller.s.change_music(MusicStyle.Pop);
	//        }
	//		else if (actualCharInScreen == 3)
	//		{
	//			PlayerPrefs.SetString("ACTUAL_CHAR", "popGaga");
	//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "popGaga");
	//			changeActualCharSkin();
	//
	//			changeAnimationEquipButton("popGaga");
	//
	//			sound_controller.s.change_music(MusicStyle.PopGaga);
	//		}
	//		else if (actualCharInScreen == 4)
	//		{
	//			PlayerPrefs.SetString("ACTUAL_CHAR", "reggae");
	//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "reggae");
	//			changeActualCharSkin();
	//
	//			changeAnimationEquipButton("reggae");
	//
	//			sound_controller.s.change_music(MusicStyle.Reggae);
	//		}
	//
	//		else if (actualCharInScreen == (int)MusicStyle.Rap)
	//		{
	//			PlayerPrefs.SetString("ACTUAL_CHAR", "rap");
	//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rap");
	//			changeActualCharSkin();
	//
	//			changeAnimationEquipButton("rap");
	//
	//			sound_controller.s.change_music(MusicStyle.Rap);
	//		}
	//    }
	//
	//


	//	public void OnCharacterChangedOLD(int type)
	//	{
	//		actualCharInScreen = type;
	//		if (type == 0)
	//		{
	//			title.text = "Electro";
	//			if(eletronicAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = eletronicPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("eletronic");
	//			}
	//		}
	//		else if (type == 1)
	//		{
	//			title.text = "Rock";
	//
	//			if (rockAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = rockPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("rock");
	//			}
	//		}
	//		else if (type == 2)
	//		{
	//			title.text = "Classic Pop";
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("pop");
	//			}
	//		}
	//		else if (type == 3)
	//		{
	//			title.text = "Modern Pop";
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("popGaga");
	//			}
	//		}
	//
	//		else if (type == 4)
	//		{
	//			title.text = "Reggae";
	//
	//			if (reggaeAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("reggae");
	//			}
	//		}
	//	}

//
//	public void tryBuyCharacter(int musicStyle = -1)
//	{
//
//		if (musicStyle == -1)
//			musicStyle = actualCharInScreen;
//
//		Debug.Log (" TRYING TO BUY CHAR: " + musicStyle);
//
//		if(musicStyle == 0)
//		{
//			if (USER.s.NOTES >= eletronicPrice)
//			{
//				USER.s.NOTES -= eletronicPrice;
//				PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
//				eletronicAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 1)
//		{
//			if (USER.s.NOTES >= rockPrice)
//			{
//				USER.s.NOTES -= rockPrice;
//				PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
//				rockAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 2)
//		{
//			if (USER.s.NOTES >= popPrice)
//			{
//				USER.s.NOTES -= popPrice;
//				PlayerPrefs.SetInt("popAlreadyBuyed", 1);
//				popAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 3)
//		{
//			if (USER.s.NOTES >= popGagaPrice)
//			{
//				USER.s.NOTES -= popGagaPrice;
//				PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
//				popGagaAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 4)
//		{
//			if (USER.s.NOTES >= reggaePrice)
//			{
//				USER.s.NOTES -= reggaePrice;
//				PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
//				reggaeAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		actualCoins.text = USER.s.NOTES.ToString();
//		DisplayNotes ();
//
//	}
	//
	//	public bool CheckIfCharacterIsAlreadyPurchased(int musicStyle){
	//		if (musicStyle == 0) {
	//			if (eletronicAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//		} else if (musicStyle == 1) {
	//			if (rockAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//		} else if (musicStyle == 2) {
	//			if (popAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//		} else if (musicStyle == 3) {
	//			if (popGagaAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//		} else if (musicStyle == 4) {
	//			if (reggaeAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//
	//		} else if (musicStyle == (int)MusicStyle.Rap) {
	//			if (rapAlreadyBuyed == 1)
	//				return true;
	//			else
	//				return false;
	//		} else
	//			return true;
	//	}
	#endregion
}
