using UnityEngine;
using System.Collections;

public class USER : MonoBehaviour {

    public static USER s;

	[HideInInspector] 	public int PW_INTRODUCED, GIFT_INTRODUCED, NEWBIE_PLAYER;
    [HideInInspector]   public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
    [HideInInspector]   public int TOTAL_GAMES_WITH_TUTORIAL, TOTAL_GAMES, TOTAL_VIDEOS_WATCHED, TUTORIAL_GAMES;
    [HideInInspector]   public int FIRST_PW_CREATED, FIRST_HOLE_CREATED, FIRST_WALL_CREATED;
	[HideInInspector]   public int NOTES, TOTAL_NOTES;
	[HideInInspector]   public int DIAMONDS, TOTAL_DIAMONDS;
//    [HideInInspector]   public int N_CHARS_PURCHASED;
    [HideInInspector]
    public int SOUND_MUTED;

    void Awake() {
//		PlayerPrefs.SetInt ("notes", 4);

		globals.s.ACTUAL_STYLE = (MusicStyle) PlayerPrefs.GetInt ("curStyle", 0);
		globals.s.ACTUAL_SKIN =  GD.s.skins[PlayerPrefs.GetInt ("curSkin", 0)];

        //PlayerPrefs.SetInt("total_games", 0);
		//PlayerPrefs.SetInt("best", 0);
		//PlayerPrefs.SetInt ("first_game", 0);
        SOUND_MUTED = PlayerPrefs.GetInt("sound_muted", 0);

		NOTES = PlayerPrefs.GetInt("notes", 0);
        TOTAL_NOTES = PlayerPrefs.GetInt("total_notes", 0);

		DIAMONDS = PlayerPrefs.GetInt ("diamonds", 0);

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
	#region === DIAMONDS ===
	public void AddDiamonds(int value, string source){
		Debug.Log ("::::::: USER ADD DDDIAMONDS CALLED: " + value + " CURRENT NOTES BEFORE: "+ USER.s.NOTES);
		USER.s.DIAMONDS += value;
		USER.s.TOTAL_DIAMONDS += value;
//		hud_controller.si.display_notes(USER.s.NOTES);

		PlayerPrefs.SetInt("diamonds", USER.s.DIAMONDS);
		PlayerPrefs.SetInt("total_diamonds", USER.s.TOTAL_DIAMONDS);

	}

	public void SpendDiamonds(int quantity, string orign){
		USER.s.DIAMONDS -= quantity;
		PlayerPrefs.SetInt("diamonds", USER.s.DIAMONDS);
	}

	public void SaveUserDiamonds(){
		PlayerPrefs.SetInt("total_diamonds", USER.s.TOTAL_DIAMONDS);
		PlayerPrefs.SetInt("diamonds", USER.s.DIAMONDS);
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

}
