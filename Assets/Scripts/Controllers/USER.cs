using UnityEngine;
using System.Collections;
using System;

public class USER : MonoBehaviour {

    public static USER s;

	[HideInInspector] 	public int PW_INTRODUCED, GIFT_INTRODUCED, NEWBIE_PLAYER;
    [HideInInspector]   public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
    [HideInInspector]   public int TOTAL_GAMES_WITH_TUTORIAL, TOTAL_GAMES, TOTAL_VIDEOS_WATCHED, TUTORIAL_GAMES;
    [HideInInspector]   public int FIRST_PW_CREATED, FIRST_HOLE_CREATED, FIRST_WALL_CREATED;
	[HideInInspector]   public int NOTES, TOTAL_NOTES;
	[HideInInspector]   public int GEMS, TOTAL_GEMS;
	[HideInInspector]   public string FIRST_SESSION_DATE, FIRST_SATURDAY, NEXT_SATURDAY;
	[HideInInspector]   public int LAST_UPDATE_UNLOCKED;
    [HideInInspector]   public int TOTAL_SESSIONS;

    [HideInInspector]   public int CUR_SPECIAL_OFFER;
	[HideInInspector]   public string SPECIAL_OFFER_END_DATE;

//    [HideInInspector]   public int N_CHARS_PURCHASED;
    [HideInInspector]
    public int SOUND_MUTED;

    void Awake() {
//		PlayerPrefs.SetInt ("notes", 4);
        TOTAL_SESSIONS = PlayerPrefs.GetInt("totalSessions", 0) + 1;
        PlayerPrefs.SetInt("totalSessions", TOTAL_SESSIONS);

        globals.s.ACTUAL_STYLE = (MusicStyle) PlayerPrefs.GetInt ("curStyle", 0);
		globals.s.ACTUAL_SKIN =  GD.s.skins[PlayerPrefs.GetInt ("curSkin", 0)];

		// ========= SPECIAL OFFER LOGIC ========

		CUR_SPECIAL_OFFER = PlayerPrefs.GetInt("cur_special_offer", -1);
		SPECIAL_OFFER_END_DATE = PlayerPrefs.GetString("special_offer_end_date", "");

		//
		//PlayerPrefs.SetInt("total_games", 0);
		//PlayerPrefs.SetInt("best", 0);
		//PlayerPrefs.SetInt ("first_game", 0);

		// DATE STUFF
		FIRST_SESSION_DATE = PlayerPrefs.GetString("first_session_date", "");
		if(FIRST_SESSION_DATE == ""){ 
			#if UNITY_EDITOR 
			Debug.Log ("LOOK! A NEWBIE LET'S MAKE FUN OF HIM!!!!!!"); 
			#endif
			DateTime tempDate = System.DateTime.Now;
			FIRST_SESSION_DATE = tempDate.ToString ();
			PlayerPrefs.SetString("first_session_date", FIRST_SESSION_DATE);

			int dayOfWeek = (int)tempDate.DayOfWeek;
			int nextSaturdayDif = 6 - dayOfWeek ;
			if (nextSaturdayDif == 0) // today is saturday
				nextSaturdayDif = 7;
			else
				nextSaturdayDif += 1; // saturday is not today, sum 1
//			TimeSpan dif; dif.
//			tempDate.Subtract(tempDate.Hour);
			Debug.Log("TEMPDATE RAW : " + tempDate.ToString() + " day of week: " + (int)tempDate.DayOfWeek);
			int curHours = tempDate.Hour;
			tempDate = tempDate.AddHours (-curHours);
			int curMins = tempDate.Minute;
			tempDate = tempDate.AddMinutes (-curMins);

			Debug.Log("TEMPDATE  - HOURS : " + tempDate.ToString() + " SAT DIF: "+ nextSaturdayDif);

			tempDate = tempDate.AddDays (nextSaturdayDif);
			Debug.Log("TEMPDATE NEXT SATURDAY: " + tempDate.ToString());

			FIRST_SATURDAY = tempDate.ToString ();
			PlayerPrefs.SetString("first_saturday", FIRST_SATURDAY);
			NEXT_SATURDAY = FIRST_SATURDAY;
			PlayerPrefs.SetString ("next_saturday", NEXT_SATURDAY);
//			System.DateTime.da
		}
		FIRST_SATURDAY = PlayerPrefs.GetString("first_saturday", "");
		NEXT_SATURDAY = PlayerPrefs.GetString("next_saturday", "");
		LAST_UPDATE_UNLOCKED = PlayerPrefs.GetInt ("last_update_unlocked", 0);

		SOUND_MUTED = PlayerPrefs.GetInt("sound_muted", 0);

		NOTES = PlayerPrefs.GetInt("notes", 0);
        TOTAL_NOTES = PlayerPrefs.GetInt("total_notes", 0);

		GEMS = PlayerPrefs.GetInt ("gems", 0);

        BEST_SCORE = PlayerPrefs.GetInt("best", 0);
        LAST_SCORE = PlayerPrefs.GetInt("last_score", 0);
        DAY_SCORE = PlayerPrefs.GetInt("day_best", 0);
		TOTAL_GAMES_WITH_TUTORIAL = PlayerPrefs.GetInt("total_games", 0);
//		TOTAL_GAMES = PlayerPrefs.GetInt("total_games_without_tutorial", 0);
		TOTAL_GAMES = PlayerPrefs.GetInt("total_games", 0);
		TUTORIAL_GAMES = PlayerPrefs.GetInt("total_tutorial_games", 0);
        TOTAL_VIDEOS_WATCHED =  PlayerPrefs.GetInt("total_videos_watched", 0);

        // new user variables
        FIRST_PW_CREATED =  PlayerPrefs.GetInt("first_pw_created", 0);
        FIRST_HOLE_CREATED =  PlayerPrefs.GetInt("first_hole_created", 0);
        FIRST_WALL_CREATED =  PlayerPrefs.GetInt("first_wall_created", 0);

		PW_INTRODUCED =  PlayerPrefs.GetInt("pw_introduced", 0);
		GIFT_INTRODUCED = PlayerPrefs.GetInt("gift_introduced", 0);
		NEWBIE_PLAYER = PlayerPrefs.GetInt("newbie_player", 1); // player already passed through 'hole' tutorial

        s = this;

		if (TOTAL_GAMES_WITH_TUTORIAL > 4 && FIRST_PW_CREATED == 1) {
//			GIFT_INTRODUCED = 1;
//			PlayerPrefs.SetInt("gift_introduced", 1);
		}
    } 

