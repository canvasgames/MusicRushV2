using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CohortMaster : MonoBehaviour {
	//-------- UPDATE ORDER -------
	// 0 = CURRENT 18 CHARS
	// 1 = RAP
	// 2 = LATINA
	// 3 = REGGAE
	// 4 = ANIME SHONEN
	// 5 = KPOP OR DINGO BELLS
	public static CohortMaster s;
	public int newCharacterForOpenAtStore = 0;

	void Awake(){
		s = this;
		DateTime tempcurDate = System.DateTime.Now;
		DateTime tempNextSaturdayDate = Convert.ToDateTime(USER.s.FIRST_SATURDAY);
			
//		USER.s.LAST_UPDATE_UNLOCKED = ;

		TimeSpan diff = tempcurDate.Subtract(tempNextSaturdayDate);
		Debug.Log ("[CH] DIF DAYS: " + diff.Days  +" NEXT SAT: " + USER.s.FIRST_SATURDAY + " DIF H: " + diff.Hours + " DIF TOTAL H: " + diff.TotalHours + " DIFF MIN: "+ diff.Minutes + " DIFF TOTAL MIN: "+ diff.TotalMinutes );
		bool displayCustomMessage = false;

		// Try to Unlock rap
		if (USER.s.LAST_UPDATE_UNLOCKED == 0 && diff.Days >= 0 && (GD.s.N_SKINS + GD.s.SKINS_PER_MUSIC * USER.s.LAST_UPDATE_UNLOCKED) < RemoteMaster.s.maximumAllowedChars) {
//		if (1==1) {
			Debug.Log ("WOOOOOW A 1 WEEK RETENTION USER. WE LOVE YOU!!!");
//			GD.s.N_SKINS += GD.s.SKINS_PER_MUSIC;
			USER.s.SetUpdateCharsUnlocked ();
			displayCustomMessage = true;
		} 
		//  Try to Unlock latina
		if (USER.s.LAST_UPDATE_UNLOCKED == 1 && diff.Days >= 6 && (GD.s.N_SKINS + GD.s.SKINS_PER_MUSIC * USER.s.LAST_UPDATE_UNLOCKED) < RemoteMaster.s.maximumAllowedChars) { 
			Debug.Log ("WOOOOOW A 2 WEEK RETENTION USER. WE LOVE YOU EVEN MORE!!!");
//			GD.s.N_SKINS += GD.s.SKINS_PER_MUSIC;6
			USER.s.SetUpdateCharsUnlocked ();
			displayCustomMessage = true;
		} 
		// Try to Unlock Reggae
		if (USER.s.LAST_UPDATE_UNLOCKED == 2 && diff.Days >= 13 && (GD.s.N_SKINS + GD.s.SKINS_PER_MUSIC * USER.s.LAST_UPDATE_UNLOCKED) < RemoteMaster.s.maximumAllowedChars) { 
			Debug.Log ("WOOOOOW A 3 WEEK RETENTION USER. WE LOVE YOU PLEASE MARRY ME!!!");
			USER.s.SetUpdateCharsUnlocked ();
//			GD.s.N_SKINS += GD.s.SKINS_PER_MUSIC;
			displayCustomMessage = true;
		} 
		// Try to Unlock Anime
		if (USER.s.LAST_UPDATE_UNLOCKED == 3 && diff.Days >= 21 && (GD.s.N_SKINS + GD.s.SKINS_PER_MUSIC * USER.s.LAST_UPDATE_UNLOCKED) < RemoteMaster.s.maximumAllowedChars) { // Unlock Reggae
			Debug.Log ("WOOOOOW A 4 WEEK RETENTION USER. LET'S HAVE SEX. NOW.");
//			GD.s.N_SKINS += GD.s.SKINS_PER_MUSIC;
			USER.s.SetUpdateCharsUnlocked ();
			displayCustomMessage = true;
		} 
//		else {
//			Debug.Log ("[COH] NO UPDATE TO SHOW");
//			hud_controller.si.DisplayNewCharactersAvailable ();
//		}

		GD.s.N_SKINS += GD.s.SKINS_PER_MUSIC * USER.s.LAST_UPDATE_UNLOCKED;
		GD.s.N_SKINS = 24;
		GD.s.N_STYLES = GD.s.N_SKINS / 3;
		if (displayCustomMessage) {
			hud_controller.si.DisplayNewCharactersAvailable ();
			newCharacterForOpenAtStore = GD.s.N_SKINS - 1;
		}
//		newCharacterForOpenAtStore = GD.s.N_SKINS - 1;

		//		tempDateRoulette = Convert.ToDateTime(roullete_date);
		//		PlayerPrefs.SetString("RouletteDate2ChangeState", tempDateRoulette.ToString());
		//		int canRotate = PlayerPrefs.GetInt("CanRotate", 1);
		//		if (canRotate == 1) {
		//			CAN_ROTATE_ROULETTE = true;
		//			if(activate_pw_bt.activeInHierarchy) activate_pw_bt.GetComponent<Button> ().interactable = true;

		//		//NO DATE CASE, TRIGGER 5 MINUTES
		//		{
		//			TimeSpan diff = tempDate.Subtract(tempcurDate);
		//			//Debug.Log(diff + "  TimeDif " + globals.s.PW_ACTIVE);
		//			if (diff.Minutes > GD.s.GD_WITHOUT_PW_TIME && globals.s.PW_ACTIVE == false)
		//			{
		//				Debug.Log("new date");
		//
		//				PW_time_set_new_date_and_state(true);
		//			}
		//			else
		//			{
		//				if (tempDate < tempcurDate)
		//				{
		//					Debug.Log("new date");
		//
		//					PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
		//				}
		//			}
		//		}
		//
		//
		//		TimeSpan difference = tempDate.Subtract(tempcurDate);
		
	}

	
	// Update is called once per frame
//	void Update () {
		
//	}
}
