using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Advertisements;
using Ads;

public enum RewardedVideoType{ Revive, ResortChar, RespinDisk, SpinDisk, DoubleReward}

public class hud_controller : MonoBehaviour {

	#region === Variables Declaration ===
	public static hud_controller si;

	RewardedVideoType curVideoType = RewardedVideoType.Revive;

	public GameObject displayingVideoMessage;

	public GameObject handTapToPlay;

	public GameObject jukeboxBtMainMenu;


    [HideInInspector]
    public bool HUD_BUTTON_CLICKED = false;
	public GameObject screenCustomMessage;

	public GameObject store_label;

	[Space (5)]
	[Header ("Restart")]
	public GameObject restartScreen;
	public GameObject restartBlackBG;
	public GameObject restartMotivationalPhrase;
	public GameObject restartDisk;
	public GameObject restartDiskGroup;


	[Space (5)]
	[Header ("Career Over")]
    public GameObject game_over_text;
	public GameObject game_over_score;
	public GameObject game_over_best;

	[Space (5)]
	[Header ("New Highscore")]
	public NewHighscoreScreen myNewHighscoreScreen;

	[Space (5)]
	[Header ("GAMEPLAY")]
	public GameObject gameplah_hud_label;
    public GameObject pauseButton;
    public GameObject floor;
    public GameObject best;
    public GameObject notes;

	[Space (5)]
	[Header ("MAIN MENU")]
    public GameObject intro_label;

	public GameObject header;
	public GameObject game_title;

	public GameObject bottomLabel;
    public GameObject activate_pw_bt;

	[Space (5)]
	[Header ("REVIVE")]
    public GameObject revive;
    public GameObject video;
    public GameObject ready;
    //public GameObject v_pw_on;
	public GameObject start_game_bt;

	[Space (10)]
	[Header ("SPIN DISK")]
	public PwWheelMaster roda_a_roda;
	public GameObject pw_time_bar;
	public GameObject pw_time_left_title_on, roullete_time_left, gift_time_left;

    [Space(10)]
    [Header("PAUSE")]
    public GameObject pause;

    [Space(10)]
    public Text pw_Text_Header;
    public Text PW_time_text;

	public bool giftAnimationEnded = false;


    string PW_date, roullete_date, gift_date;
    DateTime tempDate;
    DateTime tempDateRoulette;
    DateTime tempDateGift;
    public bool CAN_ROTATE_ROULETTE = true;
    public bool CAN_GET_GIFT = true;
    DateTime tempcurDate;

    int temp_cur_floor;
    int temp_best_floor;

    public bool runningRoullete;

    bool flagVideoPower = false, flagVideoRevive = false, flagVideoCoins = false;

	public GameObject giftBt;
	public GameObject giftAnimation;
	public GameObject giftChar;

	public PW_Collect firstPw;

    // Use this for initialization
    void Awake()
    {
        flagVideoPower = false; flagVideoRevive = false; flagVideoCoins = false;
        si = this;
		start_game_bt.SetActive (true);
    }

	#endregion

	#region ======= INIT ========

	public void ActivateFirstPw(){
		if (USER.s.FIRST_PW_CREATED == 0) {
//			firstPw.gameObject.SetActive (true);
//			firstPw.pw_type = 2;
//			firstPw.init_my_icon ();
			USER.s.FIRST_PW_CREATED = 1;
			PlayerPrefs.SetInt("first_pw_created", 1);
			//PlayerPrefs.SetInt(
		}
	}

    void Start () {
		var studioSystem = FMODUnity.RuntimeManager.StudioSystem;
		FMOD.Studio.CPU_USAGE cpuUsage;
		studioSystem.getCPUUsage(out cpuUsage);

		var lowlevelSystem = FMODUnity.RuntimeManager.LowlevelSystem;
		uint version;
		lowlevelSystem.getVersion(out version);

		FMODUnity.RuntimeManager.PlayOneShot ("event:/JumpTest");
//		lowlevelSystem.p
		#if UNITY_ANDROID || UNITY_IOS
		Advertisement.Initialize ("1194074");
		#endif
//		game_title_y = game_title.transform.position.y;
		game_title_y = header.transform.localPosition.y;
		//Invoke ("GiftButtonClicked", 1f);

		SpinDiskTimerLogic ();
//
//		if (USER.s.GIFT_INTRODUCED == 1 ||  QA.s.OLD_PLAYER) {
////			giftBt.SetActive (true);
//			if (gift_date != "") {
//				tempDateGift = Convert.ToDateTime (gift_date);
//				PlayerPrefs.SetString ("GiftDate2ChangeState", tempDateGift.ToString ());
//				int canGet = PlayerPrefs.GetInt ("CanGetGift", 1);
//				if (canGet == 1) {
//					CAN_GET_GIFT = true;
//					giftBt.GetComponent<GiftButton> ().SetGetNowState ();
//				} else {
//					CAN_GET_GIFT = false;
//					giftBt.GetComponent<GiftButton> ().SetCountownState ();
//				}
//			} else {
//				CAN_GET_GIFT = true;
//				giftBt.GetComponent<GiftButton> ().SetGetNowState ();
//			}
//		} else {
//			giftBt.SetActive (false);
//		}

    }

