using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DebugUnlockAllChars : MonoBehaviour {
	int nTimes = 0;

	public void Unlock(){
		nTimes++; 
		if (nTimes > 4) {
			DateTime tempcurDate = System.DateTime.Now;
			tempcurDate = tempcurDate.AddDays (-90);
			USER.s.FIRST_SATURDAY = tempcurDate.ToString ();
			PlayerPrefs.SetString ("first_saturday", USER.s.FIRST_SATURDAY);
			USER.s.AddGems (600, "Debug");
			SceneManager.LoadScene("Gameplay Final");
		}
	}
}
