using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FTUController : MonoBehaviour {
	public static FTUController s;

	public int diskIntroduced = 0;
	public int firstSongPurchased = 0;

	public GameObject spinDiskBt, jukeboxBt, handTut;

	void Awake(){
		s = this;
		diskIntroduced = PlayerPrefs.GetInt ("diskIntroduced", 0);
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
		if (USER.s.NEWBIE_PLAYER == 0 && diskIntroduced == 0) {
//			hud_controller.si.RodaMenu ();
			PlayerPrefs.SetInt ("diskIntroduced", 1);
			diskIntroduced = 1;
			StartCoroutine (OpenDiskMenu ());
		}

//		if (diskIntroduced == 0) {
//			spinDiskBt.SetActive (false);
//			jukeboxBt.SetActive (false);
//		}
//
		if (USER.s.NEWBIE_PLAYER == 1 && QA.s.OLD_PLAYER == false ) {
			spinDiskBt.SetActive (false);
			jukeboxBt.SetActive (false);
//			handTut.SetActive (true);

		} else {
			spinDiskBt.SetActive (true);
			jukeboxBt.SetActive (true);
//			handTut.SetActive (false);
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
	// Update is called once per frame
	void Update () {
	
	}
}