	void SpinDiskTimerLogic(){

		display_best(USER.s.BEST_SCORE);
		display_notes(USER.s.NOTES);

		//PlayerPrefs.DeleteAll();
		PW_date = PlayerPrefs.GetString("PWDate2ChangeState");
		roullete_date = PlayerPrefs.GetString("RouletteDate2ChangeState", "");
		gift_date = PlayerPrefs.GetString("GiftDate2ChangeState");


		if (USER.s.NEWBIE_PLAYER == 0 || QA.s.OLD_PLAYER) {

			//			activate_pw_bt.GetComponent<activate_pw_button> ().HandTutLogic ();
			//SETTING PW STATE
			int temp_state = PlayerPrefs.GetInt ("PWState", 0);
			if (temp_state == 1) {
				globals.s.PW_ACTIVE = true;
				//				activate_pw_bt.GetComponent<Button> ().interactable = true;
				//				activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
			} else {
				globals.s.PW_ACTIVE = false;
				//				activate_pw_bt.GetComponent<Button> ().interactable = false;

				//				activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState ();
			}
		} else {
			//			activate_pw_bt.SetActive (false);
			//			jukeboxBtMainMenu.SetActive (false);
			//			handTapToPlay.SetActive (true);

		}

		//Debug.Log(PW_date);

		if (FTUController.s.firstSongPurchased == 1) {
			if (PW_date != "")
				tempDate = Convert.ToDateTime (PW_date);

			if (roullete_date != "") {
				tempDateRoulette = Convert.ToDateTime (roullete_date);
				PlayerPrefs.SetString ("RouletteDate2ChangeState", tempDateRoulette.ToString ());
				int canRotate = PlayerPrefs.GetInt ("CanRotate", 1);
				if (canRotate == 1) { // CAN ROTATE!!!! SET INTEACTABLE THE PW BUTTON
					CAN_ROTATE_ROULETTE = true;
					activate_pw_bt.SetActive (true);
					activate_pw_bt.GetComponent<Button> ().interactable = true;
					Debug.Log("HUD: CAN ROTATE");
				} else {
					CAN_ROTATE_ROULETTE = false;
					activate_pw_bt.SetActive (true);
					activate_pw_bt.GetComponent<Button> ().interactable = false;

					Debug.Log ("HUD: NO ROTATE");

				}
			} else {
				CAN_ROTATE_ROULETTE = true;
				Debug.Log ("HUD: vazio can rotate init");

				activate_pw_bt.SetActive (true);
				activate_pw_bt.GetComponent<Button> ().interactable = true;
			}
		}
	}

	public void start_game_coroutine(){
		StartCoroutine (start_game ());
	}

	public IEnumerator start_game()
    {
		handTapToPlay.SetActive (false);
        //floor.SetActive(true);
        //best.SetActive(true);
        //notes.SetActive(true);
		if (globals.s.AT_STORE == false) {
//			pw_info.transform.DOLocalMoveY (pw_info.transform.localPosition.y + 700
//				, 0.5f).SetEase (Ease.OutQuart);

			
			//yield return new WaitForSeconds (0.15f);
//			pw_info.transform.DOLocalMoveY (-GetComponent <RectTransform>().rect.height/2 - pw_info.GetComponent <RectTransform>().rect.height/2
//				, 0.5f).SetEase (Ease.OutQuad).OnComplete (store_entrance);

//			game_title.transform.DOLocalMoveY (game_title.transform.localPosition.y + 700
//					, 0.5f).SetEase (Ease.OutQuart);
//			yield return new WaitForSeconds (0.15f);

			header.transform.DOLocalMoveY (header.transform.localPosition.y + 200
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.2f);

			SpinDiskTimerLogic ();

			hud_entrance ();

		} else {
			
			store_label.transform.DOLocalMoveY(store_label.GetComponent <RectTransform> ().rect.height
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.14f);

			header.transform.DOLocalMoveY (header.transform.localPosition.y + 500
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.2f);

			hud_entrance ();
		}

        //Destroy(intro_label);
		//store_label.SetActive (false);

        game_controller.s.game_running();
		//return true;
    }

	void hud_entrance(){
		intro_label.SetActive (false);
		store_label.SetActive (false);
        pauseButton.SetActive(true);
        gameplah_hud_label.SetActive (true);
		float y_start = gameplah_hud_label.transform.localPosition.y;
		gameplah_hud_label.transform.localPosition = new Vector2 (gameplah_hud_label.transform.localPosition.x, 
			gameplah_hud_label.transform.localPosition.y + 250);
		gameplah_hud_label.transform.DOLocalMoveY (y_start, 0.3f).SetEase (Ease.OutQuad).OnComplete (show_floor_intro);
	}

	void show_floor_intro(){
		stage_intro.s.StartEntering (1);
	}
		
	public void display_best(int value)
	{
		best.GetComponent<Text>().text = "BEST " + value;
	}

	public void display_notes(int n) {
		notes.GetComponent<Text>().text = (n).ToString();
	}

	#endregion

	#region ==== RESTART ======

	public void OnReplayButtonPressed(){
		//		SceneManager.LoadScene("Gameplay 1");
		game_controller.s.RewindEffect();
	}

	float posX = -2323;

	public void DisplayRestartLoading(){
//		restartBlackBG.SetActive (true);
		restartScreen.SetActive (true);
		restartBlackBG.GetComponent<Image> ().color = new Color (0, 0, 0, 0);
//		restartBlackBG.GetComponent<Image> ().DOFade (1, 0.25f).OnComplete(RestartSpinDiskEnter);
		restartBlackBG.GetComponent<Image> ().DOFade (1, 0.25f);

//		restartMotivationalPhrase.GetComponent<Image> ().color = new Color (1, 1, 1, 0);
//		restartMotivationalPhrase.GetComponent<Image> ().DOFade (1, 0.35f);

		if(posX == -2323) posX = restartDiskGroup.transform.position.x;
		restartDiskGroup.transform.position = new Vector2 (posX - 1100, restartDiskGroup.transform.position.y);
		
		RestartSpinDiskEnter ();

		restartScreen.GetComponent<RestartLoadingScreen> ().InitMe ();
	}