	#region === COIN NOTES ===
	public void AddNotes(int value, string source = ""){
		Debug.Log ("::::::: USER ADD NOTES CALLED: " + value + " CURRENT NOTES BEFORE: "+ USER.s.NOTES);
		USER.s.NOTES += value;
		USER.s.TOTAL_NOTES += value;
		DataRecorderController.s.notesCollectedThisSession += value;
		hud_controller.si.display_notes(USER.s.NOTES);
//		store_controller.s.UpdateUserNotes ();

		PlayerPrefs.SetInt("notes", USER.s.NOTES);
		PlayerPrefs.SetInt("total_notes", USER.s.TOTAL_NOTES);

//		if (GameOverController.s != null && globals.s.GAME_OVER == 1)
//			GameOverController.s.Init ();
	}
	public void SpendUserNotes(int quantity, string source){
		USER.s.NOTES -= quantity;
		PlayerPrefs.SetInt("notes", USER.s.NOTES);
	}

	public void SaveUserNotes(){
		PlayerPrefs.SetInt("notes", USER.s.NOTES);
		PlayerPrefs.SetInt("total_notes", USER.s.TOTAL_NOTES);
	}



	#endregion
	#region === GEMS ===
	public void AddGems(int value, string source){
		Debug.Log ("::::::: USER ADD GEMS CALLED: " + value + " CURRENT NOTES BEFORE: "+ USER.s.NOTES);
		USER.s.GEMS += value;
		USER.s.TOTAL_GEMS += value;
//		hud_controller.si.display_notes(USER.s.NOTES);

		PlayerPrefs.SetInt("gems", GEMS);
		PlayerPrefs.SetInt("total_gems", TOTAL_GEMS);

	}

	public void SpendGems(int quantity, string orign){
		GEMS -= quantity;
		PlayerPrefs.SetInt("gems", GEMS);
	}

	public void SaveUserGems(){
		PlayerPrefs.SetInt("total_gems", TOTAL_GEMS);
		PlayerPrefs.SetInt("gems", GEMS);
	}

