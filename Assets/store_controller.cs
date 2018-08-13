using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {

	#region === Vars ===
    public static store_controller s;

	[Header ("Gems")]
	public GameObject mainCategory;
	public GameObject[] promoCategories;
	Categories curCategory = Categories.Main;

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

    public int popPrice = 70;
    public int rockPrice = 50;
	public int popGagaPrice = 70;
	public int reggaePrice = 50;
    [HideInInspector]
    public int eletronicPrice = 30;

    public Text actualCoins;
    public Text buyPrice;

    public GameObject buyButton;
    public GameObject equipButton;

    int actualCharInScreen;
	MusicStyle actualStyle, lastSortedStyle = MusicStyle.Eletro;
	int lastSortedSkin = 0;

	float yStartCoinsPile = -1068, yEndCoinsPile = -575, yIncCoinsPile;

	#endregion
    // Use this for initialization

	#region === Init ===
    void Start () {
//		USER.s.NOTES = 98;
//		USER.s.NOTES = 101;
//		globals.s.NOTES_COLLECTED_JUKEBOX = 8 ;

		SetPileOfCoinsInitalPosition ();

//		StartCoroutine (InitCoinFallingAnimation (globals.s.NOTES_COLLECTED_JUKEBOX));

        s = this;
//		USER.s.AddNotes (101);

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

		for (int i = 0; i < alreadyBuyed.Length; i++) {
//			PlayerPrefs.SetInt(((MusicStyle)i).ToString()+"AlreadyBuyed", 0);
			int unlocked = 0;
			if (QA.s.ALL_UNLOCKED)
				unlocked = 1;
			alreadyBuyed[i] = PlayerPrefs.GetInt(GD.s.skins[i].skinName + "AlreadyBuyed", unlocked);
//			Debug.Log("ALREADY BUYED I: "+ i + " TRUE: " + alreadyBuyed[i]);
			if (alreadyBuyed [i] == 1)
				nCharsBuyed++;
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

	public void DefineGenericCharPosition(){
		Debug.Log ("[JUKE] DEFINE GENERIC CHAR POS!!!! : " + GD.s.N_SKINS); 
		genericChar.transform.SetSiblingIndex (GD.s.N_SKINS);
	}

	void DefineActualCharOnTheScreenNew(){
//		actualCharInScreen = (int)globals.s.ACTUAL_STYLE;
		actualCharInScreen = globals.s.ACTUAL_SKIN.id;
		actualStyle = globals.s.ACTUAL_STYLE;
	}

	public void OpenStore(){
		Invoke ("OpenStore2", 0.1f);
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
		changeAnimationEquipButtonNew(globals.s.ACTUAL_SKIN.id);
//		ScrollSnap.SetCurrentPage((MusicStyle)globals.s.ACTUAL_STYLE);
		ScrollSnap.SetCurrentPage(globals.s.ACTUAL_SKIN.id);
		title.text = globals.s.ACTUAL_SKIN.skinName;

		sound_controller.s.curJukeboxMusic = globals.s.ACTUAL_STYLE;

		if (FTUController.s.firstSongPurchased == 0) {
			myBackBt.gameObject.SetActive (false);
			equipButton.gameObject.SetActive (false);
		}


	}

	public void CloseStore(bool fromBackBt){
//		if(fromBackBt == true) sound_controller.s.change_music((MusicStyle)globals.s.ACTUAL_STYLE);
//				if(fromBackBt == true) sound_controller.s.SoltaOSomAeDJAndreMarques(globals.s.ACTUAL_STYLE);
		if(fromBackBt == true) sound_controller.s.SoltaOSomAeDJAndreMarques(globals.s.ACTUAL_STYLE);
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
	
	// Update is called once per frame
	void Update () {
	
	}


	#endregion

	#region === CATEGORIES ===

	public void OnButtonSpecialOfferPressed(){
		ChangeCategoryTo (Categories.Promo1);
	}
	public enum Categories{Main = -1, Promo1 = 0, Promo2 = 1, Promo3 = 2, Promo4 = 3}

	void ChangeCategoryTo(Categories categTochange){
		if (curCategory != categTochange) {
			if (curCategory == Categories.Main)
				mainCategory.SetActive (false);
			else
				promoCategories [(int)curCategory].SetActive (false);
			promoCategories [(int)categTochange].SetActive (true);
			curCategory = categTochange;
		}
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
		if (skinId < GD.s.N_SKINS ) {
			MusicStyle style = GD.s.skins [skinId].musicStyle;
			if (lastChar != null && dontDeactivateLast == false)
				StartCoroutine (DeactivateLastChar (lastChar));
			myChars [skinId].SetActive (true);
			lastChar = myChars [skinId];

//		Debug.Log ("[JUKEBOX] Character changed new: " + style.ToString());
//		Debug.Log ("[JUKEBOX] Character changed new: " + GD.s.skins[skinId].skinName);
			actualCharInScreen = skinId;
			actualStyle = style;

//		title.text = GD.s.GetStyleName (style);
			title.text = GD.s.skins [skinId].skinName;

			if (globals.s.JUKEBOX_SORT_ANIMATION == false && dontPlayMusic == false) 
//			sound_controller.s.change_music(style);
			sound_controller.s.ChangeMusicForStore (style);

			if (alreadyBuyed [skinId] == 0) {
//			equipButton.GetComponent<Animator> ().Play ("select off");
				equipButton.GetComponent<Button> ().interactable = false;
				myBgLights.SetActive (false);
				myLockedBg.SetActive (true);
				buyButton.SetActive (true);

				if (GD.s.skins [skinId].isBand || GD.s.skins [skinId].isClothChanger)
					myGemsPrice.text = "30";
				else
					myGemsPrice.text = "10";
			} else {
				equipButton.GetComponent<Button> ().interactable = true;
				myLockedBg.SetActive (false);
				myBgLights.SetActive (true);
				buyButton.SetActive (false);
			}
		} else { // GENERIC CHAR CASE 
			actualCharInScreen = skinId;
			if (RemoteMaster.s.maximumAllowedChars == GD.s.N_SKINS) {
				title.text = "More comming soon";
			}
			else 
				title.text = "More next Saturday";


			equipButton.GetComponent<Button> ().interactable = false;

			myLockedBg.SetActive (false);
			myBgLights.SetActive (true);
			buyButton.SetActive (false);
		}
//		changeAnimationEquipButtonNew(style);
	}

	IEnumerator DeactivateLastChar(GameObject character){
		yield return new WaitForSeconds (0.3f);
		character.SetActive (false);
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
			Debug.Log ("CCCCCCCC COINS: " + (initialCoins + i));
			if(nCoins < 20) yield return new WaitForSeconds (0.14f);
			else yield return new WaitForSeconds (0.07f);
			if(i % 2 == 0)
				sound_controller.s.PlaySfxUIJukeboxCoinFalling ();
		}

		myCoinsFalling.SetActive (false);
		globals.s.NOTES_COLLECTED_JUKEBOX = 0;

		yield return new WaitForSeconds (0.2f);

		if ( USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE)
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
			StartCoroutine (StartRoulleteAnimation ());

//			USER.s.NOTES -= globals.s.JUKEBOX_CURRENT_PRICE; 
			USER.s.SpendUserNotes (globals.s.JUKEBOX_CURRENT_PRICE, "BuyRandomSkin");
//			USER.s.SaveUserNotes ();

			FTUController.s.SetFirstSongPurchased ();

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
	public IEnumerator StartRoulleteAnimation(){

		//FALLING COINS ANIMATION
		myCoinsFullAnimator.SetTrigger("BuyButtonPressed");
//		myCoinsFullAnimator.ResetTrigger("BuyButtonPressed");
		yield return new WaitForSeconds (1.1f);

		globals.s.JUKEBOX_SORT_ANIMATION = true;
		int rand = Random.Range (10, 13);

		myBackBt.interactable = false;
		jukeboxBt.interactable = false;

		//change animation speed
		myU.GetComponent<Animator>().speed = 3f;
		mYTitle.GetComponent<Animator>().speed = 3f;
		myBgLights.GetComponent<Animator>().speed = 3f;

		int skinToGive;
		if (FTUController.s.firstSongPurchased == 0)
			skinToGive = 16; // mozart as first
		else skinToGive = SortCharToDrop ();

		Debug.Log ("[[[[ [JUKE] SORTED CHAR TO GIVE: " + GD.s.skins [skinToGive].skinName);

		int k = 0;
		do { // FAZER UM MINIMO 
			for (int i = 0; i < rand; i++) { //logica de não repetir
				
				ScrollSnap.NextScreen ();
				yield return new WaitForSeconds (0.1f);
				if( k % 5 == 0 ) sound_controller.s.PlaySfxUIJukeboxSortingCharacter();
			}
			rand = Random.Range (1, 3);
//			Debug.Log("STYLE FOUND: "+ (MusicStyle)actualStyle + " have " +CheckIfCharacterIsAlreadyPurchasedNew(skinId) );
		} while ( actualCharInScreen != skinToGive  || actualCharInScreen == lastSortedSkin); // TBD SE FOR O ULTIMO A DROPAR VAI DAR MERDA NO LAST SORTED SKIN
//		} while (alreadyBuyed [(int)actualStyle] == 0);
//		} while (CheckIfCharacterIsAlreadyPurchasedNew(actualCharInScreen) == true || actualCharInScreen == lastSortedSkin);

		globals.s.JUKEBOX_SORT_ANIMATION = false;
//		OnCharacterChangedNew (actualStyle);

		yield return new WaitForSeconds(0.1f);

		Debug.Log("STYLE FOUND 2222: id: "+ actualCharInScreen + " name: " + GD.s.skins[actualCharInScreen].skinName + " have " +CheckIfCharacterIsAlreadyPurchasedNew(actualCharInScreen));

		lastSortedStyle = actualStyle;
		lastSortedSkin = actualCharInScreen;

		StartCoroutine (GiveReward ());

//		hud_controller.si.GiftButtonClicked (actualStyle);
	}

	int SortCharToDrop(){
		List<Skin> commonChars = new List<Skin> ();
		List<Skin> uncommonChars = new List<Skin> ();
		List<Skin> rareChars = new List<Skin> ();
//		List<Skin> epicChars = new List<Skin> ();

		//mount possible char list
		for (int i = 0; i < GD.s.N_SKINS; i++) {
//			if(GD.s.sk
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
		int maxRand = 0, commonChance = 0, uncommonChance = 0 , rareChance = 0, epicChance = 0;
		if (commonChars.Count > 0) { maxRand += GD.s.GD_DROP_CHANCE_COMMON; commonChance = GD.s.GD_DROP_CHANCE_COMMON;	}
		if (uncommonChars.Count > 0) { maxRand += GD.s.GD_DROP_CHANCE_UNCOMMON; uncommonChance = GD.s.GD_DROP_CHANCE_COMMON;}
		if (rareChars.Count > 0) {maxRand += GD.s.GD_DROP_CHANCE_RARE; rareChance = GD.s.GD_DROP_CHANCE_RARE; }
			
		//start the sort
		int rand = Random.Range (0, maxRand);

		if (commonChars.Count > 0 && rand < commonChance) {
			rand = Random.Range (0, commonChars.Count);
			return commonChars.ToArray () [rand].id;
		} else if (uncommonChars.Count > 0 && rand < commonChance + uncommonChance) {
			rand = Random.Range (0, uncommonChars.Count);
			return uncommonChars.ToArray () [rand].id;
		} else if (rareChars.Count > 0 && rand < commonChance + uncommonChance + rareChance) {
			rand = Random.Range (0, rareChars.Count);
			return rareChars.ToArray () [rand].id;
		} else {
			return -1;
			Debug.LogError (" **** OUT OF CHARS ! THAT SHOULD NEVER HAPPEN *****");
		}
	}


	IEnumerator GiveReward() { //After the sort, show the Reward Screen

		OnCharacterChangedNew (actualCharInScreen, false, true); // TBDCHAR
		globals.s.curGameScreen = GameScreen.RewardCharacter;

		GameObject curChar = null;
//		foreach (GameObject character in myChars) {
//			if (character.name == actualStyle.ToString ()) {
		for(int k=0; k < myChars.Length; k++) {
			if (k == actualCharInScreen) {
				curChar = myChars[k];
				break;
			}
		}

		if (curChar.GetComponent<Animator> () != null)
			myRewardScreen.SetMyRewardChar (curChar.GetComponent<Animator> ().runtimeAnimatorController, GD.s.skins [actualCharInScreen].skinName);
		else {
			Debug.Log ("LETS KIDNAP SOME CHILDERN");
			myRewardScreen.SetMyRewardChar (curChar.GetComponentInChildren<Animator> ().runtimeAnimatorController, GD.s.skins [actualCharInScreen].skinName);
		}

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


	public void WatchedVideoForResort(){
		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.gameObject.SetActive (false);

		StartCoroutine (StartRoulleteAnimation ());
	}

	// Collect button pressed
	public void OnButtonRewardPressed(){
		ActivatePlayAndBackButtonsAgain ();
//		jukeboxBt.interactable = true; //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
//		UpdateUserNotes(); //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
		globals.s.MENU_OPEN = false;

		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.gameObject.SetActive (false);

		//give the reward for real
		equipCharacterNew(true);
//		PlayerPrefs.SetInt (actualStyle.ToString () + "AlreadyBuyed", 1);
		PlayerPrefs.SetInt (GD.s.skins[actualCharInScreen].skinName + "AlreadyBuyed", 1);
		alreadyBuyed [actualCharInScreen] = 1;
		nCharsBuyed++;

		myBackBt.interactable = true;

		myCoinsFullAnimator.enabled = false;
		SetPileOfCoinsInitalPosition ();

		myLockedBg.SetActive (false);
		myBgLights.SetActive (true);
		equipButton.GetComponent<Button> ().interactable = true;

		// FALL THE EXTRA COINS
		if (USER.s.NOTES > 0) {
//			globals.s.NOTES_COLLECTED_JUKEBOX = USER.s.NOTES;
		
			StartCoroutine (InitCoinFallingAnimation (USER.s.NOTES));
		}
	}

	void ActivatePlayAndBackButtonsAgain(){
		if (FTUController.s.firstSongPurchased == 1) {
			myBackBt.gameObject.SetActive (true);
			equipButton.gameObject.SetActive (true);
		}
	}

	public void OnGemsPurchaseComplete(){
		ActivatePlayAndBackButtonsAgain ();

		equipCharacterNew(true);
		PlayerPrefs.SetInt (GD.s.skins[actualCharInScreen].skinName + "AlreadyBuyed", 1);
		alreadyBuyed [actualCharInScreen] = 1;
		nCharsBuyed++;

		myLockedBg.SetActive (false);
		myBgLights.SetActive (true);
		equipButton.GetComponent<Button> ().interactable = true;
		buyButton.SetActive (false);


	}

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