	void RestartSpinDiskEnter(){

		restartDiskGroup.transform.DOMoveX (posX, 0.4f).SetEase(Ease.OutCubic);

//		float tempo = UnityEngine.Random.Range (2f, 2.6f);
//		float angle = UnityEngine.Random.Range  (-1, -360);
//		float force = UnityEngine.Random.Range (240,250);
//		angle = angle * (force);
//
//		restartDisk.transform.DORotate (new Vector3 (0, 0, angle), 4f, RotateMode.WorldAxisAdd);
	}

	public void HideRestartLoading(){
		//		restartBlackBG.SetActive (true);
		Debug.Log ("gogogo disk");

		restartBlackBG.GetComponent<Image> ().DOFade (0, 0.35f);

		restartDiskGroup.transform.DOMoveX (posX + 1100f, 0.3f).SetEase(Ease.InCubic);
//		Debug.Log ("gogogo disk2");

//		restartMotivationalPhrase.GetComponent<Image> ().DOFade (0, 0.35f);
	}

	public void MainMenuEntranceForRestart(){

		restartBlackBG.GetComponent<Image> ().DOFade (0, 0.35f);

//		restartDiskGroup.transform.DOMoveX (posX + 1100f, 0.3f).SetEase(Ease.InCubic);
//		Debug.Log ("gogogo diskss !! posx: " + posX + " TARGET: " + (posX + 1100f));
//		restartDiskGroup.transform.DOMoveX ((posX + 1100f), 0.3f).SetEase(Ease.InCubic);
		restartDiskGroup.transform.DOLocalMoveX ((1000f), 0.3f);

		globals.s.curGameScreen = GameScreen.MainMenu;

//		restartScreen.SetActive (false);
		intro_label.SetActive (true);

		header.transform.localPosition = new Vector2 (header.transform.localPosition.x, 
//			header.transform.localPosition.y + 450);
			game_title_y + 450);
		header.transform.DOLocalMoveY (game_title_y, 0.5f).SetEase (Ease.OutQuad); // NAO TA CORRETO

		float y_start = bottomLabel.transform.localPosition.y; // NAO TA CORRETO
		bottomLabel.transform.localPosition = new Vector2 (bottomLabel.transform.localPosition.x, 
			bottomLabel.transform.localPosition.y - 350);
//		bottomLabel.transform.DOLocalMoveY (y_start, 0.3f).SetEase (Ease.OutQuad).OnComplete(() => restartScreen.SetActive(false));
		bottomLabel.transform.DOLocalMoveY (y_start, 0.3f).SetEase (Ease.OutQuad);

		handTapToPlay.SetActive (true);		Invoke ("DeactivateRestartScreen", 0.3f);
	}

	void DeactivateRestartScreen(){
		restartScreen.SetActive (false);
	}
		
	#endregion

	#region ==== Update ====
	// Update is called once per frame
	void Update () {
        //GAME OVER GAME CASE
        //if(Input.GetMouseButtonDown(0))
        //   Debug.Log("ueeeeeeeeeeeeeeeeee epaaaaaaaaaaaaaaaaaaa epa, veja la como fala sua " + globals.s.CAN_RESTART);
        // Debug.Log(globals.s.PW_ACTIVE);
        //        if (globals.s.CAN_RESTART && Input.GetMouseButtonDown(0))
        //        {
        //            //Application.LoadLevel("Gameplay");
        //            //Application.LoadLevel()
        //            
        //            SceneManager.LoadScene("Gameplay 1");
        //        }
        if (USER.s.NEWBIE_PLAYER == 0 && globals.s.FIRST_GAME == false && globals.s.GAME_STARTED == false) {
			if (USER.s.NEWBIE_PLAYER == 0) {
				show_pw_time ();
				show_roullete_time ();
			}
			if ( USER.s.GIFT_INTRODUCED == 1)
				show_gift_time();
		}


		if (QA.s.PC_MODE && Input.GetKey ("space") && globals.s.curGameScreen == GameScreen.LevelEnd) {
			OnReplayButtonPressed ();
		}


		if (globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
		{
			if (Input.GetMouseButtonUp(0) && HUD_BUTTON_CLICKED == false)
			{
				//globals.s.GAME_STARTED = true;
				// start_game();

			}
			else if(Input.GetMouseButtonDown(0) && HUD_BUTTON_CLICKED == true)
			{
				HUD_BUTTON_CLICKED = false;
			}
		}
		if (globals.s.GIFT_ANIMATION == true && giftAnimationEnded == true && Input.GetMouseButtonDown (0)) {
			giftChar.SetActive (false);
			giftAnimation.SetActive (false);
			globals.s.GIFT_ANIMATION = false;
		}
	}

	public void update_floor(int n)
	{
		if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" NEW FLOOR!!!!!! ");
		//GetComponentInChildren<TextMesh>().text =  "Floor " + (n+1).ToString();
		floor.GetComponent<Text>().text = "Floor " + (n + 1).ToString("00");
	}

	#endregion

	#region === Custom Messages ===
	public void DisplayNewCharactersAvailable(){
		StartCoroutine (DisplayNewCharactersMessageNow ());
	}

	IEnumerator DisplayNewCharactersMessageNow(){
		yield return new WaitForSeconds (0.35f);
		screenCustomMessage.SetActive (true);
	}

	public void OnButtonNiceHideNewCharacatersMessage(){
		screenCustomMessage.SetActive (false);
	}

	#endregion

	#region ==== Store ====
	float pw_info_y, game_title_y;

