using System.Collections;
using UnityEngine;
using System;
//using UnityEngine.UI;

public class RemoteMaster : MonoBehaviour {
	public static RemoteMaster s;
	public int maximumAllowedChars = 24;
	public int minimumUnlockedChars = 18;

	//public int 

	void Awake(){
		s = this;

		DateTime tempcurDate = System.DateTime.Now;
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
}
