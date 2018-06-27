using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FTUController : MonoBehaviour {
	public static FTUController s;

	public int canIntroduceDisk = 0;
	public int canIntroduceStore = 0;
	public int firstSongPurchased = 0;

	public GameObject spinDiskBt, jukeboxBt, handTut;

	void Awake(){
		s = this;
		canIntroduceDisk = PlayerPrefs.GetInt ("canIntroduceDisk", 0);
		canIntroduceStore = PlayerPrefs.GetInt ("canIntroduceStore", 0);
		firstSongPurchased = PlayerPrefs.GetInt ("firstSongPurchased", 0);
	}

	// Use this for initialization
	public void Start () {
		if (firstSongPurchased == 0)
			globals.s.JUKEBOX_CURRENT_PRICE = 5;
		else
			globals.s.JUKEBOX_CURRENT_PRICE = GD.s.JUKEBOX_PRICE;


		//SETTING  FIRST_GAME GLOBAL
		if(PlayerPrefs.GetInt("first_game", 1) == 1)
		{
			globals.s.FIRST_GAME = true;
			PlayerPrefs.SetInt("first_game", 0); ;
		}
		else
		{
			globals.s.FIRST_GAME = false;
		}
			
		// introduce spin disk for the first time)
		if (USER.s.NEWBIE_PLAYER == 0 && canIntroduceDisk == 0) {
			PlayerPrefs.SetInt ("canIntroduceDisk", 1);
			canIntroduceDisk = 1;
		}
			
		if (firstSongPurchased == 1) {
			spinDiskBt.SetActive (true);
			jukeboxBt.SetActive (true);
		} else {
			spinDiskBt.SetActive (false);
			jukeboxBt.SetActive (false);
		}
	}

	public void SetFirstSongPurchased(){
		PlayerPrefs.SetInt ("firstSongPurchased", 1);
		firstSongPurchased = 1;
		globals.s.JUKEBOX_CURRENT_PRICE = GD.s.JUKEBOX_PRICE;
	}

	IEnumerator OpenDiskMenu(){
		yield return new WaitForSeconds (0.01f);
//		hud_controller.si.RodaMenu ();
	}

	public void AllowIntroduceStore(){
		PlayerPrefs.SetInt ("canIntroduceStore", 1);
		canIntroduceStore = 1;
		GameOverController.s.jukeboxGroup.SetActive (true);
	}

}