	public void OnJukeboxBtPressed(){
		if (globals.s.AT_STORE == false && globals.s.MENU_OPEN == false) {
			Debug.Log (" MENU HEIGHT: " + game_title.GetComponent <RectTransform> ().rect.height + " POS: " + game_title.transform.position.y + " LOCAL Y: " + game_title.transform.localPosition.y);
			globals.s.AT_STORE = true;
			//			pw_info_y = pw_info.transform.position.y;
			//			pw_info.transform.DOLocalMoveY (-GetComponent <RectTransform>().rect.height/2 - pw_info.GetComponent <RectTransform>().rect.height/2
			//				, 0.5f).SetEase (Ease.OutQuad);
			Invoke ("store_entrance", 0.2f);

//			game_title_y = game_title.transform.localPosition.y;
			header.transform.DOLocalMoveY (GetComponent <RectTransform> ().rect.height / 2 + game_title.GetComponent <RectTransform> ().rect.height / 2
				//game_title.transform.DOLocalMoveY (GetComponent <Rect>().height - game_title.transform.localPosition.y + 500
				, 0.5f).SetEase (Ease.OutQuad);
		}
	}
	float storeY = 99999;

	void store_entrance(){
		globals.s.previousGameScreen = globals.s.curGameScreen;
		globals.s.curGameScreen = GameScreen.Store;

		store_label.SetActive (true);
		if(storeY == 99999) storeY = store_label.transform.localPosition.y;
		store_controller.s.OpenStore ();

		store_label.transform.localPosition = new Vector3 (0, storeY - store_label.GetComponent <RectTransform> ().rect.height , store_label.transform.localPosition.z);
		store_label.transform.DOLocalMoveY(storeY
			, 0.5f).SetEase (Ease.OutQuad);
		//		store_controller.s.changeAnimationEquipButton("eletronic");
		//        store_controller.s.changeAnimationEquipButtonNew((;
	}


	public void OnJukeboxCloseBtPressed(bool fromBackBt = true){
		if (globals.s.AT_STORE == true && globals.s.MENU_OPEN == false) { // close store
			if (store_controller.s.curCategory == store_controller.Categories.Main) {
				globals.s.AT_STORE = false;
				store_label.transform.DOLocalMoveY (-200 - store_label.GetComponent <RectTransform> ().rect.height
				, 0.5f).SetEase (Ease.OutQuad);
//			Invoke ("store_closing", 0.35f);
				StartCoroutine (StoreCloseForReal (fromBackBt));

				store_controller.s.CloseStore (fromBackBt);
			}
			else {
				store_controller.s.CloseStore (fromBackBt);
			}
		} 
	}

	IEnumerator StoreCloseForReal(bool fromBackBt = true){
		
		if (globals.s.previousGameScreen == GameScreen.LevelEnd && fromBackBt == false) {
//			hide_game_over ();
//			game_over_text.SetActive(false);
			yield return new WaitForSeconds (0.35f);

			yield return new WaitForSeconds (0.3f);

//			globals.s.curGameScreen = globals.s.previousGameScreen;

			game_controller.s.RewindEffect ();

		} else {

			if(globals.s.previousGameScreen == GameScreen.LevelEnd){
				GameOverController.s.UpdateJukeboxInformation ();
			}

			yield return new WaitForSeconds (0.35f);

			globals.s.curGameScreen = globals.s.previousGameScreen;

			if(globals.s.curGameScreen == GameScreen.MainMenu) 
				header.transform.DOLocalMoveY (game_title_y, 0.5f).SetEase (Ease.OutQuad);
			store_label.SetActive (false);

		}

//		pw_info.transform.DOMoveY (pw_info_y, 0.5f).SetEase (Ease.OutQuad);

	}


	#endregion

    #region === GAME OVER ===
	IEnumerator ManualReset(){
		yield return new WaitForSeconds (100);

//		if (globals.s.GAME_OVER == 1) {
//			hud_controller.si.OnReplayButtonPressed ();
//		}
	}

	public void show_game_over(int currentFloor, bool fromRevive = false)
    {
		Debug.Log ("[HUD] CURRENT: " + currentFloor + " HIGHS: " + USER.s.BEST_SCORE);
		if (currentFloor > USER.s.BEST_SCORE) {
			ShowNewHighscoreScreen ();

		} else {

			if (fromRevive == false)
				Invoke ("appear_game_over", 0.6f);
			else
				Invoke ("appear_game_over", 0.1f);
		}

		// TBD: PASSAR O CODIGO DE SALVAR PARA A CLASSE USER
		int last_score = USER.s.LAST_SCORE;
		int bestFloor = get_and_set_best_score(currentFloor);
		int dayFloor = get_and_set_day_score(currentFloor);

		USER.s.SaveLastFloor (currentFloor);

		temp_cur_floor = currentFloor;
		temp_best_floor = bestFloor;

		if (QA.s.PC_MODE == true) {
			StartCoroutine (ManualReset ());
		}

    }

	void ShowNewHighscoreScreen(){
		myNewHighscoreScreen.gameObject.SetActive (true);
		myNewHighscoreScreen.Init ();
		sound_controller.s.PlaySfxUINewHighscore ();
	}

	public void appear_game_over()
    {
		Debug.Log (" APPEAR GAME OVER!!!! ");

        if(globals.s.GAME_OVER == 1)
        {
            //if (game_over_text.GetComponent<Text>().IsActive()) print(" IS GAME OVER ACTIVE ");
//			game_over_text.GetComponent<Text>().text = "GAME OVER\n\nSCORE: " + temp_cur_floor + "\n BEST: " + temp_best_floor;
//			game_over_score.GetComponent<Text>().text =  temp_cur_floor.ToString () ;
//			game_over_best.GetComponent<Text>().text =  temp_best_floor.ToString ();
			game_over_text.SetActive(true);
			GameOverController.s.Init (temp_cur_floor, temp_best_floor);
        }
    }