	#endregion

	public void SetCurrentSelectedMusic(MusicStyle style, int skinId){
		PlayerPrefs.SetInt ("curStyle", (int)style);
		globals.s.ACTUAL_STYLE = style;
		PlayerPrefs.SetInt ("curSkin", skinId);
		globals.s.ACTUAL_SKIN = GD.s.skins[skinId];;
		Debug.Log (" [USER] SET NEW SKIN: " + globals.s.ACTUAL_SKIN.skinName + " ID: " + globals.s.ACTUAL_SKIN.id);
	}

	public void SaveLastFloor(int currentFloor){
		PlayerPrefs.SetInt("last_score", currentFloor);
		LAST_SCORE = currentFloor;
	}

	public void SaveUserTotalGames(int n){
//		PlayerPrefs.SetInt ("total_games_without_tutorial", n);
		PlayerPrefs.SetInt ("total_games", n);
		USER.s.TOTAL_GAMES = n;	
	}

	public void SaveUserTutorialGames(int n){
		PlayerPrefs.SetInt ("total_games_without_tutorial", n);
		USER.s.TUTORIAL_GAMES = n;
	}

	public void SetNotNewbiePlayer(){
		PlayerPrefs.SetInt ("newbie_player", 0);
		USER.s.NEWBIE_PLAYER = 0;
	}

	public void SetUpdateCharsUnlocked(){
		LAST_UPDATE_UNLOCKED++;
		PlayerPrefs.SetInt ("last_update_unlocked", LAST_UPDATE_UNLOCKED);
		// updating next saturday date
		DateTime firstSatTime = Convert.ToDateTime (USER.s.FIRST_SATURDAY);
		firstSatTime = firstSatTime.AddDays (LAST_UPDATE_UNLOCKED * 6);
		Debug.Log (" SETTING LAST UPDATE UNLOCKED!!: " + LAST_UPDATE_UNLOCKED + " NEXT SAT DATE: " + firstSatTime);
		NEXT_SATURDAY = firstSatTime.ToString ();
		PlayerPrefs.SetString ("next_saturday", NEXT_SATURDAY);
	}

	public bool CheckIfSpecialOfferTimeHasElapsedAndUpdate(){

		DateTime tempDate = System.DateTime.Now;
		if (SPECIAL_OFFER_END_DATE == "") {
			tempDate = tempDate.AddDays (1);
			SPECIAL_OFFER_END_DATE = tempDate.ToString ();
			PlayerPrefs.SetString ("special_offer_end_date", SPECIAL_OFFER_END_DATE);
			Debug.Log ("SPECIAL OFFER TIME IS EMPTY!!! DEFINTING TO " + SPECIAL_OFFER_END_DATE.ToString() );
			return true;
		} else {
			DateTime specialOfferDate = Convert.ToDateTime (SPECIAL_OFFER_END_DATE);
			TimeSpan diff = tempDate.Subtract(specialOfferDate);
			if (diff.TotalSeconds > 0) {
				Debug.Log ("time to define new special offer!!!!! : "+SPECIAL_OFFER_END_DATE.ToString());

				tempDate = tempDate.AddDays (1);
				SPECIAL_OFFER_END_DATE = tempDate.ToString ();
				PlayerPrefs.SetString ("special_offer_end_date", SPECIAL_OFFER_END_DATE);

				return true;
				//tbd check screen
			} else
				return false;
		}
	}

	public void SetANewSpecialOffer(){
		CUR_SPECIAL_OFFER = -1;
		PlayerPrefs.SetInt ("cur_special_offer", CUR_SPECIAL_OFFER);

		DateTime tempDate = System.DateTime.Now;
		tempDate = tempDate.AddDays (1);
		SPECIAL_OFFER_END_DATE = tempDate.ToString ();
		PlayerPrefs.SetString ("special_offer_end_date", SPECIAL_OFFER_END_DATE);
		Debug.Log ("SPECIAL OFFER TIME IS EMPTY!!! DEFINTING TO " + SPECIAL_OFFER_END_DATE.ToString() );
	}

	public void SetCurrentSpecialOffer(MusicStyle style){
		CUR_SPECIAL_OFFER = (int)style;
		PlayerPrefs.SetInt ("cur_special_offer", CUR_SPECIAL_OFFER);
	}
}
