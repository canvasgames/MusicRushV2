using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameOverController : MonoBehaviour {

	#region === Variables ===
	public static GameOverController s;

	public GameObject careerOverTitle, careerOverE;
	public Text scoreText, bestText;
	public GameObject scorePanel;

	[Space(5)] 
	public GameObject jukeboxGroup;
	public GameObject jukeboxBt, jukeboxNote, jukeboxIcon, jukeboxGetNow, jukeboxPauta;
	public GameObject jukeboxBtGlow, jukeboxBtText;
	public Text jukeboxBarNotesText;
	public GameObject jukeboxGreenBar;
	[Space(10)] 

//	public Text ;
	public GameObject diskGroup;
	public GameObject diskGroupBg, storeGroupBg;
	public GameObject diskFreeSpinText, diskTimeLeft, diskFreeSpinNowText;
	public GameObject diskBt, diskBarGreen;
	[Space(10)] 
	public GameObject replayBt;

	bool DiskSpinNowAnimationStarted = false;

//	float noteXStart= -270 , noteXEnd = 272;
	float noteXStart= -678f , noteXEnd = -243f;
	float noteXLocalStart= -678f , noteXLocalEnd = -243f;

	float jukeboxBarInitialFill = 0.05f;

	float xTarget = 0, fillTarget = 0;
	// Use this for initialization
	float noteBarXPos;
	float yScorePos;
	float yTitlePos;
	float yReplayBtPos;
	#endregion

	#region === Init ===
	void Awake () {
		s = this;
//		noteBarXPos = jukeboxNote.transform.localPosition.x;
//		noteBarXPos = jukeboxNote.GetComponent<Rect>().rectTransform.anchoredPosition.x;
		noteBarXPos = jukeboxNote.GetComponent<RectTransform> ().anchoredPosition.x;

//
//		globals.s.curGameScreen = GameScreen.LevelEnd;
//		USER.s.NOTES = 58;
//		globals.s.NOTES_COLLECTED = 15;
//
//		EnteringAnimations();
//		Invoke ("EnteringAnimations", 0.5f);
		yTitlePos = careerOverTitle.transform.localPosition.y;
		yScorePos = scorePanel.transform.localPosition.y;
		yReplayBtPos = replayBt.transform.localPosition.y;

//		Init (34, 50);
//		USER.s.NOTES = 98;
//		globals.s.NOTES_COLLECTED_JUKEBOX = 8 ;
//		globals.s.NOTES_COLLECTED = 8 ;
	}

	public void Init(int curFloor = 0, int bestFloor = 0){
		USER.s.SaveUserNotes ();
		Debug.Log ("=============== GAME OVER START ============" );
		Debug.Log (" USER NOTES:" + USER.s.NOTES + "  jukebox price : " + globals.s.JUKEBOX_CURRENT_PRICE +" COLLECTED: " + globals.s.NOTES_COLLECTED);

//		if (USER.s.NEWBIE_PLAYER == 0) {
//			replayBt.transform.localPosition = new Vector2 (0, -157); 
//		}

		// Disk button logic - the player is a newbie yet
		if (FTUController.s.canIntroduceDisk == 0) {
			diskBt.SetActive (false);
			jukeboxBt.SetActive (false);
		} 
		// DISK NOT INTRODUCED YET
		else {
			// Introduce the disk for the first time
			if (FTUController.s.canIntroduceDisk == 1 && FTUController.s.canIntroduceStore == 0) {
				diskBt.SetActive (true);
//				SetDiskSpinNowState ();
			}
//			else
			SetDiskCountDownState ();

			if (FTUController.s.canIntroduceStore == 1) {
				// INIT JUKEBOX BT LOGIC - PROGRESS STATE
				if ((USER.s.NOTES - globals.s.NOTES_COLLECTED) < globals.s.JUKEBOX_CURRENT_PRICE) {
					SetJukeboxProgressState ();

					DisplayJukeboxNotes (USER.s.NOTES - globals.s.NOTES_COLLECTED);

					float fillDif = 1 - jukeboxBarInitialFill;
					float fillCurrent = (USER.s.NOTES - globals.s.NOTES_COLLECTED) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;

					jukeboxGreenBar.GetComponent<Image> ().fillAmount = fillCurrent + jukeboxBarInitialFill;
					fillTarget = (USER.s.NOTES) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;
				} 
			// JUKEBOX GET NOW STATE
				else {
					jukeboxBt.GetComponent<BtJukebox> ().ResetMyParticles ();
					StartCoroutine (JukeboxGetNow (0));
				}
			}
		}
		StartCoroutine (EnteringAnimations (curFloor, bestFloor));
	}

	public void Enterer(){
		globals.s.curGameScreen = GameScreen.LevelEnd;
//		Init ();
		Init (Random.Range(2,40), 50);
//		StartCoroutine (EnteringAnimations ());
	}

	public IEnumerator EnteringAnimations(int curFloor = 0, int bestFloor = 0){
//		Debug.Log ("x pos : "+  jukeboxGroup.transform.position +  " width "+ jukeboxGroup.GetComponent<RectTransform> ().rect.width);

		// >>>> PLACE THE BUTTON OUTSIDE THE SCREEN
		jukeboxGroup.GetComponent<RectTransform> ().position = new Vector2 (0 - jukeboxGroup.GetComponent<RectTransform> ().rect.width/100 , jukeboxGroup.GetComponent<RectTransform> ().position.y);
		diskGroup.GetComponent<RectTransform> ().position = new Vector2 (0 - diskGroup.GetComponent<RectTransform> ().rect.width / 100, diskGroup.GetComponent<RectTransform> ().position.y);
//		replayBt.transform.position = new Vector2 (replayBt.transform.position.x,  globals.s.CANVAS_Y_BOTTOM/100 - replayBt.GetComponent<RectTransform> ().rect.height/100); 
		replayBt.transform.position = new Vector2 (replayBt.transform.position.x,  replayBt.transform.position.y + -3f); 

		//  >>>>> TITLE ANIMATION
		if (curFloor > 0) {
			// reset positions
			scorePanel.transform.localScale = Vector3.one;
			scorePanel.transform.localPosition = new Vector2 (scorePanel.transform.localPosition.x, yScorePos + 1200);
			scoreText.text = "0";
			bestText.text = bestFloor.ToString();
			careerOverTitle.transform.localPosition = new Vector2 (careerOverTitle.transform.localPosition.x, careerOverTitle.transform.localPosition.y + 450);

			yield return new WaitForSeconds (0.05f); // init animations

			careerOverTitle.transform.DOLocalMoveY (yTitlePos, 0.5f).SetEase (Ease.OutBounce);
			careerOverTitle.transform.DOShakeRotation (0.5f, new Vector3 (0, 0, 80), 14, 94);

			yield return new WaitForSeconds (0.5f);

			scorePanel.transform.DOLocalMoveY(yScorePos, 0.34f).SetEase(Ease.OutQuart);

			yield return new WaitForSeconds (0.33f);

			StartCoroutine (RaiseNumbers (curFloor, bestFloor));

			yield return new WaitForSeconds (0.6f);
			StartCoroutine (CareerOverEAnimation ());

			scorePanel.transform.DOLocalMoveY (-182f, 0.2f);
			scorePanel.transform.DOScale (new Vector3 (0.562f, 0.562f, 0.562f), 0.2f);
			yield return new WaitForSeconds (0.25f);
		}
		//  >>>> BUTTONS ANIMATION

		if ((globals.s.FIRST_GAME == false && FTUController.s.canIntroduceStore == 1) && (USER.s.NEWBIE_PLAYER == 0 || QA.s.OLD_PLAYER == true)) {
			jukeboxGroup.GetComponent<RectTransform> ().DOLocalMoveX (0, 0.5f).SetEase (Ease.OutCubic).OnComplete(InitJukeboxGroupInfo);
			yield return new WaitForSeconds (0.45f);
//		Debug.Log ("22 x pos : "+  jukeboxGroup.transform.position +  " width "+ jukeboxGroup.GetComponent<RectTransform> ().rect.width);
		}

		if (globals.s.FIRST_GAME == false && FTUController.s.canIntroduceDisk == 1 && (USER.s.NEWBIE_PLAYER == 0 || QA.s.OLD_PLAYER == true)) {
			diskGroup.GetComponent<RectTransform> ().DOLocalMoveX (0, 0.5f).SetEase (Ease.OutCubic).OnComplete(SpinDiskTryToStartAnimation);
			yield return new WaitForSeconds (0.25f);
		}

		// >>>>> REPLAY BUTTON LOGIC 
		if ( (FTUController.s.canIntroduceDisk == 1 && FTUController.s.canIntroduceStore == 0) ||
			(FTUController.s.canIntroduceStore == 1 && FTUController.s.firstSongPurchased == 0)) {
		} else {
			replayBt.transform.DOLocalMoveY (yReplayBtPos, 0.5f);
		}
	}

	IEnumerator CareerOverEAnimation(){
		if (globals.s.GAME_OVER == 1) {

			careerOverE.SetActive (true);
			yield return new WaitForSeconds (0.1f);
			careerOverE.SetActive (false);
			yield return new WaitForSeconds (0.1f);
			careerOverE.SetActive (true);
			yield return new WaitForSeconds (0.1f);
			careerOverE.SetActive (false);

			yield return new WaitForSeconds (Random.Range(1.3f, 2.5f));

			StartCoroutine (CareerOverEAnimation ());
		}
	}

	IEnumerator RaiseNumbers(int curScore = 0, int curBest = 0){
		int value = 0;
		int inc = curScore/15;
		if (inc == 0)
			inc = 1;
		for (int i = 0; i < 15 && value < curScore; i++) {
			yield return new WaitForSeconds (0.035f);
			value += inc;
			scoreText.text = value.ToString();
		}
		scoreText.text = curScore.ToString();
	}

	#endregion

	// Update is called once per frame
	void Update () {
		if(globals.s.curGameScreen == GameScreen.LevelEnd){
			if (hud_controller.si.CAN_ROTATE_ROULETTE == false) {
//				Debug.Log ("LEVEL END SCREEEN GAME OVER");

				DiskSpinNowAnimationStarted = true;

				hud_controller.si.show_roullete_time_level_end ();

//				SetDiskCountDownState ();
			}
			else {
//				SpinDiskStartAnimation ();
			}
			//			GetComponent<Text>().text =  difference.Minutes + ":" + difference.Seconds + "";
		}
	}

	#region === Jukebox ===

	void InitJukeboxGroupInfo(){
		if (globals.s.NOTES_COLLECTED > 0 && USER.s.NOTES - globals.s.NOTES_COLLECTED < globals.s.JUKEBOX_CURRENT_PRICE) 
			StartCoroutine (NoteAnimation());
	}

	public void UpdateJukeboxInformation(){

		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			SetJukeboxNewMusicState ();
		}
		else
			SetJukeboxProgressState ();

		DisplayJukeboxNotes (USER.s.NOTES);

		float fillDif = 1 - jukeboxBarInitialFill;
		float fillCurrent = (USER.s.NOTES) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;

		jukeboxGreenBar.GetComponent<Image> ().fillAmount = fillCurrent + jukeboxBarInitialFill;

	}

	void SetJukeboxNewMusicState(){
		jukeboxGetNow.SetActive (true);
		jukeboxPauta.SetActive(false);
		jukeboxNote.SetActive(false);
		jukeboxBarNotesText.gameObject.SetActive(false);
//		jukeboxBt.GetComponent<Animator> ().Play ("bt_store_new_style");

		jukeboxBt.GetComponent<BtJukebox>().SetNewStyleState(true);

		jukeboxGreenBar.GetComponent<Animator> ().Play ("BlueBarReadyAnim");
	}

	void SetJukeboxProgressState(){
		jukeboxGetNow.SetActive (false);
		jukeboxPauta.SetActive(true);
		jukeboxNote.SetActive(true);
		jukeboxBarNotesText.gameObject.SetActive(true);
		jukeboxBt.SetActive (true);

		jukeboxBt.GetComponent<BtJukebox>().SetChangeStyleState();

		jukeboxGreenBar.GetComponent<Animator> ().Play ("BlueBarStaticAnim");

//		jukeboxBt.GetComponent<Animator> ().Play ("bt_store_anim");
	}


	public void DisplayJukeboxNotes(int notesToShow){
		if (GD.s.JUKEBOX_FTU_PRICE == globals.s.JUKEBOX_CURRENT_PRICE) {
//			jukeboxNote.transform.localPosition = new Vector2 (noteBarXPos, jukeboxNote.transform.localPosition.y);
			jukeboxNote.GetComponent<RectTransform>().anchoredPosition = new Vector2 (noteBarXPos, jukeboxNote.transform.localPosition.y);
			jukeboxBarNotesText.text = notesToShow.ToString("00") + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
		else {
			jukeboxNote.GetComponent<RectTransform>().anchoredPosition = new Vector2 (noteBarXPos, jukeboxNote.transform.localPosition.y);
			jukeboxBarNotesText.text =  notesToShow.ToString("00") + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();

//			if (notesToShow < 10) {
//				jukeboxBarNotesText.text = "0" + notesToShow.ToString() + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
//			}
//			else
//				jukeboxBarNotesText.text = notesToShow.ToString() + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
	}

	IEnumerator JukeboxGetNow(float wait){
		yield return new WaitForSeconds (wait);
		SetJukeboxNewMusicState ();

//		jukeboxNote.transform.localPosition = new Vector2 (noteXEnd, jukeboxNote.transform.localPosition.y);

		jukeboxGreenBar.GetComponent<Image> ().fillAmount = 1;

//		BlinkJukeboxGroup();
//		jukeboxIcon.GetComponent<Animator>().Play("jukebox icon animation");
	}


	IEnumerator NoteAnimation(){
		yield return new WaitForSeconds (0.3f);

		StartCoroutine(IncreaseNoteNumbers(0.5f));

		//		jukeboxNote.transform.DOLocalMoveX (noteXStart + xTarget, 0.5f).SetEase (Ease.InOutCubic);

		Debug.Log ("Note Animation: FILL AMOUNT TARGET IS : " + fillTarget);

		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxGreenBar.GetComponent<Image> ().DOFillAmount (jukeboxBarInitialFill + fillTarget, 0.5f).SetEase (Ease.InOutCubic).OnComplete(() 
				=> StartCoroutine(JukeboxGetNow(0.15f)));

		} else {
			jukeboxGreenBar.GetComponent<Image> ().DOFillAmount (jukeboxBarInitialFill + fillTarget, 0.5f).SetEase (Ease.InOutCubic);
		}

	}


	IEnumerator IncreaseNoteNumbers(float time){
		float dif = globals.s.NOTES_COLLECTED ;
		for (int i = 1; i <= dif ; i++) {
			DisplayJukeboxNotes (USER.s.NOTES - globals.s.NOTES_COLLECTED + i);
			yield return new WaitForSeconds (time / dif);
			Debug.Log ("DIF: " + dif + " INCREASING!! " + (dif + i));
		}

		yield return new WaitForSeconds (1f);

		globals.s.NOTES_COLLECTED = 0;

	}

	void BlinkJukeboxGroup(){
		jukeboxGetNow.GetComponent<Text> ().color = Color.green;
		jukeboxBt.GetComponent<Image> ().color = new Color(0.4f, 1, 1);
		//		jukeboxBtGlow.GetComponent<Image> ().color = new Color(0.4f, 1, 1);
		Invoke ("BlinkJukeboxGroup2", 0.2f);
	}
	void BlinkJukeboxGroup2(){
		jukeboxGetNow.GetComponent<Text> ().color = Color.white;
		jukeboxBt.GetComponent<Image> ().color = Color.white;
		//		jukeboxBtGlow.GetComponent<Image> ().color = Color.white;

		Invoke ("BlinkJukeboxGroup", 0.2f);
	}

	#endregion

	#region === SpinDisk === 

	void SetDiskCountDownState(){
//		diskBarGreen.SetActive (false);
//		diskFreeSpinNowText.SetActive (false);
//		diskTimeLeft.SetActive (true);
//		diskFreeSpinText.SetActive (true);
//		diskBt.GetComponent<Button> ().interactable = false;

		diskBt.GetComponent<activate_pw_button> ().SetCountownState();
	}

	public void SetDiskSpinNowState(){
		diskBarGreen.SetActive (true);
		diskTimeLeft.SetActive (false);
		diskFreeSpinText.SetActive (false);

		diskBt.GetComponent<Button> ().interactable = true;
	}

	void SpinDiskTryToStartAnimation(){
		if (hud_controller.si.CAN_ROTATE_ROULETTE == true) 
			diskBt.GetComponent<activate_pw_button> ().SetSPinNowState ();

//		diskFreeSpinText.SetActive (false);
//		diskTimeLeft.SetActive (false);
//		diskFreeSpinNowText.SetActive (true);
//		diskBarGreen.SetActive (true);

//		if (DiskSpinNowAnimationStarted == false) {
//			DiskSpinNowAnimationStarted = true;
//			BlinkSpinNow ();
//		}
	}
		
	#endregion
}