    public void hide_game_over()
    {
        globals.s.GAME_OVER = 0;
        game_over_text.SetActive(false);
    }

    #endregion

    #region === PLAYER PREFS ===
    int get_and_set_best_score(int cur_floor)
    {
        int cur_best = PlayerPrefs.GetInt("best", 0);

        if (cur_floor > cur_best)
        {
            PlayerPrefs.SetInt("best", cur_floor);
            cur_best = cur_floor;
			USER.s.BEST_SCORE = cur_floor;
        }

        return cur_best;
    }

    int get_and_set_notes(int n) {
        int cur_notes = PlayerPrefs.GetInt("notes", 0);
        return cur_notes;
    }


    int get_and_set_day_score(int cur_floor)
    {
        int day_best = PlayerPrefs.GetInt("day_best", 0);
        bool day_gone = day_passed();

        if(day_gone == false)
        {
            if (cur_floor > day_best)
            {
                PlayerPrefs.SetInt("day_best", cur_floor);
				USER.s.DAY_SCORE = cur_floor;
                day_best = cur_floor;
            }
        }
        else
        {
            day_best = 0;
            PlayerPrefs.SetInt("day_best", 0);
        }

        return day_best;
    }

    #endregion

    bool day_passed()
    {
        DateTime newDate = System.DateTime.Now;
        string stringDate = PlayerPrefs.GetString("PlayDate");
        DateTime oldDate;

        if (stringDate == "")
        {
            oldDate = newDate;
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
        }
        else
        {
            oldDate = Convert.ToDateTime(stringDate);
        }

       // Debug.Log("LastDay: " + oldDate);
       // Debug.Log("CurrDay: " + newDate);

        TimeSpan difference = newDate.Subtract(oldDate);

       // Debug.Log("Dif Houras: " + difference);
        if (difference.Days >= 1)
        {
           // Debug.Log("Day passed");
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
            return true;
        }

        return false;
    }

    #region === LIFE SYSTEM ===

	public void show_pw_time() // atualiza o tempo do power ups
    {
        tempcurDate = System.DateTime.Now;
        
        //NO DATE CASE, TRIGGER 5 MINUTES
        if (PW_date == "")
        {
            Debug.Log("no date");
            PW_time_set_new_date_and_state(false);
        }
        else
        {
            TimeSpan diff = tempDate.Subtract(tempcurDate);
            //Debug.Log(diff + "  TimeDif " + globals.s.PW_ACTIVE);
            if (diff.Minutes > GD.s.GD_WITHOUT_PW_TIME && globals.s.PW_ACTIVE == false)
            {
                Debug.Log("new date");

                PW_time_set_new_date_and_state(true);
            }
            else
            {
                if (tempDate < tempcurDate)
                {
                    Debug.Log("new date");

                    PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
                }
            }
        }


        TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);
        if( globals.s.PW_ACTIVE == true)
        {

			float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / GD.s.GD_WITH_PW_TIME;
//            pw_Text_Header.text = "Power Ups On";
            pw_time_left_title_on.SetActive(true);
			pw_time_left_title_on.GetComponent<Text>().text =  difference.Minutes + ":" + difference.Seconds + "";
            //activate_pw_bt.SetActive(false);
//			if(pw_time_bar.GetComponent<Animator>()) pw_time_bar.GetComponent<Animator>().Play("PowerUpsCharginBarGreen");
//            pw_time_bar.GetComponent<Image> ().fillAmount =  fill;

			//activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
        }
        else
        {
            float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / GD.s.GD_WITHOUT_PW_TIME;
//            pw_Text_Header.text = "Power Up Status";
//            pw_time_left_title_on.SetActive(false);
           // pw_time_bar.GetComponent<Animator>().Play("PowerUpsChargingBarAnimation");
           // pw_time_bar.GetComponent<Image> ().fillAmount =  1f - fill;

//			activate_pw_bt.SetActive(true);
//			activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState();
//			PW_time_text.text = difference.Minutes + ":" + difference.Seconds + "";

        }  
    }

	// mostra o tempo pros power ups
    public void PW_time_set_new_date_and_state(bool PW_active_state)
    {
        if(PW_active_state == true) // 
        {
			Debug.Log ("!!!!!!! PW ACTIVE STATE = TRUE! COUNTOWN");
            //v_pw_on.SetActive(true);
//			pw_time_left_title_on.SetActive (true);
//            pw_time_bar.GetComponent<Animator>().Play("PowerUpsCharginBarGreen");
//
//            pw_Text_Header.text = "Power Ups On";
            //pw_text.SetActive (true);
            globals.s.PW_ACTIVE = true;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITH_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);
            PW_date = tempDate.ToString();
            //activate_pw_bt.SetActive(false);

            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 1);

			if(FTUController.s.firstSongPurchased == 1) activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
        }
		else if(FTUController.s.firstSongPurchased == 1)
        {
			Debug.Log ("!!!!!!! PW ACTIVE STATE = FALSE! SPIN NOW!");

            //v_pw_on.SetActive(false);
//			pw_time_left_title_on.SetActive (false);

//            pw_Text_Header.text = "Power Up Status";
//            pw_time_bar.GetComponent<Animator>().Play("PowerUpsChargingBarAnimation");
            globals.s.PW_ACTIVE = false;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITHOUT_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);

            PW_date = tempDate.ToString();

            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 0);

			activate_pw_bt.SetActive(true);
//			activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState ();

        }
    }

	//called by the Roullete
    public void add_pw_time(float time)
    {
        globals.s.PW_ACTIVE = true;
        tempDate = tempcurDate;
        tempDate = tempDate.AddMinutes(time);

        //tempDate = tempDate.AddSeconds(6);
        PW_date = tempDate.ToString();
        //activate_pw_bt.SetActive(false);

        PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
        PlayerPrefs.SetInt("PWState", 1);

    }

    void show_roullete_time()
    {
        tempcurDate = System.DateTime.Now;

        //NO DATE CASE=
        if (roullete_date == "")
        {
            CAN_ROTATE_ROULETTE = true;
            PlayerPrefs.SetInt("CanRotate", 1);
        }
        else
        {
            TimeSpan diff = tempDateRoulette.Subtract(tempcurDate);
            if (CAN_ROTATE_ROULETTE == false)
            {
                
                if (diff.TotalSeconds <= 0)
                {
                    Debug.Log("o tempo passou e eu sofri calado");
                    CAN_ROTATE_ROULETTE = true;
                    PlayerPrefs.SetInt("CanRotate", 1);
                }
            }
            if(diff.TotalSeconds > 0 && CAN_ROTATE_ROULETTE == false)
            {
//                roullete_time_left.GetComponent<Text>().text = diff.Minutes + "m " + diff.Seconds + "s ";
            }

        }
        if(CAN_ROTATE_ROULETTE == true)
        {
//            roullete_time_left.GetComponent<Text>().text = " ROTATE NOW ";

        }
        //TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);

    }

	public void show_roullete_time_level_end()
	{
		tempcurDate = System.DateTime.Now;
//		Debug.Log ("ETEEEEMPO");
		//NO DATE CASE=
		if (roullete_date == "")
		{
			CAN_ROTATE_ROULETTE = true;
			PlayerPrefs.SetInt("CanRotate", 1);
		}
		else
		{
			TimeSpan diff = tempDateRoulette.Subtract(tempcurDate);
			if (CAN_ROTATE_ROULETTE == false)
			{
				if (diff.TotalSeconds <= 0)
				{
					Debug.Log("o tempo passou e eu sofri calado");
					CAN_ROTATE_ROULETTE = true;
					PlayerPrefs.SetInt("CanRotate", 1);
				}
			}
			if(diff.TotalSeconds > 0 && CAN_ROTATE_ROULETTE == false)
			{
				pw_time_left_title_on.GetComponent<Text>().text = diff.Minutes.ToString("00") + ":" + diff.Seconds.ToString("00") + "";
			}

		}
		if(CAN_ROTATE_ROULETTE == true)
		{
//			pw_time_left_title_on.GetComponent<Text>().text = " ROTATE NOW ";
			GameOverController.s.SetDiskSpinNowState ();

		}
		//TimeSpan difference = tempDate.Subtract(tempcurDate);
		//Debug.Log(tempDate + " sadsad " + tempcurDate);

	}
    
	public void addRoulleteTime()
    {
        PlayerPrefs.SetInt("CanRotate", 0);
        //Debug.Log("rodou efalsificou");
        CAN_ROTATE_ROULETTE = false;
        tempDateRoulette = System.DateTime.Now;
		if(QA.s.SPIN_INFINITE == false)
        tempDateRoulette = tempDateRoulette.AddMinutes(GD.s.GD_ROULLETE_WAIT_MINUTES);
		else
			tempDateRoulette = tempDateRoulette.AddSeconds(3);
        roullete_date = tempDateRoulette.ToString();

        PlayerPrefs.SetString("RouletteDate2ChangeState", roullete_date);

    }
    #endregion

    #region ===== REVIVE =======
    public void show_revive_menu()
    {
		Debug.Log ("[HUD] REVIVE MENU OPENING");
        revive.SetActive(true);
		revive.GetComponent<Revive_menu_ctrl> ().Init ();
    }

    public void close_revive_menu() // If player said no to Revive
    {
		Debug.Log ("[HUD] CLOSING REVIVE MENU");

        revive.SetActive(false);
        AnalyticController.s.ReportRevive(false);

        game_controller.s.game_over_for_real(true);
    }

    public void revive_menu_start() // If player pressed Yes to revive
    {
        globals.s.SHOW_VIDEO_AFTER = true;
        globals.s.CAN_RESTART = false;
        globals.s.REVIVING = true;

        hide_game_over();
        revive.SetActive(false);
        ready.SetActive(true);

        game_controller.s.activate_logic();
        game_controller.s.ReviveRemovalLogicForClosestObstacles();
//		if(sound_controller.s != null) sound_controller.s.play_music();
        AnalyticController.s.ReportRevive(true);

        Invoke("partiu", 1);
    }

    void partiu()
    {
        ready.GetComponent<Text>().text = "GO!";
        Invoke("vaivaivai", 1);
    }

    void vaivaivai()
    {
        game_controller.s.anda_bolinha_fdd();
        globals.s.REVIVING = false;
        ready.SetActive(false);
    }

    public void show_video_revive()
    {
        globals.s.MENU_OPEN = true;
		displayingVideoMessage.SetActive (true);

//		#if UNITY_ANDROID
        Invoke("appear_video", 1.7f);
//		#endif

//		#if UNITY_STANDALONE_WIN
//		watched_the_video_revive();
//		#endif
    }

    void appear_video()
    {
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(true,false);
        //video.GetComponent<new_external_link_bt>().set_variables(true, false);
        flagVideoPower = false;
        flagVideoCoins = false;
        flagVideoRevive = true;
        ShowAd();
    }

    public void watched_the_video_revive()
    {
        //video.SetActive(false);
		displayingVideoMessage.SetActive (false);
       
        globals.s.SHOW_VIDEO_AFTER = false;
        Invoke("change_menu_open_state_revive", 0.5f);
    }

    void change_menu_open_state_revive()
    {
        globals.s.MENU_OPEN = false;
        globals.s.CAN_RESTART = true;

		game_controller.s.game_over_for_real(true);

//		hud_controller.si.show_game_over(cur_floor + 1, temp_flag_high_score_game_over);
//		game_over_for_real();
    }

    #endregion

	#region ======== Video =========

	public void DisplayRewardedVideo2(string videoType){
		RewardedVideoType tempVideo = (RewardedVideoType) System.Enum.Parse (typeof(RewardedVideoType), videoType);
		if (tempVideo != null) { // @TBD THERE IS REWARDED VIDEO TO SHOW
			curVideoType = tempVideo;
			ShowAd ();
		}
	}

	public void DisplayRewardedVideo(RewardedVideoType videoType){
		if (1 == 1) { // @TBD THERE IS REWARDED VIDEO TO SHOW
			curVideoType = videoType;
			ShowAd ();
		}
	}

    public void show_video_pw()
    {
        //globals.s.MENU_OPEN = true;
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        //video.GetComponent<new_external_link_bt>().set_variables(false, true);
//        if(CAN_ROTATE_ROULETTE == false)
//        {
        

		flagVideoRevive = false;
		flagVideoCoins = false;
		flagVideoPower = true;
        ShowAd();

		if (QA.s.PC_MODE)
			watched_the_video_pw();


//        }
    }

    public void watched_the_video_pw()
    {
        hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        Debug.Log("DISK new date!!");
        game_controller.s.activate_logic();
        video.SetActive(false);
        Invoke("change_menu_open_state", 1f);
    }

    public void show_video_coins()
    {
        globals.s.MENU_OPEN = true;
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        //video.GetComponent<new_external_link_bt>().set_variables(false, true);
        flagVideoRevive = false;
        flagVideoCoins = true;
        flagVideoPower = false;
        ShowAd();

    }

    public void watched_the_video_coins()
    {
        //hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        game_controller.s.activate_logic();
        video.SetActive(false);
        Debug.Log("new date");

        Invoke("change_menu_open_state", 1f);
    }

	public void ShowVideoResortStyle() {
		globals.s.MENU_OPEN = true;
		//video.SetActive(true);
		//video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
		//video.GetComponent<new_external_link_bt>().set_variables(false, true);
		flagVideoRevive = false;
		flagVideoCoins = true;
		flagVideoPower = false;
		#if UNITY_ANDROID || UNITY_IOS
		ShowAd();
		#endif

		#if UNITY_STANDALONE_WIN
			WatchedVideoResortStyle();
		#endif

	}
		
	public void WatchedVideoResortStyle() {
		//hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
		game_controller.s.activate_logic();
		video.SetActive(false);
		Debug.Log("watched resort style!!!");

		Invoke("change_menu_open_state", 1f);
	}


    void change_menu_open_state()
    {
        globals.s.MENU_OPEN = false;
    }

    public void ShowAd()
    {
    #if UNITY_ANDROID || UNITY_IOS
    #if USE_APPODEAL
        // Runs Appodeal Rewarded Ad.
        PluginManager.Instance.RunAppodealAd((Ads.AdType.Rewarded));
    #endif
            /*
            if (Advertisement.IsReady("rewardedVideo"))
            {
                var options = new ShowOptions { resultCallback = HandleShowResult };
                Advertisement.Show("rewardedVideo", options);
            }
            else
            {
                Advertisement.Initialize("1194074");
            }
            */
    #endif
    }

	#if UNITY_ANDROID || UNITY_EDITOR || UNITY_IOS
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
			if (flagVideoPower == true || curVideoType == RewardedVideoType.RespinDisk) {
				CAN_ROTATE_ROULETTE = true;
				PlayerPrefs.SetInt ("CanRotate", 1);
				roda_a_roda.ReSpinVideoWatched ();
//                    StartCoroutine (openTampa());
				//Invoke("activeRodaaRoda", 1);
				flagVideoPower = false;
			} else if (flagVideoRevive == true || curVideoType == RewardedVideoType.Revive) {
				watched_the_video_revive ();
			} else if (flagVideoCoins == true || curVideoType == RewardedVideoType.ResortChar) {
//					store_controller.s.watchedVideo();
				store_controller.s.WatchedVideoForResort ();
			} else if (curVideoType == RewardedVideoType.SpinDisk) {
				RodaMenu ();
			} else if (curVideoType == RewardedVideoType.DoubleReward) {

			}

                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
				// @TBD HANDLE RESULT DIFFERENTLY
				if(flagVideoPower == true)
				{
					CAN_ROTATE_ROULETTE = true;
					PlayerPrefs.SetInt("CanRotate", 1);
					roda_a_roda.ReSpinVideoWatched ();
					//                    StartCoroutine (openTampa());
					//Invoke("activeRodaaRoda", 1);
					flagVideoPower = false;
				}
				else if(flagVideoRevive == true) {
					watched_the_video_revive();
				}
				else if(flagVideoCoins == true) {
					//					store_controller.s.watchedVideo();
					store_controller.s.WatchedVideoForResort();
				}
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");

				// @TBD HANDLE RESULT DIFFERENTLY

				if(flagVideoPower == true)
				{
					CAN_ROTATE_ROULETTE = true;
					PlayerPrefs.SetInt("CanRotate", 1);
					roda_a_roda.ReSpinVideoWatched ();
					//                    StartCoroutine (openTampa());
					//Invoke("activeRodaaRoda", 1);
					flagVideoPower = false;
				}
				else if(flagVideoRevive == true)
				{
					watched_the_video_revive();
				}
				else if(flagVideoCoins == true)
				{
					//					store_controller.s.watchedVideo();
					store_controller.s.WatchedVideoForResort();
				}

                break;
        }
    }
#endif

    #endregion


    #region ======== SPIN DISK ============
    public void RodaMenu(){
		//StartCoroutine(activeRodaaRoda());
		globals.s.previousGameScreen = globals.s.curGameScreen;
		globals.s.curGameScreen = GameScreen.SpinDisk;

		roda_a_roda.gameObject.SetActive(true);

		roda_a_roda.Entrance ();
	}

	public void PowerUpsMenuClose(){
		globals.s.curGameScreen = globals.s.previousGameScreen;

		roda_a_roda.gameObject.SetActive (false);
	}

	IEnumerator openTampa()
    {
        yield return new WaitForSeconds(0.15f);
        //roda_a_roda.gameObject.SetActive(true);
        //roda_a_roda.Entrance ();
        roda_a_roda.openTampa();
    }

    IEnumerator closeTampa()
    {
        yield return new WaitForSeconds(0.15f);
        //roda_a_roda.gameObject.SetActive(true);
        //roda_a_roda.Entrance ();
        roda_a_roda.openTampa();
    }
    #endregion

    #region ======== Gift ============
    void show_gift_time()
    {
        tempcurDate = System.DateTime.Now;

        //NO DATE CASE
        if (gift_date == "")
        {
            CAN_GET_GIFT = true;
			//Debug.Log("asd");
            PlayerPrefs.SetInt("CanGetGift", 1);
			//giftBt.GetComponent<GiftButton> ().SetGetNowState ();
        }

        else
        {
            TimeSpan diff = tempDateGift.Subtract(tempcurDate);
            if (CAN_GET_GIFT == false)
            {
                if (diff.TotalSeconds <= 0)
                {
                    Debug.Log("o tempo passou e eu sofri calado");
                    CAN_GET_GIFT = true;
                    PlayerPrefs.SetInt("CanGetGift", 1);
//					giftBt.GetComponent<GiftButton> ().SetGetNowState ();

                }
            }

            if (diff.TotalSeconds > 0 && CAN_GET_GIFT == false)
            {
				//gift_time_left.GetComponent<Text>().text = diff.Days + "d " + diff.Hours + "h " + diff.Minutes + "m " + diff.Seconds + "s ";
				if( diff.Hours >= 1)
					gift_time_left.GetComponent<Text>().text = diff.Hours + "h\n" + diff.Minutes + "m";
				else
					gift_time_left.GetComponent<Text>().text = diff.Minutes + "m\n" + diff.Seconds + "s";

            }

        }

//        if(CAN_GET_GIFT == true)
//        {
//            gift_time_left.GetComponent<Text>().text = " GET NOW";
//			giftBt.GetComponent<GiftButton> ().SetGetNowState ();
//
//        }

        //TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);


    }

	// OLD GIFT ANIMATION LOGIC
    public void getGift()
    {
        PlayerPrefs.SetInt("CanGetGift", 0);
        CAN_GET_GIFT = false;
        tempDateGift = System.DateTime.Now;
        tempDateGift = tempDateGift.AddMinutes(GD.s.GD_GIFT_WAIT_MINUTES);
        gift_date = tempDateGift.ToString();

        PlayerPrefs.SetString("GiftDate2ChangeState", gift_date);
    }
  
	public void GiftButtonClicked(MusicStyle styleToGive){
		if (CAN_GET_GIFT || 1==1) { //codigo que testa se pode dar o presente,
			
			bool rand_found;
			int rand = 0; 
			int count = 0;

//			do {
//				rand = UnityEngine.Random.Range (1, GD.s.N_MUSIC+1);
////				rand_found = store_controller.s.CheckIfCharacterIsAlreadyPurchased (rand);
//				rand_found = store_controller.s.CheckIfCharacterIsAlreadyPurchasedNew ((MusicStyle) rand);
//				count++;
//			} while (rand_found == true && count < 50);
				
			if (count < 50) {
				giftAnimationEnded = false;
				globals.s.GIFT_ANIMATION = true;
				giftAnimation.SetActive (true);
				
//				StartCoroutine (GiveCharNew ((MusicStyle)rand));
//				StartCoroutine (GiveCharNew (styleToGive));

				giftAnimation.GetComponent<GiftAnimationLogic> ().Init ();
			}
			else {
				Debug.Log ("ERROR!!");
				// TBD
			}
			
		} else {//fazer alguma coisa ?

		}
	}	

	IEnumerator GiveCharNew(MusicStyle style){
		yield return new WaitForSeconds (1f);

		giftChar.SetActive (true);
		giftChar.GetComponent<CharGift> ().InitAnimation (style);
//		store_controller.s.GiveCharacterForFreeNew (style);

//		giftBt.GetComponent<GiftButton> ().SetCountownState ();
		getGift ();

		giftAnimation.GetComponent<GiftAnimationLogic> ().EnterTitle (style);
	}

    //
    //	IEnumerator GiveChar(int musicType){
    //		yield return new WaitForSeconds (1f);
    //
    //		giftChar.SetActive (true);
    //		giftChar.GetComponent<CharGift> ().InitAnimation ((MusicStyle)musicType);
    //		store_controller.s.GiveCharacterForFree (musicType);
    //
    //		giftBt.GetComponent<GiftButton> ().SetCountownState ();
    //		getGift ();
    //
    //		giftAnimation.GetComponent<GiftAnimationLogic> ().EnterTitle ((MusicStyle)musicType);
    //	}

    #endregion

    #region PAUSE MENU
    public void OpenPauseMenu()
    {
        pause.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pause.GetComponent<PauseMenu>().BGAlphaOff();
    }
    #endregion
}
